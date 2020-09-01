using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{

    [SerializeField] GameObject DeathExplosionFX;
    [SerializeField] Transform Parent;
    [SerializeField] int pointsOnDeath = 10;
    [SerializeField] int hits = 10;

    ScoreBoard scoreBoard;
    

    private void Start()
    {
        SetUpCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void SetUpCollider()
    {
        Collider EnemyCollider = gameObject.AddComponent<BoxCollider>();
        EnemyCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        UpdateScoreBoard();
        hits--;
        if(hits < 1)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject explosion = Instantiate(DeathExplosionFX, transform.position, Quaternion.identity);
        explosion.transform.parent = Parent;
        Destroy(gameObject);
    }

    private void UpdateScoreBoard()
    {
        scoreBoard.UpdateScore(pointsOnDeath);
    }
}
