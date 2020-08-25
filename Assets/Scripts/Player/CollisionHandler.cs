using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float restartLevelDelay = 1f;
    public GameObject DeathExplosion;

    private void OnTriggerEnter(Collider other)
    {
        print("Death iniatated");
        InitateDeathSequence();
    }
    
    private void InitateDeathSequence()
    {
        SendMessage("DisableMovement");
        DeathExplosion.SetActive(true);
        Invoke("RestartCurrentLevel", restartLevelDelay);
    }

    // String Referenced
    private void RestartCurrentLevel()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentBuildIndex);
    }
}
