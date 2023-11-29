using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Object Declarations")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Rigidbody2D RB;
    [SerializeField] Transform cameraTransform;

    [Header("Offests And Tweaks")]
    [SerializeField] float mouseToWorldMultiplier;
    [SerializeField] float zoomMultiplier = 5f;
    [SerializeField] float zoomSpeed = 5f;
    [SerializeField] float minZoom = 5f;
    [SerializeField] float maxZoom = 8f;
    [SerializeField] float RBmin = 8f;
    [SerializeField] float RBmax = 14f;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();
        UpdateCameraZoom();
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

    private void UpdateCameraZoom() 
    {
        float Target;
        float speed = Mathf.RoundToInt(Mathf.Abs(RB.velocity.x));

        if (speed <= RBmin)
        {
            Target = minZoom;
        }
        else if (speed > RBmin && speed < RBmax)
        {
            Target = minZoom + (speed - RBmin) * zoomMultiplier;
        }
        else 
        {
            Target = maxZoom;
        }

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Target, zoomSpeed);

    }
}
