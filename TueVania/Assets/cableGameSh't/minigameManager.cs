using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class minigameManager : MonoBehaviour
{

    public terminalConnector upperGate;
    public terminalConnector lowerGate;
    public GameObject camera;

    /*
    public WireScript EndWire;
    public WireScript EndWire2;
    public WireScript EndWire3;
    public WireScript EndWire4;*/

    public Vector3 targetTransform;

    public GameObject endCanvas;
    public static bool cableManager;


    // Start is called before the first frame update
    void Start()
    {
        endCanvas.SetActive(false);
        cableManager = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (upperGate.getGateComplete() && lowerGate.getGateComplete()){
                endCanvas.SetActive(true);
                cableManager = true;
                SceneManager.LoadScene("AtlasLevel1");                
        }
    }    
}
