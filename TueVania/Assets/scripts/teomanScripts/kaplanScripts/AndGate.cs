using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AndGate : LogicGate
{
    public TMP_Text outputText; // Text component to display the gate's output (optional)

    void Start()
    {
        // Initialize the number of inputs for the AND gate (you can customize this)
        inputs = new Wire[2];
    }

    // Override the GetOutput method for the AND gate logic
    public override bool GetOutput()
    {
        // AND gate logic: Output is true only if all inputs are true
        foreach (Wire input in inputs)
        {
            if (input == null || !input.GetValue())
            {
                return false;
            }
        }
        return true;
    }

    // Override the IsFull method to update the output text (optional)
    public override bool IsFull()
    {
        bool isFull = base.IsFull();

        if (outputText != null)
        {
            outputText.text = "Output: " + GetOutput().ToString();
        }

        return isFull;
    }
}
