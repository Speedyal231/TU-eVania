using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashScript : MonoBehaviour 
{
    float currentDashCooldown = 0;
    float currentDashDuration = 0;
    bool canDash;
    bool hasDashed;
    bool dashLCheck;
    bool dashRCheck;
    int currentDashes;

    [Header("Dash Settings")]
    [SerializeField] bool unlocked = true;
    [SerializeField] float dashDistance;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration; 
    [SerializeField] float dashCooldown;
    [SerializeField] float dashLimitCheck;
    [SerializeField] int numberOfDashes;
    Vector2 target;
    public bool dashing;


    // needs heavy reworking, add raycasts to reduce distance and stop phasing through walls
    public void Dash(Vector2 direction, Transform playerTransform, PlayerInputActions playerInputActions, bool grounded, bool dashJumpCheck, bool Lcheck, bool Rcheck, CapsuleCollider2D capsuleCollider, float wallRayOffset, LayerMask groundLayer) 
    {
        float dash = DashInput(playerInputActions);

        if (unlocked) 
        {
            if (dashJumpCheck) { currentDashes = numberOfDashes; }

            if ((!(currentDashCooldown > 0) && !(dash > 0) && grounded))
            {
                hasDashed = false;
            }

            if (dash > 0) 
            {
                if ((!(currentDashCooldown > 0) && !hasDashed) || (!(currentDashCooldown > 0) && (currentDashes > 0)))
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
                currentDashes -= 1;
            }
            else 
            {
                //nothing target remains the same;
            }

            if (currentDashDuration > 0 && hasDashed)
            {
                dashing = true;
                dashLCheck = Physics2D.BoxCast(playerTransform.position + playerTransform.up.normalized * capsuleCollider.size.y / 2, new Vector2(capsuleCollider.size.x / 2, (capsuleCollider.size.y - wallRayOffset) * 3 / 5), 0, Vector2.left, dashLimitCheck, groundLayer);
                dashRCheck = Physics2D.BoxCast(playerTransform.position + playerTransform.up.normalized * capsuleCollider.size.y / 2, new Vector2(capsuleCollider.size.x / 2, (capsuleCollider.size.y - wallRayOffset) * 3 / 5), 0, Vector2.right, dashLimitCheck, groundLayer);
                if (dashLCheck || dashRCheck)
                {
                    currentDashDuration = 0;
                }
                else
                {
                    playerTransform.position = Vector2.Lerp(playerTransform.position, target, dashSpeed);
                }
                if (dashJumpCheck) { hasDashed = false; }
            } 
            else if (!(currentDashDuration > 0))
            {
                dashing = false;
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

    private void Start()
    {
        currentDashes = numberOfDashes;
    }

    private void FixedUpdate() 
    {
        Count();
    }

    public void changeDashStatus(){

        unlocked = true;
    }
    public bool dashStatus()
    {
        return unlocked;
    }
}
