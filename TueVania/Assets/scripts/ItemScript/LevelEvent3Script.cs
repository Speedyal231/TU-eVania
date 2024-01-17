using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEvent3Script : Interactable
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject entryClosed;
    [SerializeField] GameObject entryOpen;
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip clip;
    Color baseColor;
    Color flashColor;
    bool pressed;

    private void Start()
    {
        baseColor = button.GetComponent<SpriteRenderer>().color;
        flashColor = Color.white;
        entryClosed.active = true;
        entryOpen.active = false;
        if (pressed)
        {
            baseColor = Color.green;
            button.GetComponent<SpriteRenderer>().color = baseColor;
            entryClosed.active = false;
            entryOpen.active = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            button.GetComponent<SpriteRenderer>().color = flashColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        button.GetComponent<SpriteRenderer>().color = baseColor;
    }

    public override void ExtraInteract(GameObject player)
    {
        //nothing happens
    }

    public override void Interact(GameObject player)
    {
        if (!pressed) 
        {
            pressed = true;
            src.PlayOneShot(clip);
            entryClosed.active = false;
            entryOpen.active = true;
            baseColor = Color.green;
        }
    }

    public bool getInteracted()
    {
        return pressed;
    }

    public void SetPressed()
    {
        pressed = true;
    }
}
