using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{
    public Transform EndWire;
    public LineRenderer Line;
    bool Dragging = false;
    Vector3 OriginalPosition;
    Vector3 positionDifference;
    bool Connected = false;
    static List<WireScript> connectedWires = new List<WireScript>();

    void Start()
    {
        OriginalPosition = transform.position;
    }

    void Update()
    {
        if (Dragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            convertedMousePosition.z = 0;
            SetPosition(convertedMousePosition);

            Vector3 endWireDifference = convertedMousePosition - EndWire.position;
            if (endWireDifference.magnitude < .5f)
            {
                SetPosition(EndWire.position);
                Dragging = false;
                Connected = true;
                connectedWires.Add(this);
            }
        }
        else if (Connected && Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            convertedMousePosition.z = 0;
            if (Vector3.Distance(convertedMousePosition, transform.position) < .5f)
            {
                Dragging = true;
            }
        }
    }

    void SetPosition(Vector3 pPosition)
    {
        transform.position = pPosition;

        positionDifference = pPosition - Line.transform.position;
        Line.SetPosition(2, positionDifference - new Vector3(.5f, 0, 0));
        Line.SetPosition(3, positionDifference - new Vector3(.15f, 0, 0));
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.name == "And") 
        {
            Connected = true;
            Dragging = false;
            connectedWires.Add(this);
        }
    }

    void OnMouseDown()
    {
        if (!Connected)
        {
            Dragging = true;
            connectedWires.Clear();
            connectedWires.Add(this);
        }
        else if (Connected && !connectedWires.Contains(this))
        {
            Dragging = false;
        }
    }

    void OnMouseUp()
    {
        Dragging = false;

        if (!Connected)
        {
            // SetPosition(OriginalPosition);
        }
    }
}
