using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicLoader : MonoBehaviour
{
    private void Awake()
    {

        // Mimicking Singleton Pattern!

        int numMusicPlayers = FindObjectsOfType<MusicLoader>().Length;
        if(numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
}
