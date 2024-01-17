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
        "A U B",
        "(A N B) / C",
        "A / (B U C)",
        "A U C",
        "C"
        // Add more strings as needed
    };

    public Color[][] correctColors;
    public string[] sectionStrings;

    private void Start()
    {
        sectionStrings = new string[] {
            "(A U B) / C",
            "(A U C) / B",
            "A U B",
            "(A N B) / C",
            "A / (B U C)",
            "A U C",
            "C"
        };
        correctColors = new Color[][] {
            AorBUnionC,
            AorCUnionB,
            AorB,
            AintersectBnoC,
            AnoCwithb,
            Aorc,
            C
        };
    }


    // Public color array for Combination1
    Color[] AorBUnionC = { Color.green, Color.green, Color.white, Color.white, Color.green, Color.white, Color.white };
    Color[] AorCUnionB = { Color.green, Color.white, Color.white, Color.green, Color.white, Color.white, Color.green };
    Color[] AorB = { Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.white };
    Color[] AintersectBnoC = { Color.white, Color.green, Color.white, Color.white, Color.white, Color.white, Color.white };
    Color[] AnoCwithb = { Color.green, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white };
    Color[] Aorc = { Color.green, Color.green, Color.green, Color.green, Color.white, Color.green, Color.green };
    Color[] C = { Color.white, Color.white, Color.green, Color.green, Color.white, Color.green, Color.green };

}