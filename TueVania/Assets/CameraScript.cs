using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Object Declarations")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform cameraTransform;

    [Header("Offests And Tweaks")]
    [SerializeField] float sidewaysMaxOffset;
    [SerializeField] float mouseMaxOffset;
    [SerializeField] float mouseToWorldMultiplier; 

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();
    }

    private Vector2 FetchMouseOffset() 
    {
        return (Camera.main.ScreenToViewportPoint(Input.mousePosition) - new Vector3(0.5f,0.5f, 0f)) * mouseToWorldMultiplier;
    }

    private void UpdateCameraPosition()
    {
        Vector3 newCamPos = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraTransform.position.z);
        newCamPos += new Vector3(FetchMouseOffset().x, FetchMouseOffset().y, 0);
        cameraTransform.position = newCamPos;
    }
}
