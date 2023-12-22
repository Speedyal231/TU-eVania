using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour {


    [Header("Shooting Settings")]
    [SerializeField] bool unlockedShoot;
    [SerializeField] bool unlockedBigBlast;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bigBlast;
    [SerializeField] Transform targetTransform;
    [SerializeField] float fireRateTime;
    [SerializeField] float chargeTime;
    
    float currentfireRateTime;
    float currentChargeTime;
    bool canShoot = false;
    bool hasShot = false;
    bool canBlastMax = false;
    Vector2 shootTarget;

    public void ShootBullet(Transform playerTransform, PlayerInputActions playerInputActions, bool isAirFlipping) 
    {
        bool input = ShootInput(playerInputActions);

        if (unlockedShoot)
        {
            if (!hasShot && !(currentfireRateTime > 0) && input && !isAirFlipping)
            {
                canShoot = true;
            }

            if ((hasShot || (currentfireRateTime > 0)) && input)
            {
                canShoot = false;
            }
            else
            {
                hasShot = false;
            }


            if (canShoot)
            {
                Vector2 spawnPoint = playerTransform.position + playerTransform.up.normalized * playerTransform.localScale.y / 2;
                SpawnObject(spawnPoint, FetchPlayerToMouseDirection(spawnPoint), bullet);
                currentfireRateTime = fireRateTime;
                currentChargeTime = chargeTime;
                hasShot = true;
            }

            if (unlockedBigBlast)
            {
                if (!(currentChargeTime > 0) && hasShot && input && !canShoot)
                {
                    canBlastMax = true;
                }

                if (canBlastMax && !input && !isAirFlipping)
                {
                    Vector2 spawnPoint = playerTransform.position + playerTransform.up.normalized * playerTransform.localScale.y / 2;
                    SpawnObject(spawnPoint, FetchPlayerToMouseDirection(spawnPoint), bigBlast);
                    Debug.Log("Boom");
                    currentfireRateTime = fireRateTime;
                    hasShot = true;
                    canBlastMax = false;
                }
            }
        }
    }

    //Fix this so it works better, use debugs and test thouroughly.
    private Vector2 FetchPlayerToMouseDirection(Vector2 spawnPoint)
    {
        
        return new Vector2(targetTransform.position.x, targetTransform.position.y) - spawnPoint;
    }

    private bool ShootInput(PlayerInputActions playerInputActions)
    {
        return 0 < playerInputActions.Keyboard.Shoot.ReadValue<float>();
    }

    private void Count()
    {
        if (currentfireRateTime > 0)
            currentfireRateTime -= Time.fixedDeltaTime;
        if (currentChargeTime > 0)
            currentChargeTime -= Time.fixedDeltaTime;
     }

    void FixedUpdate()
    {
        Count();
    }

    void SpawnObject(Vector2 spawnPoint, Vector2 mouseDir, GameObject projectile)
    {
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        // Create a rotation based on the angle
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        Instantiate(projectile, spawnPoint, rotation);
    }

}
