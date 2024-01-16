using UnityEngine;

public class hurtPlayer : MonoBehaviour
{
    public int damageToGive;
    


    // Use this for initialization
    void Start()
    {
        // Initialization code if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Update code if needed
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<CharacterController>();
        if (other.CompareTag("Player"))
        {
            if (player.stunned){
                
            } else {

                
                // Use the updated method name HurtPlayer from the PlayerData class
                healthManager.hurtPlayer(damageToGive);
                Debug.Log("Damage Given");


                
                if (other.transform.position.x < transform.position.x)
                {               
                    player.knockFromRight = true;
                }
                else
                {
                    player.knockFromRight = false;
                }

                player.knockBack();
            }
        }
    }
}
