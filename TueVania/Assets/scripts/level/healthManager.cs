using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class healthManager : MonoBehaviour {
	public static int playerHealth;
	public int maxPlayerHealth;

	public bool isDead;

	//public Slider healthBar;

	TMP_Text  text;

	private levelManager levelManager;

	private lifeManager lifeSystem;

	// Use this for initialization
	void Start () {

		text = GetComponent<TMP_Text>(); 

		//healthBar = GetComponent<Slider>();

		playerHealth = maxPlayerHealth;

		levelManager = FindObjectOfType<levelManager>();
		isDead = false;

		lifeSystem = FindObjectOfType<lifeManager>();
	}
	
	// Update is called once per frame
	void Update () {

		if (playerHealth <= 0 && !isDead)
		{
			levelManager.RespawnPlayer();
			isDead = true;
			playerHealth = 0;
			lifeSystem.TakeLife();
		}
		//text.text = "" + playerHealth;
		//healthBar.value = playerHealth;

        if (playerHealth > maxPlayerHealth)
        {
			playerHealth = maxPlayerHealth;
        }
	}

    public static void hurtPlayer(int damageToGive)
    {
		playerHealth -= damageToGive;
    }

    public void fullHealth()
    {
		playerHealth = maxPlayerHealth;
    }
}