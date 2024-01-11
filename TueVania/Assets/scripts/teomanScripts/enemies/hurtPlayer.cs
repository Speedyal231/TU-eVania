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
        if (other.CompareTag("Player"))
        {
            // Use the updated method name HurtPlayer from the PlayerData class
            PlayerHealthManager.HurtPlayer(damageToGive);
            Debug.Log("Damage Given");

            var player = other.GetComponent<tempPlayerController>();
            player.knockbackCount = player.knockbackLength;

            if (other.transform.position.x < transform.position.x)
            {
                player.knockFromRight = true;
            }
            else
            {
                player.knockFromRight = false;
            }
        }
    }
}
