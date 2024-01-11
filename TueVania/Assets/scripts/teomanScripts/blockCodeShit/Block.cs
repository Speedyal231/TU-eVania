using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int index; // Unique index assigned to each block

    void Start()
    {
        // Set the index based on the block's position in the hierarchy
        index = transform.GetSiblingIndex();
    }
}