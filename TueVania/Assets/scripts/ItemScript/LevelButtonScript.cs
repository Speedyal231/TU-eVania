using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelButtonScript : Interactable
{
    [SerializeField] GameObject button;
    [SerializeField] Elevator elevator;
    Color baseColor;
    Color flashColor;

    private void Start()
    {
        baseColor = button.GetComponent<SpriteRenderer>().color;
        flashColor = Color.white;
        elevator.SetActive(false);
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
        elevator.SetActive(true);
        button.GetComponent<SpriteRenderer>().color = Color.green;
        baseColor = Color.green;
    }
}
