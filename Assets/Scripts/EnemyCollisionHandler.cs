using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{

    private void Start()
    {
        SetUpCollider();
    }

    private void SetUpCollider()
    {
        Collider EnemyCollider = gameObject.AddComponent<BoxCollider>();
        EnemyCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        print(gameObject.name + " Hit by " + other.name);
        Destroy(gameObject);
    }
}
