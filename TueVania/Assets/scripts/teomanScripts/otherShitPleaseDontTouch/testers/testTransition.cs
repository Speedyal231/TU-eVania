using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class testTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("touched");

        var temp = other.GetComponent<WireScript>();
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("BlockCodeGame");
        }
    }


    
}
