using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class finishScore : MonoBehaviour
{
    public static int fScore;
    public TMP_Text textScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fScore != null)
        {
            // Use the instance to access the non-static property
            textScore.text = "Score: " + fScore.ToString();
        }
        else
        {
            Debug.LogWarning("PlayerData script not found!");
        }
    }
}
