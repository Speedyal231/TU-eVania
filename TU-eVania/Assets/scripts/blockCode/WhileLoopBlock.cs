using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WhileLoopBlock : MonoBehaviour
{
    public TextMeshProUGUI monitorText;

    public void Execute()
    {
        int counter = 0;

        while (counter <= 20)
        {
            monitorText.text = "Counter: " + counter;
            counter++;
        }
    }
}
