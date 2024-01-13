using UnityEngine;

public class LogicGate : MonoBehaviour
{
    protected Wire[] inputs;  // Array of input wires
    protected Terminal outputTerminal;  // Output terminal

    // Check if the gate is full (all inputs are connected)
    public virtual bool IsFull()
    {
        foreach (Wire input in inputs)
        {
            if (input == null || !input.IsConnected())
            {
                return false;
            }
        }
        return true;
    }

    // Get the output value of the gate (override this in subclasses for specific logic)
    public virtual bool GetOutput()
    {
        return false;
    }

    // Connect a wire to the gate's input
    public virtual void ConnectWire(Wire wire)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            if (inputs[i] == null)
            {
                inputs[i] = wire;
                return;
            }
        }
    }
}
