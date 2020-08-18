using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";

    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 1f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 1f;

    [SerializeField] float xRange = 4f;
    [SerializeField] float yRange = 4f;

    private float xMaxRange, xMinRange, yMaxRange, yMinRange;
    
    // Start is called before the first frame update
    void Start()
    {
        xMaxRange = transform.localPosition.x + xRange;
        xMinRange = transform.localPosition.x - xRange;
        yMaxRange = transform.localPosition.y + yRange;
        yMinRange = transform.localPosition.y - yRange;

        Debug.Log("X max: " + xMaxRange + ", X min: " + xMinRange + ", Y max: " + yMaxRange + "Y min: " + yMinRange);
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * xThrow and yThrow determine the sensitivity of the x and y axis, ranging between
         * -1 and 1!
         */
        float xThrow = CrossPlatformInputManager.GetAxis(HORIZONTAL);
        float yThrow = CrossPlatformInputManager.GetAxis(VERTICAL);

        /*
         * The speeds are multiplied to their respective throws on top of Time.deltaTime to
         * account for the varying times between frames. The longer the time between a frame,
         * the greater the value.
         */
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        MovePlayer(xOffset, yOffset); 
    }

    private void MovePlayer(float xOffset, float yOffset)
    {
        /*
         * Normalizing the speed for consistent speed
         * Link: https://answers.unity.com/questions/1190224/how-can-i-normalize-2d-vectors.html
         */

        Vector2 totalVectorOffset = new Vector2(xOffset, yOffset);
        totalVectorOffset.Normalize();

        float xPos = totalVectorOffset.x + transform.localPosition.x;
        float yPos = totalVectorOffset.y + transform.localPosition.y;

        xPos = Mathf.Clamp(xPos, xMinRange, xMaxRange);
        yPos = Mathf.Clamp(yPos, yMinRange, yMaxRange);

        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
    }
}
