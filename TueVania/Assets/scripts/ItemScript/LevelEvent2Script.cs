using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelEvent2Script : Interactable
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject entryClosed;
    [SerializeField] GameObject entryOpen;
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip clip;
    [SerializeField] Sprite red;
    [SerializeField] Sprite green;
    Color baseColor;
    Color flashColor;
    bool pressed;
    VariableManager variableManager;

    private void Start()
    {
        //variableManager = FindObjectOfType<VariableManager>();
        baseColor = button.GetComponent<SpriteRenderer>().color;
        button.GetComponent<SpriteRenderer>().sprite = red;
        flashColor = Color.white;
        entryClosed.active = true;
        entryOpen.active = false;
        if (pressed)
        {
            button.GetComponent<SpriteRenderer>().sprite = green;
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
            //VariableManager.playerPosition = transform.position;
            pressed = true;
            src.PlayOneShot(clip);
            entryClosed.active = false;
            entryOpen.active = true;
            button.GetComponent<SpriteRenderer>().sprite = green;
            baseColor = Color.white;
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
