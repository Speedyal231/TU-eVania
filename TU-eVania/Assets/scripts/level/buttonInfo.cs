using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buttonInfo : MonoBehaviour
{
    public int itemID;
    public TMP_Text priceText;
    public TMP_Text quantityText;
    public GameObject shopManager;

    // Update is called once per frame
    void Update()
    {
        priceText.text = "Price: " + shopManager.GetComponent<shopManager>().shopItems[2, itemID].ToString();
        quantityText.text = shopManager.GetComponent<shopManager>().shopItems[3, itemID].ToString();
    }
}
