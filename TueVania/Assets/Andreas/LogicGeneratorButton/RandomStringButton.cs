using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RandomStringButton : MonoBehaviour
{
    public ObjectArrays objectArrays; // Reference to the class containing stringSet
    public TextMeshProUGUI LogicalProposition; // Reference to the TextMeshPro component on the button
    private string currentSectionString;
    private List<int> usedIndeces = new List<int>();
    int lastCheck;
    public bool checkDone;
    public bool currGuess;

    void Start()
    {
        // Make sure the references are set in the Inspector
        lastCheck = 0;
        if (objectArrays == null || LogicalProposition == null)
        {
            Debug.LogError("References not set properly. Make sure to assign ObjectArrays and buttonText in the Inspector.");
            return;
        }
    }

    public void OnButtonPress()
    {
        
        // Update the text on the button when it is pressed
        if (checkDone || LogicalProposition.text.Equals("Click Here To Start"))
        {
            UpdateButtonText();
            currentSectionString = LogicalProposition.text;
        }
        
    }

    private void UpdateButtonText()
    {
        // Get a random string from the stringSet
        string randomString = GetRandomString();

        // Update the TextMeshPro text with the random string
        LogicalProposition.text = randomString;
    }

    private string GetRandomString()
    {
        // Check if the stringSet is not null and has at least one element
        if (objectArrays.stringSet != null && objectArrays.stringSet.Count > 0)
        {
            // Convert the HashSet<string> to a list for easy indexing
            List<string> stringList = new List<string>(objectArrays.stringSet);

            // Get a random index within the range of the stringSet
            int randomIndex = 0;
            if (objectArrays.stringSet.Count != usedIndeces.Count) {
                do
                {
                    randomIndex = Random.Range(0, stringList.Count);
                    
                } while (usedIndeces.Contains(randomIndex) && lastCheck == randomIndex);
                if (currGuess && checkDone)
                {
                    usedIndeces.Add(randomIndex);
                }
            }
            lastCheck = randomIndex;

            // Return the random string from the stringSet
            return stringList[randomIndex];
        }
        else
        {
            // If stringSet is null or empty, return a default message
            return "No strings available";
        }
    }
    public string returnString()
    {
        return currentSectionString;
    }
}