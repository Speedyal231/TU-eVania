using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClingScript : MonoBehaviour
{
    bool LWall;
    bool RWall;
    bool canCling;
    bool clinging;
    Vector2 clingPosition;
    float currentClingResetTime;
    float gravity = 5f; 

    [Header("Cling Settings")]
    [SerializeField] bool unlocked;
    [SerializeField] float wallHitRange;
    [SerializeField] float clingResetTime;

    //Holy shit fix this
    public bool Cling(Transform playerTransform, BoxCollider2D boxCollider, float wallRayOffset, LayerMask groundLayer, bool dashJumpCheck, Vector2 direction, bool grounded, Rigidbody2D RB, Vector2 velocity) 
    {
        if (unlocked) 
        {
            wallClingcheck(playerTransform, boxCollider, wallRayOffset, groundLayer);

            if ((LWall || RWall) && !clinging && !(0 < currentClingResetTime))
            {
                canCling = true;
                clingPosition = playerTransform.position;
            }
            else
            {
                canCling = false;
            }

            if (!grounded)
            {
                if (canCling)
                {
                    clinging = true;
                    playerTransform.position = clingPosition;
                    RB.gravityScale = 0;
                    RB.velocity = Vector2.zero;
                    velocity = Vector2.zero;
                    currentClingResetTime = clingResetTime;
                    canCling = false;
                }
                else if (dashJumpCheck && !canCling)
                { 
                    RB.gravityScale = gravity;
                    clinging = false;
                }
                else if (clinging && !canCling)
                {
                    playerTransform.position = clingPosition;
                }
            }
            else 
            { 
                clinging = false;
            }

            Debug.Log(clinging);
            
            return clinging;

        }
        else
        {
            return false;
        }

    }

    private void wallClingcheck(Transform playerTransform, BoxCollider2D boxCollider, float wallRayOffset, LayerMask groundLayer) 
    {
        LWall = Physics2D.BoxCast(playerTransform.position + playerTransform.up.normalized * boxCollider.size.y / 2, new Vector2(boxCollider.size.x / 2, boxCollider.size.y / 3 - wallRayOffset), 0, Vector2.left, wallHitRange, groundLayer);
        RWall = Physics2D.BoxCast(playerTransform.position + playerTransform.up.normalized * boxCollider.size.y / 2, new Vector2(boxCollider.size.x / 2, boxCollider.size.y / 3 - wallRayOffset), 0, Vector2.right, wallHitRange, groundLayer);
    }

    private void Count()
    {
        if (currentClingResetTime > 0)
            currentClingResetTime -= Time.fixedDeltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Count();
    }
}
