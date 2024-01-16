using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventScript : Interactable
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject entryClosed;
    [SerializeField] GameObject entryOpen;
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip clip;
    Color baseColor;
    Color flashColor;

    private void Start()
    {
        baseColor = button.GetComponent<SpriteRenderer>().color;
        flashColor = Color.white;
        entryClosed.active = true;
        entryOpen.active = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        button.GetComponent<SpriteRenderer>().color = flashColor;
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
        src.PlayOneShot(clip);
        entryClosed.active = false;
        entryOpen.active = true;
        button.GetComponent<SpriteRenderer>().color = Color.green;
        baseColor = Color.green;
    }
}
