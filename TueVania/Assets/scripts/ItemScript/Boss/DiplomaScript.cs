using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiplomaScript : MonoBehaviour
{
    BoxCollider2D BoxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //add score and time stuff here
            Destroy(this.gameObject);
        }
    }

    
}
