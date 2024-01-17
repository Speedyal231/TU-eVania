using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelButtonScript : Interactable
{
    [SerializeField] GameObject button;
    [SerializeField] Elevator elevator;
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip clip;
    Color baseColor;
    Color flashColor;

    public bool pressed;

    private void Start()
    {
        baseColor = button.GetComponent<SpriteRenderer>().color;
        flashColor = Color.white;
        if (!elevator.active)
        {
            elevator.SetActive(false);
        } else
        {
            button.GetComponent<SpriteRenderer>().color = Color.green;
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
        if (!elevator.active)
        {
            pressed = true;
            elevator.SetActive(true);
            src.PlayOneShot(clip);
            button.GetComponent<SpriteRenderer>().color = Color.green;
            baseColor = Color.green;
        }
        
    }
}
