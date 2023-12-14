using UnityEngine;
using TMPro;

public class IfBlock : MonoBehaviour
{
    public StringVariableBlock inputVariable;
    public TextMeshProUGUI monitorText;

    public void Execute()
    {
        string inputText = inputVariable.GetValue();

        // Your logic here
        if (inputText.Equals("Hello"))
        {
            monitorText.text = "String is 'Hello'!";
        }
        else
        {
            monitorText.text = "String is not 'Hello'!";
        }
    }
}
