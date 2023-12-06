using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour { 



    [Header("Cling Settings")]
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRateTime;

    float currentfireRateTime;

    public void ShootBullet(Transform playerTransform, BoxCollider2D boxCollider, PlayerInputActions playerInputActions) 
    { 


        FetchPlayerToMouseDirection(playerTransform);

        if (ShootInput(playerInputActions) && !(currentfireRateTime > 0)) 
        {
            Vector2 spawnPoint = playerTransform.position + playerTransform.up * boxCollider.size.y / 2 ;
            SpawnObject(spawnPoint, FetchPlayerToMouseDirection(playerTransform));
            currentfireRateTime = fireRateTime;
        }
    }

    private Vector2 FetchPlayerToMouseDirection(Transform playerTransform)
    {
        return (Camera.main.ScreenToViewportPoint(Input.mousePosition) - new Vector3(0.5f, 0.5f, 0f)) - (Camera.main.ScreenToViewportPoint(playerTransform.position));
    }

    private bool ShootInput(PlayerInputActions playerInputActions)
    {
        return 0 < playerInputActions.Keyboard.Shoot.ReadValue<float>();
    }

    private void Count()
    {
        if (currentfireRateTime > 0)
            currentfireRateTime -= Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        Count();
    }

    void SpawnObject(Vector2 spawnPoint, Vector2 mouseDir)
    {
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        // Create a rotation based on the angle
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        Instantiate(bullet, spawnPoint, rotation);
    }


}
