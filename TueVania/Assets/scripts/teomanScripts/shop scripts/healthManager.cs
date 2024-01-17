using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class healthManager : MonoBehaviour {

	[Header("Health Info")] 
    [SerializeField] static int playerHealth;
	CharacterController characterController;
    [SerializeField] static int maxPlayerHealth = 20;

	public bool isDead;

	public Slider healthBar;

	//TMP_Text  text;

	private levelManager levelManager;

	private lifeManager lifeSystem;

	public bool animPlayed;

	static bool invincible;

	public bool checkStunned;

    private void Awake()
    {
        playerHealth = maxPlayerHealth;
    }

    // Use this for initialization
    void Start () {

        //text = GetComponent<TMP_Text>(); 

        //healthBar = GetComponent<Slider>();
        characterController = FindObjectOfType<CharacterController>();
        if (playerHealth <= 0) { playerHealth = maxPlayerHealth; }

		levelManager = FindObjectOfType<levelManager>();
		isDead = false;

		lifeSystem = FindObjectOfType<lifeManager>();
	}
	
	// Update is called once per frame
	void Update () {

		invincible = characterController.getInvincible();
		//Debug.Log("Current lives: " + playerHealth);
		if (playerHealth <= 0)
		{
            isDead = true;
            Debug.Log(animPlayed +" number");
            Debug.Log(animPlayed + " number");
            if (animPlayed && !checkStunned) {
				Debug.Log("died");
				levelManager.RespawnPlayer();
				playerHealth = maxPlayerHealth;
				lifeSystem.TakeLife();
			}
		}
		//text.text = "" + playerHealth;
		healthBar.value = playerHealth;

        if (playerHealth > maxPlayerHealth)
        {
			playerHealth = maxPlayerHealth;
        }
	}

    public static void hurtPlayer(int damageToGive)
    {
		if (!invincible)
		{
            Debug.Log("Player Hurt");
            playerHealth -= damageToGive;
        }	
    }

    public void fullHealth()
    {
		playerHealth = maxPlayerHealth;
    }

	public static void increaseHealth()
    {
		if (playerHealth == maxPlayerHealth) {
			Debug.Log("Max Health");
			return;
		}
		playerHealth += 1;
    }


	public bool getDeadStatus() {
		return isDead;
	}

    public int getHealth()
    {
        return playerHealth;
    }

    public void setHealth(int health)
    {
        playerHealth = health;
    }

}