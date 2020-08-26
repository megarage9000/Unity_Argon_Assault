using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        print(gameObject.name + " Hit by " + other.name);
        Destroy(gameObject);
    }
}
