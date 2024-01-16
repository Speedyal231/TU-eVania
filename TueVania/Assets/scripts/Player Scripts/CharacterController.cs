using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

/**
 * This script can be editied to add hitstun and other stuff
 * 
 */
public class CharacterController : MonoBehaviour
{
    [Header("Object Declarations")] 
    [SerializeField] Rigidbody2D RB;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform cameraTransform;
    [SerializeField] GameObject gunVisual;
    [SerializeField] Transform gunTransform;
    [SerializeField] CapsuleCollider2D capsuleCollider;
    [SerializeField] PlayerDashScript playerDashScript;
    [SerializeField] PlayerClingScript playerClingScript;
    [SerializeField] PlayerShootScript playerShootScript;
    [SerializeField] AnimationControlScript animation;
    [SerializeField] PlayerInteract InteractScript;
    [SerializeField] PlayerSound sfx;
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
    [SerializeField, Range(0, 1)] float Aircontrol;
    [SerializeField] float velocityDeltaThreshold;
    [SerializeField] float jumpTime;
    [SerializeField] float jump;
    [SerializeField, Range(0,2)] float airDragMultiplier;
    [SerializeField] float wallJumpDiviser;
    [SerializeField] float wallJumpPower;
    [SerializeField] float flipThreshold;

    [Header("Sound Sources")]
    [SerializeField] AudioSource srcStepsflipsdash;
    [SerializeField] AudioSource srcGun;
    [SerializeField] AudioSource srcJumpCling;


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
    private bool clinging;
    // true if player speed is large and they are in air
    private bool willFlip;
    private bool isAirFlipping;
    // stores current direction faced
    bool lFace;
    bool rFace;
    // ground snap stuff
    RaycastHit2D hit;

    //sounds
    int smallBlastsfx = 0;
    int bigBlastsfx = 1;
    int grabsfx = 2;
    int dashdsfx = 3;
    int landOrStepsfx = 4;
    int flipsfx = 5;
    int jumpsfx = 6;
    int chargesfx = 7;
    int chargeStartsfx = 8;
    int DamageSfx = 9;
    //sounds

