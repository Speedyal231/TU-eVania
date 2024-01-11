// OrderCheckButton.cs
using UnityEngine;
using UnityEngine.UI;

public class OrderCheckButton : MonoBehaviour
{
    public BlockManager blockManager;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(CheckOrder);
    }

    void CheckOrder()
    {
        if (blockManager.CheckOrder())
        {
            Debug.Log("Correct Order!");
        }
        else
        {
            Debug.Log("Incorrect Order!");
        }
    }
}
