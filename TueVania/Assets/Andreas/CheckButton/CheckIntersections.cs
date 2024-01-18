using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CheckIntersections : MonoBehaviour
{
    [Header("section declaration")]
    [SerializeField] GameObject sectionA;
    [SerializeField] GameObject sectionAB;
    [SerializeField] GameObject sectionABC;
    [SerializeField] GameObject sectionAC;
    [SerializeField] GameObject sectionB;
    [SerializeField] GameObject sectionBC;
    [SerializeField] GameObject sectionC;

    [SerializeField] RandomStringButton randomStringButton;
    [SerializeField] ObjectArrays objectArrays;

    GameObject[] sections;
    Color[] colors;
    bool userCorrect;
    bool checkDone;
    public TextMeshProUGUI checkString;
    int completions;
    public static bool intersectinMiniGameFinished;

    private void Start()
    {
        intersectinMiniGameFinished = false;
        if (objectArrays == null || checkString == null || randomStringButton == null)
        {
            Debug.LogError("References not set properly. Make sure to assign ObjectArrays and buttonText in the Inspector.");
            return;
        }
        completions = 0;
    }

    public void OnButtonPress()
    {
        checkDone = false;
        userCorrect = true;
        getSectionColors();
        checkColors();
        if (userCorrect && checkDone)
        {
            checkString.text = "Correct!";
            completions++;
        } else
        {
            checkString.text = "Incorrect"; 
        }
        if (completions == 3) 
        {
            intersectinMiniGameFinished = true;
            checkString.text = "COMPLETED";
            SceneManager.LoadScene("AtlasLevel3");
        }
    }


    void getSectionColors()
    {
        // Initialize the array with the desired GameObjects
        colors = new Color[7];
        sections = new GameObject[] { sectionA, sectionAB, sectionABC, sectionAC, sectionB, sectionBC, sectionC };
        

        // Example: Accessing and using the GameObjects in the array
        for (int i = 0; i < 7; i++)
        {
            Color curcol = sections[i].GetComponent<SpriteColorChanger>().returnColor();
            colors[i] = curcol;
        }
    }

    private void checkColors()
    {
        string currentString = randomStringButton.returnString();
        for (int i = 0; i < objectArrays.sectionStrings.Length; i++)
        {
            if (objectArrays.sectionStrings[i].Equals(currentString))
            {
                for (int j = 0; j < 7; j++)
                {
                    if (colors[j] == objectArrays.correctColors[i][j])
                    {
                        
                    } else
                    {
                        userCorrect = false;
                    }
                }
                checkDone = true;
                getCheckDone();
            }
        }
    }

    public void getCheckDone()
    {
        randomStringButton.checkDone = checkDone;
        randomStringButton.currGuess = userCorrect;
    }

}
