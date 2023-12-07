using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("The door on the other side")]
    [SerializeField] GameObject otherSideDoor;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void WalkThroughDoor(GameObject player)
    {
        Vector2 relative = player.transform.position - this.transform.position;
        Vector2 exit = otherSideDoor.transform.position;
        Vector2 final = relative + exit;
        player.transform.position = final;
    }
}