using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Object Declarations")]
    [SerializeField] Rigidbody2D RB;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform cameraTransform;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] PlayerDashScript playerDashScript;
    private PlayerInputActions playerInputActions;


    [Header("Movement")]
    [SerializeField] float acceleration;
    [SerializeField] float accelerationFriction;
    [SerializeField] float groundMaxWalkSpeed;
    [SerializeField] float groundMaxRunSpeed;
    

    [Header("Ground + Wall detection")]
    [SerializeField] float groundHitRange;
    [SerializeField] float wallHitRange;
    [SerializeField] float wallRayOffset;
    [SerializeField] LayerMask groundLayer;

    [Header("Jump + Air movement")]
    [SerializeField] float jumpTime;
    [SerializeField] float jump;
    [SerializeField, Range(0,2)] float airDragMultiplier;

    //physics variables
    private Vector2 velocity;
    private float prevGroundVelocity;
    private float currentJumpTime;

    //ground detection variables
    private bool grounded;
    private bool Lcheck;
    private bool Rcheck;
    private bool canJump;
    private bool hasJumped;
    private bool dashJumpCheck;
    private enum State
    {
        Ground,
        Air,
        Cling
    }
    private State playerState;

    /// <summary>
    /// 
    /// 
    /// 
    /// 
    /// Main Method Declarations
    /// 
    /// 
    /// 
    /// 
    /// </summary>


    private void Awake()
    {
        //enable player input script.
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PhysicsCalcInit();
        GroundedCheck();
        WalledCheck();
        StateSwitch();
        Count();
        Vector2 moveInput = GetMoveInput();
        float runInput = GetRunInput();
        Move(moveInput);
        Jump(GetJumpInput());
        playerDashScript.Dash(moveInput, playerTransform, playerInputActions, grounded, dashJumpCheck, Lcheck, Rcheck, boxCollider, wallRayOffset, groundLayer);
        Debug.Log(Mathf.Abs(RB.velocity.x));
        if (playerState == State.Ground)
        {
            GetLastSpeed();
            Friction(runInput);
        }
        else if (playerState == State.Air)
        {
            AirDrag(prevGroundVelocity);
        }

        ApplyForces();
    }

    /// <summary>
    /// 
    /// 
    /// 
    /// 
    /// Supplemental Method Declarations
    /// 
    /// 
    /// 
    /// 
    /// </summary>

    private Vector2 GetMoveInput()
    {
        Vector2 inputVector = playerInputActions.Keyboard.Move.ReadValue<Vector2>();
        if ((Rcheck && inputVector.x > 0) || (Lcheck && inputVector.x < 0))
        {
            inputVector.x -= inputVector.x;
        }
        return inputVector;
    }

    private float GetJumpInput()
    {
        return playerInputActions.Keyboard.Jump.ReadValue<float>();
    }

    private float GetRunInput()
    {
        return playerInputActions.Keyboard.Run.ReadValue<float>();
    }

    private void GetLastSpeed()
    {
        prevGroundVelocity = RB.velocity.magnitude;
    }

    private void PhysicsCalcInit()
    {
        velocity = Vector2.zero;
    }

    private void ApplyForces()
    {
        RB.velocity += velocity;
    }

    private void Move(Vector2 inputVector)
    {
        velocity.x += inputVector.x * acceleration;
    }

    // funtion to apply friction to motion of player on ground 
    private void Friction(float run)
    {
        float max;
        if (run > 0)
        {
            max = groundMaxRunSpeed;
        }
        else
        {
            max = groundMaxWalkSpeed;
        }

        if (RB.velocity.x != 0f)
        {
            if (Mathf.Abs(RB.velocity.x) >= max)
            {
                velocity.x -= RB.velocity.normalized.x * acceleration;
            }
            else
            {

                if (Mathf.Abs(RB.velocity.x) < accelerationFriction)
                {
                    velocity.x -= RB.velocity.x;
                }
                else
                {
                    velocity.x -= RB.velocity.normalized.x * accelerationFriction;
                }

            }
        }
    }

    private void Jump(float input)
    {
        if (!(input > 0) && (hasJumped))
        {
            hasJumped = false;
        }

        if ((input > 0) && !(hasJumped) && grounded)
        {
            canJump = true;
        }

        if (canJump)
        {
            currentJumpTime = jumpTime;
            hasJumped = true;
            canJump = false;
        }

        if ((input > 0) && (currentJumpTime > 0) & hasJumped)
        {
            velocity.y += jump * input;
            dashJumpCheck = true;
        }
        else 
        {
            dashJumpCheck = false;
        }
    }

    // Checks if player is in grounding range
    private void GroundedCheck()
    {
        //grounded = Physics2D.Raycast(playerTransform.position, Vector2.down, groundHitRange, groundLayer);
        grounded = Physics2D.BoxCast(playerTransform.position + playerTransform.up.normalized * boxCollider.size.x * 3 / 8, new Vector2(boxCollider.size.x / 2,boxCollider.size.x * 3 / 4), 0, -playerTransform.up, groundHitRange, groundLayer);
    }

    private void WalledCheck()
    {
        Lcheck = Physics2D.BoxCast(playerTransform.position + playerTransform.up.normalized * boxCollider.size.y / 2, new Vector2(boxCollider.size.x/2, boxCollider.size.y - wallRayOffset), 0, Vector2.left, wallHitRange, groundLayer);
        Rcheck = Physics2D.BoxCast(playerTransform.position + playerTransform.up.normalized * boxCollider.size.y / 2, new Vector2(boxCollider.size.x/2, boxCollider.size.y - wallRayOffset), 0, Vector2.right, wallHitRange, groundLayer); 
    }

    private void StateSwitch()
    {
        if (grounded)
        {
            playerState = State.Ground;
        }
        else
        {
            playerState = State.Air;
        }
    }

    private void AirDrag(float lastSpeed)
    {
        float max = lastSpeed * airDragMultiplier;

        if (RB.velocity.x != 0f)
        {
            if (Mathf.Abs(RB.velocity.x) > max)
            {
                velocity.x -= RB.velocity.x - RB.velocity.normalized.x * max;
            }
            else if (Mathf.Abs(RB.velocity.x) == max)
            {
                velocity.x -= RB.velocity.normalized.x * acceleration;
            }
            else
            {

                if (Mathf.Abs(RB.velocity.x) < accelerationFriction)
                {
                    velocity.x -= RB.velocity.x;
                }
                else
                {
                    velocity.x -= RB.velocity.normalized.x * accelerationFriction;
                }

            }
        }
    }

    private void Count()
    {
        if (currentJumpTime > 0)
            currentJumpTime -= Time.fixedDeltaTime;
    }
}