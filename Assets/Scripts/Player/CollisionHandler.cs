using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("Death iniatated");
        InitateDeathSequence();
    }
    
    private void InitateDeathSequence()
    {
        SendMessage("DisableMovement");
    }
}
