using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fEnemyDatasheet : MonoBehaviour, IEnemy
{
    public int enemyHealth;
    public int pointsOnDeath;


    public AnimationControlScript animation;
    const string FlyHit = "FlyHit";
    const string DeathFly = "DeathFly";

    const string fEnemyFly = "fEnemyFly";

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
        animation.ChangeAnimationState(fEnemyFly);
    }

    public void TakeDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;
        animation.ChangeAnimationState(FlyHit);
    }

    void HandleEnemyDeath()
    {
        // Instantiate deathEffect if needed
        // Instantiate(deathEffect, transform.position, transform.rotation);

        // Add points to the player's score
        PlayerScoreManager.AddPoints(pointsOnDeath);
        animation.ChangeAnimationState(DeathFly);

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}
