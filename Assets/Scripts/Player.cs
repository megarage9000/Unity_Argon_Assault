using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";
    [Tooltip("In ms^-1")][SerializeField] float XSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
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
        float horizontalSpeed = xThrow * XSpeed * Time.deltaTime;
        float verticalSpeed = yThrow * XSpeed * Time.deltaTime;

        Debug.Log("Horizontal Speed : " + horizontalSpeed + ". Vertical Speed : " + verticalSpeed);
        
    }
}
