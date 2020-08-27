using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{

    [SerializeField] GameObject DeathExplosionFX;
    [SerializeField] Transform Parent;
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
        GameObject explosion = Instantiate(DeathExplosionFX, transform.position, Quaternion.identity);
        explosion.transform.parent = Parent;
        Destroy(gameObject);
    }
}
