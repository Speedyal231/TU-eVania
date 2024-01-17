using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{
    //public List<Transform> TerminalLists;
    public Transform EndWire;
    public Transform EndWire2;
    public Transform EndWire3;
    public Transform EndWire4;
    public LineRenderer Line;
    bool Dragging = false;
    bool isFull;
    public bool booleanValue; 
    Vector3 OriginalPosition;
    Vector3 positionDifference;
    List<bool> Connected; // List to keep track of connected states for each end wire
    static List<WireScript> connectedWires = new List<WireScript>();

    void Start()
    {
        OriginalPosition = transform.position;
        InitializeConnections();
    }

    void InitializeConnections()
    {
        Connected = new List<bool> { false, false, false, false }; 
    }

    void Update()
    {
        if (Dragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            convertedMousePosition.z = 0;
            SetPosition(convertedMousePosition);

            // Check each end wire for proximity
            for (int i = 0; i < Connected.Count; i++)
            {
                Transform currentEndWire;
                //Transform currentEndWire = (i == 0) ? EndWire : EndWire2;
                if(i == 0)
                {
                    currentEndWire = EndWire;
                } 
                else if(i == 1)
                {
                    currentEndWire = EndWire2;
                }
                else if(i == 2)
                {
                    currentEndWire = EndWire3;
                }
                else
                {
                    currentEndWire = EndWire4;
                }
                Vector3 endWireDifference = convertedMousePosition - currentEndWire.position;

                if (!Connected[i] && endWireDifference.magnitude < 0.5f)
                {
                    isFull = true;
                    SetPosition(currentEndWire.position);
                    Dragging = false;
                    Connected[i] = true;
                    connectedWires.Add(this);
                    //LogConnection(true, i); // Log the connection
                    break; // Break the loop after the first connection
                }
                else if (Connected[i] && endWireDifference.magnitude >= 0.5f)
                {
                    Connected[i] = false;
                    connectedWires.Remove(this);
                    //LogConnection(false, i); // Log the disconnection
                    break; // Break the loop after the first disconnection
                }
            }
        }
        else if (Connected.Contains(false) && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            convertedMousePosition.z = 0;

            // Check if the mouse is close to any wire's position for initiating dragging
            if (Vector3.Distance(convertedMousePosition, transform.position) < 0.7f)
            {
                Dragging = true;
            }
        }
    }

    void SetPosition(Vector3 pPosition)
    {
        transform.position = pPosition;

        positionDifference = pPosition - Line.transform.position;
        Line.SetPosition(2, positionDifference - new Vector3(0.5f, 0, 0));
        Line.SetPosition(3, positionDifference - new Vector3(0.15f, 0, 0));
    }

//     void LogConnection(bool isConnected, int inputIndex)
// {
//     string connectionStatus = isConnected ? "connected" : "disconnected";
//     string inputName = (inputIndex == 0) ? "EndWire" : "EndWire2";
//     Debug.Log($"Wire {inputName} {connectionStatus}");

//     // Check if both wires are connected
//     if (connectedWires.Count == 2)
//     {
//         Debug.Log($"Wire 1 connected to {connectedWires[0].gameObject.name}");
//         Debug.Log($"Wire 2 connected to {connectedWires[1].gameObject.name}");
//     }
// }


    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.name == "Input1AND") 
        {
            //
        }
    }

    void OnMouseDown()
    {
        if (Connected.Contains(false))
        {
            // If any end wire is not connected, initiate dragging
            Dragging = true;
            connectedWires.Clear(); 
            connectedWires.Add(this);
        }
    }

    void OnMouseUp()
    {
        Dragging = false;

        if (Connected.Contains(false))
        {
            //SetPosition(OriginalPosition);
        }
    }

    void makeFull(){
        isFull = true;
    }

    public bool getBooleanValue(){
        return booleanValue;
    }

    public void setBooleanValue(bool newVal){
        booleanValue = newVal;
    }

    public bool getDrag(){
        return Dragging;
    }

    public void setDrag(bool newVal){
        Dragging = newVal;
    }
}
