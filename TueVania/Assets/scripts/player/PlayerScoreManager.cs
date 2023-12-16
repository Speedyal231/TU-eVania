using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    public static int playerScore;
    public int damageToGive;
    public float bounceOnEnemy;

    private TMP_Text textScore;

    private static PlayerScoreManager _instance;
    public static PlayerScoreManager Instance { get { return _instance; } }

    void Start()
    {
        textScore = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (playerScore < 0)
        {
            playerScore = 0;
        }

        // Update UI or handle score-related logic here
        // textScore.text = "" + playerScore;
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
