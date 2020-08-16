using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalThrow = CrossPlatformInputManager.GetAxis(HORIZONTAL);
        float verticalThrow = CrossPlatformInputManager.GetAxis(VERTICAL);

        Debug.Log("Horizontal: " + horizontalThrow + " Vertical :" + verticalThrow);
    }
}
