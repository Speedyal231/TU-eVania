using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollowScript : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] Transform targetTransform;
    // Update is called once per frame
    private void FixedUpdate()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Update the object's position to follow the mouse
        targetTransform.position = new Vector2(mousePosition.x, mousePosition.y);
    }
}
