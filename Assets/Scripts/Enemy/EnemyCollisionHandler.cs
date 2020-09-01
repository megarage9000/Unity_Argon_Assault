using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{

    [SerializeField] GameObject DeathExplosionFX;
    [SerializeField] Transform Parent;
    [SerializeField] int pointsOnDeath = 10;

    ScoreBoard scoreBoard;
    private bool isDead;

    private void Start()
    {
        isDead = false;
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
        if (!isDead)
        {
            GameObject explosion = Instantiate(DeathExplosionFX, transform.position, Quaternion.identity);
            explosion.transform.parent = Parent;
            UpdateScoreBoard();
            Destroy(gameObject);
            isDead = true;
        }
    }

    private void UpdateScoreBoard()
    {
        scoreBoard.UpdateScore(pointsOnDeath);
    }
}
