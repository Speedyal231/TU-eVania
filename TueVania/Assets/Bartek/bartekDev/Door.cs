using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [Header("The door on the other side")]
    [SerializeField] GameObject otherSideDoor;
    [SerializeField] AnimationControlScript animation;

    const string open = "DoorOpening";
    const string close = "Doorclosing";

    // Walk through the door
    public override void Interact(GameObject player)
    {
        Vector2 relative = player.transform.position - this.transform.position;
        Vector2 exit = otherSideDoor.transform.position;
        Vector2 final = relative + exit;
        player.transform.position = final;
    }

    public override void ExtraInteract(GameObject player)
    {
        // empty on purpose
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            animation.ChangeAnimationState(open);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animation.ChangeAnimationState(close);
        }
    }

}