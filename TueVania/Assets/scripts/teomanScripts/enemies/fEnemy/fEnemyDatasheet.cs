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
    private Collider2D myCollider;

    // Use this for initialization
    void Start()
    {
        // Initialization code if needed
        myCollider = GetComponent<Collider2D>();

        if (myCollider == null)
        {
            Debug.LogError("You fucked up");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyHealth <= 0)
        {
            myCollider.enabled = false;
            animation.ChangeAnimationState(DeathFly);
            HandleEnemyDeath();
        }
        else if (currentAnimTime > 0)
        {
            animation.ChangeAnimationState(FlyHit);
        }
        else 
        {
            animation.ChangeAnimationState(fEnemyFly);
        }

    }

    void FixedUpdate() {
        Count();
    }

    public void TakeDamage(int damageToGive)
    {
        if (currentStunTime <= 0)
        {
            enemyHealth -= damageToGive;
            currentAnimTime = 0.3f;
            currentStunTime = 0.05f;
        }
    }

    void HandleEnemyDeath()
    {
        // Instantiate deathEffect if needed
        // Instantiate(deathEffect, transform.position, transform.rotation);

        if (currentAnimTime <=  0) {

            PlayerScoreManager.AddPoints(pointsOnDeath);
            // Destroy the enemy GameObject
            Destroy(gameObject);
            
        }
    }

    float currentAnimTime;
    float currentStunTime;

    bool countController;
    private void Count()
    {
        if (currentAnimTime > 0)
            currentAnimTime -= Time.fixedDeltaTime;
        if (currentStunTime > 0)
            currentStunTime -= Time.fixedDeltaTime;
    }
}
