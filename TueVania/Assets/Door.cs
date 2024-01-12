using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [Header("The door on the other side")]
    [SerializeField] GameObject otherSideDoor;

    // Walk through the door
    public override void Interact(GameObject player)
    {
        Vector2 relative = player.transform.position - this.transform.position;
        Vector2 exit = otherSideDoor.transform.position;
        Vector2 final = relative + exit;
        player.transform.position = final;
    }

}