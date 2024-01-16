using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;


public class pEnemyDataSheet : MonoBehaviour, IEnemy
{
    public int enemyHealth;
    public int pointsOnDeath;
    public AnimationControlScript animation;
    
    const string Hit = "Hit";
    const string Death = "Death";

    const string pEnemyWalk = "pEnemyWalk";

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
        animation.ChangeAnimationState(pEnemyWalk);
    }

    void FixedUpdate() {
        Count();
    }

    public void TakeDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;

        if (enemyHealth > 0) {
            animation.ChangeAnimationState(Hit);
        } else {
            animation.ChangeAnimationState(Death);
        }
        
        
        currentAnimTime = 1f;
        if (currentAnimTime <= 0) {
  
        }
        
        
    }

    void HandleEnemyDeath()
    {
        // Instantiate deathEffect if needed
        // Instantiate(deathEffect, transform.position, transform.rotation);

        // Add points to the player's score
        
        
        if (currentAnimTime <=  0) {

            PlayerScoreManager.AddPoints(pointsOnDeath);
            // Destroy the enemy GameObject
            Destroy(gameObject);
            
        }
        
    }

    float currentAnimTime;
    bool countController;
    private void Count()
    {
        if (currentAnimTime > 0)
            currentAnimTime -= Time.fixedDeltaTime;
    }
}
