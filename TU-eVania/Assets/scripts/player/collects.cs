using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collects : MonoBehaviour {

    public int pointsToAdd;


    void OnTriggerEnter2D(Collider2D other)
    {
        {
            if (other.GetComponent<tempPlayerController>() == null)
                return;
        }
        playerData.AddPoints(pointsToAdd);
        Destroy(gameObject);

     }

}