using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";

    [Header("General movement information")]
    [Tooltip("In ms^-1")] [SerializeField] float movementSpeed = 25f;
    [Tooltip("In m")] [SerializeField] float xRange = 12f;
    [Tooltip("In m")] [SerializeField] float yRange = 6f;

    [Header("Weapons")]
    [SerializeField] GameObject[] guns;

    [Header("Aim parameters")]
    [Range(0, 1)]
    [SerializeField] float AimSensitivity = 0.5f;
    [SerializeField] float maxAimZDist = 500f;
   
    private bool canMove = true;
    private float xThrow, yThrow;
    private float xMaxRange, xMinRange, yMaxRange, yMinRange;
    private Camera mainCam;
    

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

        mainCam = gameObject.transform.parent.GetComponent<Camera>();
        
        Debug.Log("X max: " + xMaxRange + ", X min: " + xMinRange + ", Y max: " + yMaxRange + " Y min: " + yMinRange);
    }

    // Called by string method in CollisionHandler.cs
    void DisableMovement()
    {
        canMove = false;
    }

    void Update()
    {
        if (canMove)
        {
            MovePlayer();
            RotatePlayerToMouseAim();
            FireGuns();
        }
    }


    // Prototype function, tryna have player look at mouse position!
    private void RotatePlayerToMouseAim()
    {
        // Solution Source! 
        // https://answers.unity.com/questions/564009/how-to-shoot-exactly-where-mouse-is-third-person.html
        Vector3 worldPosition = transform.position;
        Vector3 lookPosition;
        Ray mouseRay = mainCam.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(mouseRay, out RaycastHit hitMouse, maxAimZDist))
        {
            lookPosition = hitMouse.point;
        }
        else
        {
            lookPosition = Input.mousePosition;
            lookPosition.z = worldPosition.z + maxAimZDist;
            lookPosition = mainCam.ScreenToWorldPoint(lookPosition);
        }

        Vector3 direction = lookPosition - worldPosition;
        Debug.DrawLine(lookPosition, worldPosition, Color.green, 0.5f);
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, AimSensitivity);

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
    private void FireGuns()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns();
        }
        else
        {
            DeactivateGuns();
        }
    }

    private void DeactivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }

    private void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }
}
