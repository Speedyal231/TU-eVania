using UnityEngine;
using UnityEngine.UI;

public class PrintBlock : MonoBehaviour
{
    public Text textToPrint;

    public void Execute()
    {
        Debug.Log(textToPrint.text);
    }
}
