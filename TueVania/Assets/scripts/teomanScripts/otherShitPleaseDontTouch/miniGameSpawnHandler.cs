using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGameSpawnHandler : MonoBehaviour
{
    public GameObject player;

    private GameObject button1Placement;
    private GameObject button2Placement;
    private GameObject button3Placement;
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindWithTag("Player");
        button1Placement = GameObject.Find("Level1Panel");
        button2Placement = GameObject.Find("Level2Panel");
        button3Placement = GameObject.Find("Level3Panel");
        
        if (BlockManager.blockMiniGameFinished) {
            Debug.Log("It detects the game being finished");
            player.transform.position = button1Placement.transform.position;
            BlockManager.blockMiniGameFinished = false;
        } else if (CheckIntersections.intersectinMiniGameFinished) {
             player.transform.position = new Vector3(-21.39f, 3.69f, 0f);
            CheckIntersections.intersectinMiniGameFinished = false;
        } else if (minigameManager.cableManager) {
            player.transform.position = new Vector3(-90.15f, -7.92f, 0f);
            minigameManager.cableManager = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
