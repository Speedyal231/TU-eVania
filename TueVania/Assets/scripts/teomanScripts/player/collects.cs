using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collects : MonoBehaviour {

    public int pointsToAdd;
    public int identifier;


    void OnTriggerEnter2D(Collider2D other)
    {
        {
            if (other.GetComponent<CharacterController>() == null)
                return;
        }
        
        if (identifier == 0) {

            PlayerScoreManager.AddPoints(pointsToAdd);
            Destroy(gameObject);
        } else if (identifier == 1) {
            //powerup enable here.
            Destroy(gameObject);
        } else if (identifier == 2) {
            //powerup enable here.
            healthManager.increaseHealth();
            Destroy(gameObject);
        } else {
            Debug.Log("Bruh");
        }
        //PlayerScoreManager.AddPoints(pointsToAdd);
        //Destroy(gameObject);

     }

}