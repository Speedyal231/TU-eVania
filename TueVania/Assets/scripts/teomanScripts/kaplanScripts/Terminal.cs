using UnityEngine;

public class Terminal : MonoBehaviour
{
    public LogicGate connectedGate; // Reference to the connected logic gate

    // Method to connect the terminal to a logic gate
    public void ConnectToGate(LogicGate gate)
    {
        connectedGate = gate;
    }

    // Inside the Terminal class
    public void SetValue(bool value)
    {
        // Implement logic to handle the terminal value (e.g., display it, trigger events, etc.)
        Debug.Log("Terminal value set to: " + value);
    }

}
