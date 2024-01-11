using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerData : MonoBehaviour
{
    private float modifiedPlayerSpeed;

    private void Start()
    {
        // Initialization if needed...
    }

    public int PlayerHealth
    {
        get { return PlayerHealthManager.playerHealth; }
        set { PlayerHealthManager.playerHealth = value; }
    }

    public int PlayerScore
    {
        get { return PlayerScoreManager.playerScore; }
        set { PlayerScoreManager.playerScore = value; }
    }

    public float ModifiedPlayerSpeed
    {
        get { return modifiedPlayerSpeed; }
        set { modifiedPlayerSpeed = value; }
    }
}
