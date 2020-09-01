using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO implement a combo timer! 

public class ScoreBoard : MonoBehaviour
{
    private Text scoreText;
    private int score;
    

    private void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    public void UpdateScore(int scoreIncrement)
    {
        score += scoreIncrement;
        scoreText.text = score.ToString();
    }

}
