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
        SceneManager.LoadScene(getSceneName(floorNum));

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        prompt.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        prompt.SetActive(false);
    }

    public override void ExtraInteract(GameObject player)
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
}
