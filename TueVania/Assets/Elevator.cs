using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : Interactable
{
    [SerializeField]
    private int floorNum;

    public void setFloor(int num)
    {
        this.floorNum = num;
    }

    public override void Interact(GameObject player)
    {
        SceneManager.LoadScene(getSceneName(floorNum));
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
