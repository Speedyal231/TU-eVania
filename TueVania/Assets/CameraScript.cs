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
    [SerializeField] float sidewaysOffset;
    [SerializeField] float mouseOffset;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(FetchMousePos());
        UpdateCameraPosition();
    }

    private Vector2 FetchMousePos() 
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void UpdateCameraPosition()
    {
        Vector3 newCamPos = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraTransform.position.z);
        cameraTransform.position = newCamPos;
    }
}
