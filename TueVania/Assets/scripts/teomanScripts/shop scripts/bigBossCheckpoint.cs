using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigBossCheckpoint : MonoBehaviour
{
    public levelManager level_manager;
    public fEnemyDatasheet bossData;
    public UponBossDeath uponBossDeath;
    public healthManager healthManagerHere;

    
    // Start is called before the first frame update
    void Start()
    {
        level_manager = FindObjectOfType<levelManager>();
        uponBossDeath = FindObjectOfType<UponBossDeath>();
        healthManagerHere = FindObjectOfType<healthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player")
		{
			level_manager.currentCheckpoint = gameObject;
            Debug.Log (("Activated Checkpoint") + transform.position);
		}

        if (healthManagerHere.isDead){
            Debug.Log("lets go");
            bossData.enemyHealth = 300;
            uponBossDeath.broJustDied();
        }
        
	}
}
