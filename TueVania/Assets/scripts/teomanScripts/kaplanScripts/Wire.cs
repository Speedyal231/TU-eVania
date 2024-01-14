using UnityEngine;

public class Wire : MonoBehaviour
{
    public bool value; // Boolean value of the wire
    public Terminal connectedTerminal; // Reference to the connected terminal

    // Method to check if the wire is connected
    public bool IsConnected()
    {
        return connectedTerminal != null;
    }

    // Method to get the boolean value of the wire
    public bool GetValue()
    {
        return value;
    }

    // Method to connect the wire to a terminal
    public void ConnectToTerminal(Terminal terminal)
    {
        connectedTerminal = terminal;
    }

    public void ConnectTerminal(bool terminalValue)
    {
        if (connectedTerminal != null)
        {
            connectedTerminal.SetValue(terminalValue);
        }
    }
}
