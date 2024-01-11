using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fEnemyDatasheet : MonoBehaviour, IEnemy
{
    public int enemyHealth;
    public int pointsOnDeath;

    // Use this for initialization
    void Start()
    {
        // Initialization code if needed
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            HandleEnemyDeath();
        }
    }

    public void TakeDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;
    }

    void HandleEnemyDeath()
    {
        // Instantiate deathEffect if needed
        // Instantiate(deathEffect, transform.position, transform.rotation);

        // Add points to the player's score
        PlayerScoreManager.AddPoints(pointsOnDeath);

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}
