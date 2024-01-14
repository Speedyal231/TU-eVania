using UnityEngine;

public class WiringGame : MonoBehaviour
{
    public LogicGate[] logicGates;   // Array of logic gate objects (AND, OR, NOT)
    public Wire[] wires;             // Array of wire objects with boolean values (TRUE or FALSE)
    public Terminal[] terminals;     // Array of terminal game objects

    // Update is called once per frame
    void Update()
    {
        // Check for player input (assuming left mouse button for simplicity)
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a wire
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Wire"))
            {
                Wire clickedWire = hit.collider.GetComponent<Wire>();

                // Check if the clicked wire is not already connected
                if (!clickedWire.IsConnected())
                {
                    // Find a logic gate that is not full (you can customize this logic)
                    LogicGate availableGate = FindAvailableGate();

                    if (availableGate != null)
                    {
                        // Connect the wire to the logic gate
                        availableGate.ConnectWire(clickedWire);

                        // Connect the logic gate output to the terminal
                        clickedWire.ConnectTerminal(availableGate.GetOutput());

                        Debug.Log("Wire connected to " + availableGate.GetType().Name);
                    }
                    else
                    {
                        Debug.Log("All logic gates are full! Cannot connect the wire.");
                    }
                }
                else
                {
                    Debug.Log("This wire is already connected!");
                }
            }
        }
    }

    // Find an available logic gate that is not full (you can customize this logic)
    LogicGate FindAvailableGate()
    {
        foreach (LogicGate gate in logicGates)
        {
            if (!gate.IsFull())
            {
                return gate;
            }
        }
        return null;
    }

}