    private enum State
    {
        Ground,
        Air
    }  
    private State playerState;
    //private healthManager healthManager;

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
        rFace = true;
        lFace = false;
        //healthManager = FindObjectOfType<healthManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Count();
        checkStunned();
        if(InteractScript.DoorEnterCheck())
        {
            gunVisual.GetComponent<SpriteRenderer>().enabled = false;
            animation.ChangeAnimationState(DoorEnter);
        } else if (stunned){
            gunVisual.GetComponent<SpriteRenderer>().enabled = false;
            animation.ChangeAnimationState(Damaged);
            sfx.PlaySoundfixedLoop(9, 2, srcGun);
        } /*else if (healthManager.getDeadStatus()){
            gunVisual.GetComponent<SpriteRenderer>().enabled = false;
            animation.ChangeAnimationState(Death);
        }*/
        else 
        {
            PhysicsCalcInit();
            GroundedCheck();
            WalledCheck();
            StateSwitch();
            CheckWillFlip();
            UpdateFlipping();
            
            Vector2 moveInput = GetMoveInput();
            float runInput = GetRunInput();
            Move(moveInput);
            Jump(GetJumpInput());
            playerShootScript.ShootBullet(gunTransform, playerInputActions, isAirFlipping, sfx, smallBlastsfx, bigBlastsfx, chargesfx, chargeStartsfx, srcGun);
            clinging = playerClingScript.Cling(playerTransform, capsuleCollider, wallRayOffset, groundLayer, dashJumpCheck, moveInput, grounded, RB, velocity, playerInputActions);
            playerDashScript.Dash(moveInput, playerTransform, playerInputActions, grounded, dashJumpCheck, Lcheck, Rcheck, capsuleCollider, wallRayOffset, groundLayer);
            GroundSnap();
            GetLastSpeed();
            AnimationBehaviour(moveInput, runInput);
            if (playerState == State.Ground)
            {
                Friction(runInput);
            }
            else if (playerState == State.Air)
            {
                AirDrag(prevGroundVelocity);
            }
            ApplyForces();
        }
        
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
        if ((Rcheck && inputVector.x > 0) || (Lcheck && inputVector.x < 0) || clinging)
        {
            inputVector.x -= inputVector.x;
        }
        if (inputVector.x > 0)
        {
            rFace = true;
            lFace = false;
        } 
        else if (inputVector.x < 0)
        {
            rFace = false;
            lFace = true;
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
        if (playerState == State.Ground)
        {
            prevGroundVelocity = RB.velocity.magnitude;
        }
        else if (clinging)
        {
            prevGroundVelocity = groundMaxRunSpeed * airDragMultiplier;
        }
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
        if (playerState == State.Air)
        {
            if (RB.velocity.x > 0 && inputVector.x < 0 || RB.velocity.x < 0 && inputVector.x > 0)
            {
                velocity.x += inputVector.x * acceleration * Aircontrol;
            }
            else 
            {
                velocity.x += inputVector.x * acceleration;
            }
        }
        else 
        {
            velocity.x += inputVector.x * acceleration;
        }
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

        if ((input > 0) && !(hasJumped) && clinging)
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
            
            dashJumpCheck = true;
            if (clinging && Rcheck)
            {
                RB.velocity = Vector2.zero;
                willFlip = true;
                velocity += new Vector2(-jump * input * wallJumpDiviser, jump * input / wallJumpDiviser) * wallJumpPower;
            }
            else if (clinging && Lcheck)
            {
                RB.velocity = Vector2.zero;
                willFlip = true;
                velocity += new Vector2(jump * input * wallJumpDiviser, jump * input / wallJumpDiviser) * wallJumpPower;
            }
            else 
            {
                velocity.y += jump * input;
            }
        }
        else 
        {
            dashJumpCheck = false;
        }
    }

    // Checks if player is in grounding range
    private void GroundedCheck()
    {
        grounded = Physics2D.Raycast(playerTransform.position, Vector2.down, groundHitRange, groundLayer);
        hit = Physics2D.Raycast(playerTransform.position, Vector2.down, groundHitRange, groundLayer);
    }

    private void GroundSnap()
    {
        if (!playerDashScript.dashing && playerState == State.Ground)
        {
            playerTransform.position = hit.point;
        }
    }

    private void WalledCheck()
    {
        //Lcheck = Physics2D.BoxCast(playerTransform.position + playerTransform.up.normalized * boxCollider.size.y / 2, new Vector2(boxCollider.size.x/2, boxCollider.size.y - wallRayOffset), 0, Vector2.left, wallHitRange, groundLayer);
        //Rcheck = Physics2D.BoxCast(playerTransform.position + playerTransform.up.normalized * boxCollider.size.y / 2, new Vector2(boxCollider.size.x/2, boxCollider.size.y - wallRayOffset), 0, Vector2.right, wallHitRange, groundLayer);
        Lcheck = Physics2D.BoxCast(playerTransform.position + playerTransform.up.normalized * capsuleCollider.size.y / 2, new Vector2(capsuleCollider.size.x / 2, capsuleCollider.size.y * 3 / 4 - wallRayOffset), 0, Vector2.left, wallHitRange, groundLayer);
        Rcheck = Physics2D.BoxCast(playerTransform.position + playerTransform.up.normalized * capsuleCollider.size.y / 2, new Vector2(capsuleCollider.size.x / 2, capsuleCollider.size.y * 3 / 4 - wallRayOffset), 0, Vector2.right, wallHitRange, groundLayer);
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
        if (currentStunnedTime > 0)
            currentStunnedTime -= Time.fixedDeltaTime;
    }

    private void CheckWillFlip()
    {
        if (playerState == State.Ground || clinging)
        {
            if (Mathf.Abs(RB.velocity.x) >= flipThreshold)
            {
                willFlip = true;
            }
            else
            {
                willFlip = false;
            }
        }
    }

    private void UpdateFlipping()
    {
        if (willFlip && playerState == State.Air && !clinging)
        {
            isAirFlipping = true;
            
        } 
        else
        {
            isAirFlipping = false;
        }
        
    }

    /// <summary>
    /// 
    /// Animation declaration stuff
    /// 
    /// </summary>

    //animation States
    const string RF_rIdle = "RF_rIdle";
    const string RF_lIdle = "RF_lIdle";
    const string RF_uIdle = "RF_uIdle";
    const string LF_lIdle = "LF_lIdle";
    const string LF_rIdle = "LF_rIdle";

    const string RF_rWalk = "RF_rWalk";
    const string RF_lWalk = "RF_lWalk";
    const string RF_uWalk = "RF_uWalk";
    const string LF_lWalk = "LF_lWalk";
    const string LF_rWalk = "LF_rWalk";

    const string RF_rRun = "RF_rRun";
    const string RF_lRun = "RF_lRun";
    const string RF_uRun = "RF_uRun";
    const string LF_lRun = "LF_lRun";
    const string LF_rRun = "LF_rRun";

    const string RF_rJump = "RF_rJump";
    const string RF_lJump = "RF_lJump";
    const string RF_uJump = "RF_uJump";
    const string LF_lJump = "LF_lJump";
    const string LF_rJump = "LF_rJump";

    const string rightFlip = "RightFlip";
    const string leftFlip = "LeftFlip";

    const string rightCling = "rightCling";
    const string leftCling = "leftCling";

    const string rightDash = "RightDash";
    const string leftDash = "LeftDash";

    const string DoorEnter = "DoorEnter";
    const string Damaged = "Damage";

    const string Death = "Death";

    bool jumped;
    bool clung;

    Vector2 RFGunPos = new Vector2(-0.2f,0.5f);
    Vector2 LFrGunPos = new Vector2(0.08f, 0.5f);

    

    [Header("Knockback Info")] 
    [SerializeField] float StunnedTime;
    [SerializeField] float knockback;
    [SerializeField] float moveVelocity;
    public float knockbackLength;
    public float knockbackCount;

    public bool knockFromRight;

    public bool stunned;

    float currentStunnedTime;

    private void AnimationBehaviour(Vector2 moveInput, float runInput)
    {
        float gunAngle = gunTransform.eulerAngles.z;
        bool moving = MathF.Abs(moveInput.x) > 0;
        bool running = runInput > 0;

        if (isAirFlipping || (playerDashScript.dashing && moving))
        { 
            gunVisual.GetComponent<SpriteRenderer>().enabled = false;
        }
        else 
        {
            gunVisual.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (hasJumped && !jumped) 
        {
            sfx.PlaySound(jumpsfx, srcJumpCling);
            jumped = true;

        }
        else if (!hasJumped && jumped)
        {
            jumped = false;
        }

        if (clinging && !clung)
        {
            sfx.PlaySound(grabsfx, srcJumpCling);
            clung = true;

        }
        else if (!clinging && clung)
        {
            clung = false;
        }

        

        if (rFace)
        {
            gunVisual.GetComponent<SpriteRenderer>().sortingLayerName = "GunOver";
            gunTransform.localPosition = RFGunPos;

            if (clinging || (playerDashScript.dashing && moving))
            {
                if (!clinging && playerDashScript.dashing)
                {
                    sfx.PlaySoundfixedLoop(dashdsfx, 1, srcStepsflipsdash);
                    animation.ChangeAnimationState(rightDash);
                }
            }
            else if (playerState == State.Air)
            {
                if (isAirFlipping)
                {
                    if (!(currentJumpTime > 0)) { sfx.PlaySoundfixedLoop(flipsfx, 0.7f, srcStepsflipsdash); }
                    else { sfx.PlaySoundfixedLoop(jumpsfx, 1.2f, srcJumpCling); }
                    animation.ChangeAnimationState(rightFlip);
                }
                else if (gunAngle < 45 || gunAngle > 270)
                {
                    animation.ChangeAnimationState(RF_rJump);
                }
                else if (gunAngle < 135 && gunAngle >= 45)
                {
                    animation.ChangeAnimationState(RF_uJump);
                }
                else if (gunAngle <= 270 && gunAngle >= 135)
                {
                    animation.ChangeAnimationState(RF_lJump);
                }
            }
            else if (!moving)
            {
                if (gunAngle < 45 || gunAngle > 270)
                {
                    animation.ChangeAnimationState(RF_rIdle);
                }
                else if (gunAngle < 135 && gunAngle >= 45)
                {
                    animation.ChangeAnimationState(RF_uIdle);
                }
                else if (gunAngle <= 270 && gunAngle >= 135)
                {
                    animation.ChangeAnimationState(RF_lIdle);
                }
            } 
            else if (moving)
            {
                
                if (running)
                {
                    if (!(currentJumpTime > 0)) { sfx.PlaySoundfixedLoop(landOrStepsfx, 1.7f, srcStepsflipsdash); }
                    else { sfx.PlaySoundfixedLoop(jumpsfx, 1.2f, srcJumpCling); }
                    if (gunAngle < 45 || gunAngle > 270)
                    {
                        animation.ChangeAnimationState(RF_rRun);
                    }
                    else if (gunAngle < 135 && gunAngle >= 45)
                    {
                        animation.ChangeAnimationState(RF_uRun);
                    }
                    else if (gunAngle <= 270 && gunAngle >= 135)
                    {
                        animation.ChangeAnimationState(RF_lRun);
                    }
                }
                else if (!running)
                {
                    if (!(currentJumpTime > 0)) { sfx.PlaySoundfixedLoop(landOrStepsfx, 2.5f, srcStepsflipsdash); }
                    else { sfx.PlaySoundfixedLoop(jumpsfx, 1.2f, srcJumpCling); }
                    if (gunAngle < 45 || gunAngle > 270)
                    {
                        animation.ChangeAnimationState(RF_rWalk);
                    }
                    else if (gunAngle < 135 && gunAngle >= 45)
                    {
                        animation.ChangeAnimationState(RF_uWalk);
                    }
                    else if (gunAngle <= 270 && gunAngle >= 135)
                    {
                        animation.ChangeAnimationState(RF_lWalk);
                    }
                }
                
            }
        } 
        else if (lFace)
        {
            gunVisual.GetComponent<SpriteRenderer>().sortingLayerName = "GunUnder";
            gunTransform.localPosition = LFrGunPos;

            if (clinging || (playerDashScript.dashing && moving))
            {
                if (!clinging && playerDashScript.dashing)
                {
                    sfx.PlaySoundfixedLoop(dashdsfx, 1, srcStepsflipsdash);
                    animation.ChangeAnimationState(leftDash);
                }
            }
            else if (playerState == State.Air )
            {
                if (isAirFlipping)
                {
                    if (!(currentJumpTime > 0)) { sfx.PlaySoundfixedLoop(flipsfx, 0.7f, srcStepsflipsdash); }
                    else { sfx.PlaySoundfixedLoop(jumpsfx, 1.2f, srcJumpCling); }
                    animation.ChangeAnimationState(leftFlip);
                }
                else if (gunAngle < 120 || gunAngle > 240)
                {
                    animation.ChangeAnimationState(LF_rJump);
                }
                else if (gunAngle <= 240 && gunAngle >= 120)
                {
                    animation.ChangeAnimationState(LF_lJump);
                }
            }
            else if (!moving)
            {
                if (gunAngle < 120 || gunAngle > 240)
                {
                    animation.ChangeAnimationState(LF_rIdle);
                }
                else if (gunAngle <= 240 && gunAngle >= 120)
                {
                    animation.ChangeAnimationState(LF_lIdle);
                }
            }
            else if (moving)
            {
                
                if (running)
                {
                    if (!(currentJumpTime > 0)) {sfx.PlaySoundfixedLoop(landOrStepsfx, 1.7f, srcStepsflipsdash); }
                    else { sfx.PlaySoundfixedLoop(jumpsfx, 1.2f, srcJumpCling); }
                    if (gunAngle < 120 || gunAngle > 240)
                    {
                        animation.ChangeAnimationState(LF_rRun);
                    }
                    else if (gunAngle <= 240 && gunAngle >= 120)
                    {
                        animation.ChangeAnimationState(LF_lRun);
                    }
                } 
                else if (!running)
                {
                    if (!(currentJumpTime > 0)) { sfx.PlaySoundfixedLoop(landOrStepsfx, 2.5f, srcStepsflipsdash); }
                    else { sfx.PlaySoundfixedLoop(jumpsfx, 1.2f, srcJumpCling); }
                    if (gunAngle < 120 || gunAngle > 240)
                    {
                        animation.ChangeAnimationState(LF_rWalk);
                    }
                    else if (gunAngle <= 240 && gunAngle >= 120)
                    {
                        animation.ChangeAnimationState(LF_lWalk);
                    }
                }
                
            }
        }

        if (clinging)
        {
            if (Lcheck)
            {
                gunVisual.GetComponent<SpriteRenderer>().sortingLayerName = "GunUnder";
                gunTransform.localPosition = LFrGunPos;
                animation.ChangeAnimationState(leftCling);
            }
            else if (Rcheck)
            {
                gunVisual.GetComponent<SpriteRenderer>().sortingLayerName = "GunOver";
                gunTransform.localPosition = RFGunPos;
                animation.ChangeAnimationState(rightCling);
            }
        }
    }

    public void knockBack() {
            
            Debug.Log("Knockbacked");
            currentStunnedTime = StunnedTime;
            if (knockFromRight)
            {
                RB.velocity = new Vector2(-knockback, 10f);
            }
            if (!knockFromRight)
            {
                RB.velocity = new Vector2(knockback, 10f);
            }
    }

    private void checkStunned(){

        if (currentStunnedTime > 0) {
            stunned = true;
        } else {
            stunned = false;
        }
    }
}
