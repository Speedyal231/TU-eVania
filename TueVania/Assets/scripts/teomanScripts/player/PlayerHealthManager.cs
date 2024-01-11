using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxPlayerHealth;
    public static int playerHealth;
    public bool isDead;
    public int damageToGive;

    private levelManager levelManager;
    private tempPlayerController player;
    private Rigidbody2D rb;

    private static PlayerHealthManager _instance;
    public static PlayerHealthManager Instance { get { return _instance; } }
    public float bounceOnEnemy; 

    void Start()
    {
        playerHealth = maxPlayerHealth;
        levelManager = FindObjectOfType<levelManager>();
        isDead = false;
        player = FindObjectOfType<tempPlayerController>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Awake()
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

    void Update()
    {
        if (playerHealth <= 0 && !isDead)
        {
            levelManager.RespawnPlayer();
            isDead = true;
            playerHealth = 0;
        }

        if (playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }
    }

    public static void HurtPlayer(int damageToGive)
    {
        playerHealth -= damageToGive;
    }

    public void FullHealth()
    {
        playerHealth = maxPlayerHealth;
    }

}
