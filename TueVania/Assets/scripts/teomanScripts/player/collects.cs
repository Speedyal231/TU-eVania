using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collects : MonoBehaviour {

    [SerializeField] GameObject effect;
    public int pointsToAdd;
    public int identifier;

    private PlayerDashScript pd;



    void OnTriggerEnter2D(Collider2D other)
    {
        {
            if (other.GetComponent<CharacterController>() == null)
                return;
        }

        Instantiate(effect, transform.position, Quaternion.identity);

        PlayerShootScript ps = other.GetComponent<PlayerShootScript>();
        if (ps != null) {
            if (identifier == 4) {
                ps.changeGunStatus();
                Debug.Log("gun unlocked");
                Destroy(gameObject);
            } else if (identifier == 5) {
                ps.changeBigGunStatus();
                Debug.Log("big gun unlocked");
                Destroy(gameObject);
            }
        }
        
        PlayerClingScript pc = other.GetComponent<PlayerClingScript>();
        if (pc != null) {
            if (identifier == 6) {
                pc.changeClingStatus();
                Debug.Log("cling unlocked");
                Destroy(gameObject);
            }
        }

        PlayerDashScript pd = other.GetComponent<PlayerDashScript>();
        if (pd != null) {
            if (identifier == 3) {
                pd.changeDashStatus();
                Debug.Log("dash unlocked");
                Destroy(gameObject);
            }
        }

        if (identifier == 0) {
            PlayerScoreManager.AddPoints(pointsToAdd);
            Destroy(gameObject);
        } else if (identifier == 2) {
            healthManager.increaseHealth();
            Destroy(gameObject);
        } else {
            Debug.Log("Bruh");
        }
        //PlayerScoreManager.AddPoints(pointsToAdd);
        //Destroy(gameObject);

     }

}