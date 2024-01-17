using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UponBossDeath : MonoBehaviour
{
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject Blockade;
    [SerializeField] GameObject open;
    [SerializeField] GameObject Closed;
    fEnemyDatasheet fEnemy;
    bool done;

    // Start is called before the first frame update
    void Start()
    {
        open.SetActive(false);
        Closed.SetActive(true);
        fEnemy = Boss.GetComponent<fEnemyDatasheet>(); 
        done = false;
    }

    private void Update()
    {
        if (!done)
        {
            if (fEnemy.enemyHealth <= 0 && Boss.active)
            {
                done = true;
                open.SetActive(true);
                Closed.SetActive(false);
                Blockade.SetActive(false);
            }
        }
        
    } 

}