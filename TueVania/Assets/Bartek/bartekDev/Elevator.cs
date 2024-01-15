using Ink.Parsed;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;

public class Elevator : Interactable
{
    private int floorNum = 0;
    bool active;

    [SerializeField]
    private TextMeshProUGUI floorText;

    [SerializeField]
    private GameObject prompt;

    private const int MAX_FLOOR_NUM = 4;

    public void Start()
    {
        floorText.text = "Floor " + floorNum;
        prompt.SetActive(false);
    }

    public void setFloor(int num)
    {
        this.floorNum = num;
    }

    public override void Interact(GameObject player)
    {
        if (active)
        {
            SceneManager.LoadScene(getSceneName(floorNum));
        }
    }

    private void Update()
    {
        UnityEngine.Debug.Log(active);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        prompt.SetActive(true);
        if (!active)
        {
            floorText.fontSize = 0.3f;
            floorText.text = "Activate elevator switch.";
        } else
        {
            floorText.fontSize = 0.5f;
            floorText.text = "Floor " + floorNum;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        prompt.SetActive(false);
    }

    public override void ExtraInteract(GameObject player)
    {
        if (active)
        {
            if (floorNum >= MAX_FLOOR_NUM)
            {
                floorNum = 0;
            }
            else
            {
                floorNum++;
            }

            floorText.text = "Floor " + floorNum;

            // debug
            UnityEngine.Debug.Log("floorNum = " + floorNum);
        }
    }


    private string getSceneName(int num)
    {
        switch (num)
        {
            case 0: return "BaseScene";
            case 1: return "ElevatorTest1";
            case 2: return "ElevatorTest2";
            default: return "BaseScene";
        }

    }

    public void SetActive(bool active)
    {
        this.active = active;
    }
}
