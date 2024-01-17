using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class splashScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(CheckOrder);
    }

    void CheckOrder()
    {
        SceneManager.LoadScene("AtlasLevel1");
    }
}
