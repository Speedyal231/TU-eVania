using UnityEngine;

public class TeleportOnKeyPress : MonoBehaviour
{
    public string playerTag = "Player";
    public string doorTag = "Door";
    KeyCode teleportKey = KeyCode.W;
    public Vector3 teleportDestination;

    private void OnTriggerEnter2D(Collider2D  other)
    {
        Debug.Log("Trigger entered!");
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Door Deteced");
            if (Input.GetKeyDown(teleportKey))
            {
                Debug.Log("Teleported");
                GameObject player = GameObject.FindGameObjectWithTag(playerTag);
                if (player != null)
                {
                    player.transform.position = teleportDestination;
                }
            }
        }
    }
}
