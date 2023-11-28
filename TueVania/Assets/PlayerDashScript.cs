using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashScript : MonoBehaviour 
{
    // can be used to turn on and off power-up
    bool unlocked = true;

    float currentDashCooldown = 0;
    float currentDashDuration = 0;
    bool canDash;
    bool hasDashed;
    RaycastHit2D hit;

    [Header("Dash Settings")]
    [SerializeField] float dashDistance;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration; 
    [SerializeField] float dashCooldown;
    Vector2 target;


    // needs heavy reworking, add raycasts to reduce distance and stop phasing through walls
    public void Dash(Vector2 direction, Transform playerTransform, PlayerInputActions playerInputActions, bool grounded, bool dashJumpCheck) 
    {
        float dash = DashInput(playerInputActions);

        if (unlocked) 
        {
            if ((!(currentDashCooldown > 0) && !(dash > 0) && grounded))
            {
                hasDashed = false;
            }

            if (dash > 0) 
            {
                if ((!(currentDashCooldown > 0) && !hasDashed))
                {
                    canDash = true;
                }
                else 
                { 
                    canDash = false;
                }
            } 
            else 
            {
                canDash = false;
            }

            if (canDash)
            {
                target = Displacement(direction, playerTransform);
                canDash = false;
                hasDashed = true;
                currentDashCooldown = dashCooldown;
                currentDashDuration = dashDuration;
            }
            else 
            {
                target = target;
            }

            if (currentDashDuration > 0 && hasDashed)
            {
                playerTransform.position = Vector2.Lerp(playerTransform.position, target, dashSpeed);
                if (dashJumpCheck) { hasDashed = false; }
            }
        }
    }

    private Vector2 Displacement(Vector2 direction, Transform playerTransform) 
    {
        Vector2 newPos = Vector2.zero;
        if (direction.x > 0)
        {
            newPos = new Vector2(playerTransform.position.x + dashDistance, playerTransform.position.y);
            
        }
        else if (direction.x < 0)
        {
            newPos = new Vector2(playerTransform.position.x - dashDistance, playerTransform.position.y);
            
            
        }
        else 
        { 
            newPos = playerTransform.position;
        }
        return newPos;
    }



    private float DashInput(PlayerInputActions playerInputActions) 
    {
        return playerInputActions.Keyboard.Dash.ReadValue<float>();
    }

    private void Count() 
    {
        if (currentDashCooldown > 0)
            currentDashCooldown -= Time.fixedDeltaTime;
        if (currentDashDuration > 0)
            currentDashDuration -= Time.fixedDeltaTime;
    }

    private void FixedUpdate() 
    {
        Count();
    }
}
