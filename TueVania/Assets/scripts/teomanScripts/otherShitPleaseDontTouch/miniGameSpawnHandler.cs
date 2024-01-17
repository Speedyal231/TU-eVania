using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGameSpawnHandler : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BlockManager.blockMiniGameFinished) {
            player.transform.position = new Vector3(-21.39f, 3.69f, 0f);
            BlockManager.blockMiniGameFinished = false;
        } 
        if (CheckIntersections.intersectinMiniGameFinished) {
            player.transform.position = new Vector3(-90.15f, -7.92f, 0f);
            CheckIntersections.intersectinMiniGameFinished = false;
        }
        if (minigameManager.cableManager) {
            player.transform.position = new Vector3(12.12376f, 54.10646f, 0f);
            minigameManager.cableManager = false;
        }
    }
}
