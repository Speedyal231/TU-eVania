using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class BlockManager : MonoBehaviour
{
    public List<Transform> set1Blocks;
    public List<Transform> set2Blocks;
    public List<Transform> set3Blocks;

    public List<Transform> correctOrderSet1;
    public List<Transform> correctOrderSet2;
    public List<Transform> correctOrderSet3;

    public TMP_Text instructionsText;
    public TMP_Text feedbackText;

    private List<Transform> currentBlocks;
    private List<Transform> currentCorrectOrder;

    private int currentSetIndex = 0;

    void Start()
    {
        SetCurrentSet(1); // Start with set 1
    }

    void SetCurrentSet(int setIndex)
    {
        currentSetIndex = setIndex;

        switch (currentSetIndex)
        {
            case 1:
                currentBlocks = set1Blocks;
                currentCorrectOrder = correctOrderSet1;
                instructionsText.text = "Arrange the blocks so that it prints out Hello World";
                HideSet(2); // Ensure set 2 is not visible
                HideSet(3); // Ensure set 3 is not visible
                break;
            case 2:
                currentBlocks = set2Blocks;
                currentCorrectOrder = correctOrderSet2;
                instructionsText.text = "Arrange the blocks so that it prints numbers greater than 5";
                HideSet(1); // Ensure set 1 is not visible
                HideSet(3); // Ensure set 3 is not visible
                ShowSet(2);
                break;
            case 3:
                currentBlocks = set3Blocks;
                currentCorrectOrder = correctOrderSet3;
                instructionsText.text = "Arrange the blocks so that it keeps on printing numbers less than 10";
                HideSet(1); // Ensure set 1 is not visible
                HideSet(2); // Ensure set 2 is not visible
                ShowSet(3);
                break;
        }

        UpdateBlockPositions();
        feedbackText.text = "";
    }


    // Example debug logs in BlockManager.cs
    void UpdateBlockPositions()
    {
        // Log block positions before rearranging
        foreach (Transform block in currentBlocks)
        {
            Debug.Log($"Before: Block {block.gameObject.name}: Y = {block.position.y}");
        }

        // Arrange the current set of blocks based on the shuffled order
        for (int i = 0; i < currentBlocks.Count; i++)
        {
            currentBlocks[i].position = new Vector3(i, 0, 0);
        }

        // Log block positions after rearranging
        foreach (Transform block in currentBlocks)
        {
            Debug.Log($"After: Block {block.gameObject.name}: Y = {block.position.y}");
        }
    }


    void HideSet(int setIndex)
    {
        // Set the visibility of the specified set of blocks
        switch (setIndex)
        {
            case 1:
                SetBlocksVisibility(set1Blocks, false);
                break;
            case 2:
                SetBlocksVisibility(set2Blocks, false);
                break;
            case 3:
                SetBlocksVisibility(set3Blocks, false);
                break;
        }
    }

    void ShowSet(int setIndex)
    {
        switch (setIndex)
        {
            case 1:
                SetBlocksVisibility(set1Blocks, true);
                break;
            case 2:
                SetBlocksVisibility(set2Blocks, true);
                break;
            case 3:
                SetBlocksVisibility(set3Blocks, true);
                break;
        }
    }


    void SetBlocksVisibility(List<Transform> blocks, bool isVisible)
    {
        // Set the visibility of the specified set of blocks
        foreach (Transform block in blocks)
        {
            block.gameObject.SetActive(isVisible);
        }
    }

    public bool CheckOrder()
    {
        Debug.Log("Checking order...");

        // Log current block positions
        foreach (Transform block in currentBlocks)
        {
            Debug.Log($"Block {block.gameObject.name}: Y = {block.position.y}");
        }

        // Log correct order positions
        foreach (Transform correctBlock in currentCorrectOrder)
        {
            Debug.Log($"Expected Y = {correctBlock.position.y}");
        }


        // Get the sorted indexes of current blocks based on Y positions
        List<int> sortedIndexes = currentBlocks
            .OrderBy(block => block.position.y)
            .Select(block => block.GetComponent<Block>().index)
            .ToList();

        
        
       
        // Check if the current order matches the correct order based on assigned indices
        if (currentSetIndex == 1){ 
            int corrects = 0;
            for (int i = 0; i < sortedIndexes.Count; i++)
            {
               
                float tolerance = 0.01f; // Adjust this value based on your specific requirements

                if (Mathf.Abs(currentBlocks[i].position.y - currentCorrectOrder[i].position.y) < tolerance)
                {
                    corrects += 1;
                }
                Debug.Log("Corrects:" + corrects);
                Debug.Log($"{Mathf.Floor(currentBlocks[i].position.y) } == {Mathf.Floor(correctOrderSet1[i].position.y)}");
                
            }
            if (corrects == 4) {
                    SetCurrentSet(2);
                    feedbackText.text = "True"; // Correct order
                    return true;
                }
            return false;
        } else if (currentSetIndex == 2){ 
            int corrects = 0;
            for (int i = 0; i < sortedIndexes.Count; i++)
            {
                // Use a tolerance value for comparing Y position
                /*
                float tolerance = 0.001f;

                if (Mathf.Abs(sortedIndexes[i] - i) > tolerance)
                {
                    feedbackText.text = "False"; // Incorrect order
                    return false;
                } */

                if (Mathf.Floor(currentBlocks[i].position.y) == Mathf.Floor(currentCorrectOrder[i].position.y)) {
                    corrects += 1;
                }
                Debug.Log("Corrects:" + corrects);
                Debug.Log($"{Mathf.Floor(currentBlocks[i].position.y) } == {Mathf.Floor(correctOrderSet2[i].position.y)}");
                if (corrects == 5) {
                    SetCurrentSet(3);
                    feedbackText.text = "True"; // Correct order
                    return true;
                }
            }
            return false;
        } else if (currentSetIndex == 3){ 
            int corrects = 0;
            for (int i = 0; i < sortedIndexes.Count; i++)
            {
                // Use a tolerance value for comparing Y position
                /*
                float tolerance = 0.001f;

                if (Mathf.Abs(sortedIndexes[i] - i) > tolerance)
                {
                    feedbackText.text = "False"; // Incorrect order
                    return false;
                } */

                if (Mathf.Floor(currentBlocks[i].position.y) == Mathf.Floor(currentCorrectOrder[i].position.y)) {
                    corrects += 1;
                }
                Debug.Log("Corrects:" + corrects);
                Debug.Log($"{Mathf.Floor(currentBlocks[i].position.y) } == {Mathf.Floor(correctOrderSet3[i].position.y)}");
                if (corrects == 5) {
                    Debug.Log("Bitti amk");
                    feedbackText.text = "True"; // Correct order
                    return true;
                }
            }
            return false;
        } else {return false;}
    }





    public List<Transform> GetBlockOrder()
    {
        List<Transform> blockTransforms = new List<Transform>();

        foreach (Transform block in currentBlocks)
        {
            blockTransforms.Add(block);
        }

        blockTransforms.Sort((a, b) => b.position.y.CompareTo(a.position.y));

        return blockTransforms;
    }
}
