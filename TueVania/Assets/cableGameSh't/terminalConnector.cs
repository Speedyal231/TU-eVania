using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminalConnector : MonoBehaviour
{
    //private WireScript wireScript;
    //private WireScript wireScript2;
    public endRecieve upperWire;
    public endRecieve lowerWire;
    private bool gateComplete;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (upperWire.getFilled() && lowerWire.getFilled()){
            if (upperWire.getBooleanValueEnd() && lowerWire.getBooleanValueEnd()){
                gateComplete = true;
            }
        } else {
            gateComplete = false;
        }
    }

    public bool getGateComplete(){
        return gateComplete;
    }

    public void setGateComplete(bool newVal){
        gateComplete = newVal;
    }

}
