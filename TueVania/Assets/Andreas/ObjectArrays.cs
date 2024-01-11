using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectArrays : MonoBehaviour
{
    // Public static HashSet<string> accessible from any script
    public HashSet<string> stringSet = new HashSet<string>
    {
        "(A U B) / C",
        "(A U C) / B",
        // Add more strings as needed
    };

    public Color[][] correctColors;
    public string[] sectionStrings;

    private void Start()
    {
        sectionStrings = new string[] {"(A U B) / C", "(A U C) / B"};
        correctColors = new Color[][] {
            AorBUnionC,
            AorCUnionB,
        };
    }


    // Public color array for Combination1
    Color[] AorBUnionC = { Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green };
    Color[] AorCUnionB = { Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white };

}