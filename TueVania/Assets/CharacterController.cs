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
    private PlayerInputActions playerInputActions;

    [Header("movement")]
    [SerializeField] float acceleration;
    [SerializeField] float accelerationFriction;
    [SerializeField] float groundMaxSpeed;
    [SerializeField] float jump;

    //physics variables
    private Vector2 velocity;

    private enum State
    {
        Ground,
        Air
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
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PhysicsCalcInit();
        Vector2 moveInput = GetMoveInput();
        Move(moveInput);
        Friction();
        Jump(GetJumpInput());
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
        inputVector = inputVector;
        return inputVector;
    }

    private float GetJumpInput() 
    {
        return playerInputActions.Keyboard.Jump.ReadValue<float>();
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
    private void Friction()
    {
        if (RB.velocity.x != 0f)
        {
            if (Mathf.Abs(RB.velocity.x) >= groundMaxSpeed)
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
        velocity.y += jump * input;
    }

}
