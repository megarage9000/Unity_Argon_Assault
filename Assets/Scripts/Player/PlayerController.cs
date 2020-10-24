﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";
    const string MOUSE_X = "Mouse X";
    const string MOUSE_Y = "Mouse Y";

    const float MAX_ROT_Y = 90f;
    const float MAX_ROT_X = 90f;

    [Header("General movement information")]
    [Tooltip("In ms^-1")] [SerializeField] float movementSpeed = 25f;
    [Tooltip("In m")] [SerializeField] float xRange = 12f;
    [Tooltip("In m")] [SerializeField] float yRange = 6f;
    
    [Header("Rotation based on position")]
    [SerializeField] float pitchPositionFactor= -2f;
    [SerializeField] float yawPositionFactor = 1f;
    
    [Header("Rotation based on movement")]
    [SerializeField] float pitchControlFactor = -20f;
    [SerializeField] float rollControlFactor = 20f;

    [Header("Weapons")]
    [SerializeField] GameObject[] guns;

    [Header("Aim parameters")]
    [Range(0, 1)]
    [SerializeField] float rotationSensitivity = 0.5f;
    [SerializeField] float aimZ = 10000f;
   
    private bool canMove = true;
    private float xThrow, yThrow;
    private float rotationX, rotationY;
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

        Vector3 rotation = transform.localRotation.eulerAngles;
        rotationX = rotation.x;
        rotationY = rotation.y;
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
            //RotatePlayer();
            FireGuns();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            RotatePlayerToMouseAim();
        }
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
        Vector3 mousePositionOnScreen = Input.mousePosition;
        Vector3 positionOnScreen = mainCam.WorldToScreenPoint(transform.position);
        positionOnScreen.z = 0;
        mousePositionOnScreen.z = aimZ;

        Vector3 relativePos = mousePositionOnScreen - positionOnScreen;
        // relativePos = mainCam.ScreenToWorldPoint(relativePos);
        Quaternion lookRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.localRotation = lookRotation;
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
