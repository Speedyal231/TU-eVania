using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedManager : MonoBehaviour
{
    private float playerSpeed;

    private static PlayerSpeedManager _instance;
    public static PlayerSpeedManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }

    public void ModifySpeed(float speedModifier)
    {
        playerSpeed *= speedModifier;
    }
}
