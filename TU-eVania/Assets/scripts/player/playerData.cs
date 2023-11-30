using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerData : MonoBehaviour
{

    public static int playerHealth;
    public static int playerScore;
    public GameObject currentCheckpoint;
    public bool isDead;
    public int maxPlayerHealth;
    TMP_Text  textHealth;
    TMP_Text  textScore;
	private levelManager levelManager;
    public tempPlayerController player;
    public int damageToGive;
    public float bounceOnEnemy;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        textHealth = GetComponent<TMP_Text>(); 
        textScore = GetComponent<TMP_Text>(); 
		playerHealth = maxPlayerHealth;
		levelManager = FindObjectOfType<levelManager>();
		isDead = false;
        player = FindObjectOfType<tempPlayerController>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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

        if (playerScore < 0){
            playerScore = 0;
        }
        textScore.text = "" + playerScore;
    }

    public static void hurtPlayer(int damageToGive)
    {
		playerHealth -= damageToGive;
    }

    public void fullHealth()
    {
		playerHealth = maxPlayerHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag== "enemy1")
        {
			other.GetComponent<pEnemyDataSheet>().giveDamage(damageToGive);
			rb.velocity = new Vector2(rb.velocity.x, bounceOnEnemy);
        } else if (other.tag== "enemy2") {
            other.GetComponent<fEnemyDatasheet>().giveDamage(damageToGive);
			rb.velocity = new Vector2(rb.velocity.x, bounceOnEnemy);
        }
    }

    public static void AddPoints(int pointsToAdd)
    {
        playerScore += pointsToAdd;
    }

    public static void Reset()
    {
        playerScore = 0;
    }

}
