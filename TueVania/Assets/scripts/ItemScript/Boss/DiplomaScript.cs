using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DiplomaScript : MonoBehaviour
{
    BoxCollider2D BoxCollider2D;
    SenderScript senderScript;

    public playerData pdata;


    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
        senderScript = FindObjectOfType<SenderScript>();

        if (pdata == null)
        {
            // Find PlayerData script if not assigned in the Inspector
            pdata = FindObjectOfType<playerData>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //add score and time stuff here
            Destroy(this.gameObject);
            
            senderScript.Send();
            //finishScore.fScore = pdata.PlayerScore;
            SceneManager.LoadScene("EndScene");
        }
    }

    
}
