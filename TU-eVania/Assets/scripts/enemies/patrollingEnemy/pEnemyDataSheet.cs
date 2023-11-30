using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pEnemyDataSheet : MonoBehaviour {
	public int enemyHealth;

	//public GameObject deathEffect;

	public int pointsOnDeath;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (enemyHealth < 0)
        {

			//Instantiate(deathEffect, transform.position, transform.rotation);
			playerData.AddPoints(pointsOnDeath);
			Destroy(gameObject);
        }

		
	}
    public void giveDamage(int damageToGive)
    {
		enemyHealth -= damageToGive;
    }
         
}