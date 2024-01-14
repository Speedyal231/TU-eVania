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
    [SerializeField] float offsetclamp;


    private void FixedUpdate()
    {
        float camYOffset = cameraTransform.position.y - centreTransform.position.y;
        camYOffset = Mathf.Clamp(centreTransform.position.y + camYOffset * upScrollMultiplier, centreTransform.position.y - offsetclamp, centreTransform.position.y + offsetclamp);
        backgroundTransform.position = new Vector3(centreTransform.position.x, camYOffset, 0); ;
    }
}
