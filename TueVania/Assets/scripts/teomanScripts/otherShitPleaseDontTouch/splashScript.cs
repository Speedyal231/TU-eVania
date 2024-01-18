using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class splashScript : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(CheckOrder);
    }

    void CheckOrder()
    {
        if (index == 0) {
            SceneManager.LoadScene("backstory");
        } else if (index == 1) {
            SceneManager.LoadScene("AtlasGroundLevel");
        } else {
            Debug.Log("brüh");
        }
        
    }
}
