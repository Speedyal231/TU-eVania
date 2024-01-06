using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour {


    [Header("Shooting Settings")]
    [SerializeField] Rigidbody2D rbPlayer;
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

    public void ShootBullet(Transform gunTransform, PlayerInputActions playerInputActions, bool isAirFlipping, PlayerSound sfx, int small, int big, int charge, int chargeStart, AudioSource src) 
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
                currentfireRateTime = fireRateTime;
                currentChargeTime = chargeTime;
                hasShot = true;
                canShoot = false;
                sfx.PlaySound(small, src);
                Vector2 spawnPoint = gunTransform.position + gunTransform.right.normalized * 0.7f;
                SpawnObject(spawnPoint, FetchPlayerToMouseDirection(gunTransform.position), bullet); 
            }

            if (unlockedBigBlast)
            {
                if (!(currentChargeTime > 0) && hasShot && input && !canShoot)
                {
                    canBlastMax = true;
                    sfx.PlaySoundfixedLoop(charge, 1, src);
                }
                else if (!(currentChargeTime > chargeTime * 3/5) && hasShot && input && !canShoot)
                {
                    sfx.PlaySoundfixedLoop(chargeStart, 1, src);
                } 
                

                if (canBlastMax && !input && !isAirFlipping)
                {
                    currentfireRateTime = fireRateTime;
                    hasShot = true;
                    canBlastMax = false;
                    sfx.PlaySound(big, src);
                    Vector2 spawnPoint = gunTransform.position + gunTransform.right.normalized * 0.7f;
                    SpawnObject(spawnPoint, FetchPlayerToMouseDirection(gunTransform.position), bigBlast);
                    Debug.Log("Boom");  
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
