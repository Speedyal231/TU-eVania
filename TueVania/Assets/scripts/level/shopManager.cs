using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class shopManager : MonoBehaviour
{

    public int[,] shopItems = new int[4,4];
    public int score; 
    public TMP_Text scoreText;
    public PlayerModifiers playerModifiers;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Coins: " + score.ToString();
        
        shopItems[1,1] = 1;
        shopItems[1,2] = 2;
        shopItems[1,3] = 3;

        shopItems[2,1] = 10;
        shopItems[2,2] = 20;
        shopItems[2,3] = 30;

        shopItems[3,1] = 0;
        shopItems[3,2] = 0;
        shopItems[3,3] = 0;
    }

    // Update is called once per frame
    public void Buy()
    {
        GameObject button = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        if (score >= shopItems[2, button.GetComponent<buttonInfo>().itemID]) {
            score -= shopItems[2, button.GetComponent<buttonInfo>().itemID];
            shopItems[3, button.GetComponent<buttonInfo>().itemID]++;
            scoreText.text = "Coins: " + score.ToString();
            button.GetComponent<buttonInfo>().quantityText.text = shopItems[3, button.GetComponent<buttonInfo>().itemID].ToString();
        }

        ApplyItemEffect(button.GetComponent<buttonInfo>().itemID);
    }

    void ApplyItemEffect(int itemID)
    {
        switch (itemID)
        {
            case 1:
                playerModifiers.IncreaseSpeed();
                break;
            case 2:
                playerModifiers.RestoreHealth();
                break;
            case 3:
                playerModifiers.IncreaseMaxHealth();
                break;
            // Add more cases for additional items
            default:
                break;
        }
    }
}
