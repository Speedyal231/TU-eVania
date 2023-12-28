using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunRotateScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Transform targetTransform;
    [SerializeField] Transform gunTransform;

    void Update()
    {
        if (targetTransform != null)
        {
            // Calculate the direction from the gun to the target
            Vector3 directionToTarget = targetTransform.position - gunTransform.position;

            // Calculate the angle to look at the target using the local up direction of the gun
            float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            // Set the rotation directly without interpolation
            gunTransform.rotation = Quaternion.Euler(0f, 0f, angleToTarget);
        }
    }
}
