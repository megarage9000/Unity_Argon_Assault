using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] int scoreIncrement = 12;
    private Text scoreText;
    private int score;
    

    private void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    public void UpdateScore()
    {
        score += scoreIncrement;
    }

}
