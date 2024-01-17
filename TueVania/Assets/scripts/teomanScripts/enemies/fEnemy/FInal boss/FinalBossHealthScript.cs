using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBossHealthScript : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] fEnemyDatasheet enemyDataSheet;
    [SerializeField] flyingChaseEnemy flyingChaseEnemy;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = enemyDataSheet.enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = enemyDataSheet.enemyHealth;
        if (enemyDataSheet.enemyHealth < 50)
        {
            flyingChaseEnemy.speed = 8;
        } else if (enemyDataSheet.enemyHealth < 100)
        {
            flyingChaseEnemy.speed = 6;
        } else if (enemyDataSheet.enemyHealth < 200)
        {
            flyingChaseEnemy.speed = 4;
        } else 
        {
            flyingChaseEnemy.speed = 2.5f;
        }
    }
}
