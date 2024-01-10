using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    [SerializeField] private float interactionRange = 10f;

    private void Awake()
    {
        //enable player input script.
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    void Update()
    {
        if (!DialogueManager.instance.DialogueIsPlaying)
        {
            LookForInteraction();
        }
        
    }

    void LookForInteraction()
    {
        Vector2 boxSize = new Vector2(interactionRange, interactionRange);
        Collider2D[] collidersInVicinity = Physics2D.OverlapBoxAll(this.transform.position, boxSize, 0);

        foreach (Collider2D collider in collidersInVicinity)
        {
            // check if detected object has a dialogue script 
            if (collider.gameObject.TryGetComponent(out DialogueInteractable dialogueInteractable) )
            {
                // if there is a dialogue script, prompt the user to enter dialogue
                dialogueInteractable.TriggerVisualCue(this.gameObject);

                if (playerInputActions.Keyboard.Interact.WasPressedThisFrame() && !DialogueManager.instance.DialogueIsPlaying)
                {
                    dialogueInteractable.EnterDialogue(this.gameObject);
                }
            }

            if (collider.gameObject.TryGetComponent(out Interactable interactable))
            {
                if (playerInputActions.Keyboard.Interact.WasPressedThisFrame())
                {
                    interactable.Interact(this.gameObject);
                }
            }
        }
    }

   
}
