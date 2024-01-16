﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class healthManager : MonoBehaviour {

	[Header("Health Info")] 
    [SerializeField] static int playerHealth;
    [SerializeField] static int maxPlayerHealth = 20;

	public bool isDead;

	public Slider healthBar;

	//TMP_Text  text;

	private levelManager levelManager;

	private lifeManager lifeSystem;

	// Use this for initialization
	void Start () {

		//text = GetComponent<TMP_Text>(); 

		//healthBar = GetComponent<Slider>();

		playerHealth = maxPlayerHealth;

		levelManager = FindObjectOfType<levelManager>();
		isDead = false;

		lifeSystem = FindObjectOfType<lifeManager>();
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log("Current lives: " + playerHealth);
		if (playerHealth <= 0 && !isDead)
		{
			Debug.Log("died");
			levelManager.RespawnPlayer();
			isDead = true;
			playerHealth = maxPlayerHealth;
			lifeSystem.TakeLife();
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
		Debug.Log("Player Hurt");
		playerHealth -= damageToGive;
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
}