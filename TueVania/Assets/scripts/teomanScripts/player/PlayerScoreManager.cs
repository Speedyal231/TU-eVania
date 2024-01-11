using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    public static int playerScore;
    public TMP_Text textScore;

    private static PlayerScoreManager _instance;
    public static PlayerScoreManager Instance { get { return _instance; } }

    void Start()
    {

    }

    void Update()
    {
        if (playerScore < 0)
        {
            playerScore = 0;
        }

        // Update UI or handle score-related logic here
        textScore.text = "score: " + playerScore;
    }

    public static void AddPoints(int pointsToAdd)
    {
        playerScore += pointsToAdd;
    }

    public static void Reset()
    {
        playerScore = 0;
    }

    public static int ReturnScore()
    {
        return playerScore;
    }
}
