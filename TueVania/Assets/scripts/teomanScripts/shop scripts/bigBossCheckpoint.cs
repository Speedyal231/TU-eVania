using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigBossCheckpoint : MonoBehaviour
{
    public levelManager level_manager;
    public fEnemyDatasheet bossData;
    public UponBossDeath uponBossDeath;
    public healthManager healthManagerHere;
    private bool temp;

    
    // Start is called before the first frame update
    void Start()
    {
        level_manager = FindObjectOfType<levelManager>();
        uponBossDeath = FindObjectOfType<UponBossDeath>();
        healthManagerHere = FindObjectOfType<healthManager>();
        temp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthManagerHere.isDead){
            temp = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player")
		{
			level_manager.currentCheckpoint = gameObject;
            Debug.Log (("Activated Checkpoint") + transform.position);
		}

        if (temp){
            Debug.Log("lets go");
            bossData.enemyHealth = 300;
            uponBossDeath.broJustDied();
            temp = false;
        }
        
	}
}
