using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    [SerializeField] private float interactionRange = 10f;
    bool interactedWithDoor;
    float currentAnimTime;

    private void Awake()
    {
        //enable player input script.
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        GetComponent<CharacterController>().enabled = true;
    }

    void Update()
    {
        if (!DialogueManager.instance.DialogueIsPlaying)
        {
            LookForInteraction();
        }
        Count();

        
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
                   if (collider.CompareTag("Door"))
                   { 
                        interactedWithDoor = true;
                        currentAnimTime = 3;
                        EnterDoor(interactable);
                   } 
                   else
                   {
                        interactable.Interact(this.gameObject);
                   }
                    
                           
                }
                if (playerInputActions.Keyboard.ExtraInteract.WasPressedThisFrame())
                {
                    interactable.ExtraInteract(this.gameObject);

                }
            }
        }
    }

    public bool DoorEnterCheck()
    {
        return interactedWithDoor;
    }

    private void EnterDoor(Interactable interactable)
    {
        while(currentAnimTime <= 0)
        {
            //wait
        }
        interactedWithDoor = false;
        interactable.Interact(this.gameObject);
    }

    private void Count()
    {
        if (currentAnimTime > 0)
            currentAnimTime -= Time.fixedDeltaTime;
    }
}
