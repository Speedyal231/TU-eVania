using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundupScrollScript : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform backgroundTransform;
    [SerializeField] Transform centreTransform;
    [SerializeField] float upScrollMultiplier;

    private void FixedUpdate()
    {
        float camYOffset = cameraTransform.position.y - centreTransform.position.y;
        backgroundTransform.position = new Vector3(centreTransform.position.x, centreTransform.position.y + camYOffset * upScrollMultiplier, 0); ;
    }
}
