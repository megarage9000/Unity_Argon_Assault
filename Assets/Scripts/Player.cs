using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";

    [Tooltip("In ms^-1")] [SerializeField] float movementSpeed = 16f;

    [SerializeField] float xRange = 8f;
    [SerializeField] float yRange = 6f;
    [SerializeField] float pitchPositionFactor= -2.5f;
    [SerializeField] float pitchControlFactor = -20f;
    [SerializeField] float yawPositionFactor = 1f;
    [SerializeField] float rollControlFactor = 20f;

    private float xThrow, yThrow;
    private float xMaxRange, xMinRange, yMaxRange, yMinRange;
    
    // Start is called before the first frame update
    void Start()
    {
        /*
         * Using local position as we are only concerned about the ships' position relative
         * to itself, not to the world.
         */
        xMaxRange = transform.localPosition.x + xRange;
        xMinRange = transform.localPosition.x - xRange;
        yMaxRange = transform.localPosition.y + yRange;
        yMinRange = transform.localPosition.y - yRange;

        Debug.Log("X max: " + xMaxRange + ", X min: " + xMinRange + ", Y max: " + yMaxRange + " Y min: " + yMinRange);
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }
    
    // To Detect collisions!
    private void OnTriggerEnter(Collider other)
    {
        print("Player triggered something");   
    }


    private void RotatePlayer()
    {
        /*
         * Remember, the order of rotation matters! Rotating the pitch, then the yaw will
         * be different than rotation the yaw, then the pitch.
         */

        // Controls pitch of ship due to y movement and y position
        float pitchToPosition = transform.localPosition.y * pitchPositionFactor;
        float pitchToControl = yThrow * pitchControlFactor;
        float pitch = pitchToPosition + pitchToControl;

        // Controls yaw of ship due to x position
        float yaw = transform.localPosition.x * yawPositionFactor;

        // Controls roll of ship due to x movement
        float roll = xThrow * rollControlFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    // Prototype function, tryna have player look at mouse position!
    private void RotatePlayerToMouseAim()
    {
        float rotationFactor = 100f;
        float xRotation = CrossPlatformInputManager.GetAxis("Mouse X") * rotationFactor * Time.deltaTime;
        float yRotation = CrossPlatformInputManager.GetAxis("Mouse Y") * rotationFactor * Time.deltaTime;

        xRotation = xRotation + transform.localRotation.x;
        yRotation = yRotation + transform.localRotation.y;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
    private void MovePlayer()
    {
        /*
         * xThrow and yThrow determine the sensitivity of the x and y axis, ranging between
         * -1 and 1!
         */

        xThrow = CrossPlatformInputManager.GetAxis(HORIZONTAL);
        yThrow = CrossPlatformInputManager.GetAxis(VERTICAL);

        /*
         * The speeds are multiplied to their respective throws on top of Time.deltaTime to
         * account for the varying times between frames. The longer the time between a frame,
         * the greater the value.
         */

        float xSpeed = xThrow * movementSpeed * Time.deltaTime;
        float ySpeed = yThrow * movementSpeed * Time.deltaTime;

        float xPos = xSpeed + transform.localPosition.x;
        float yPos = ySpeed + transform.localPosition.y;

        xPos = Mathf.Clamp(xPos, xMinRange, xMaxRange);
        yPos = Mathf.Clamp(yPos, yMinRange, yMaxRange);

        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
    }
}
