using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour {

    public float moveSpeed;
    public bool moveRight;

    public Transform groundCheck;
    public float groundCheckDistance;
    public LayerMask whatIsGround;

    public Transform wallCheck;
    public float wallCheckDistance;
    public LayerMask whatIsWall;

    private bool atEdge;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        bool isGrounded = CheckGround();
        bool isWalled = CheckWall();
        atEdge = isWalled;

        if (!isGrounded || isWalled)
        {
            moveRight = !moveRight;
        }

        float moveDirection = moveRight ? 1f : -1f;

        transform.localScale = new Vector3(moveDirection, 1f, 1f);

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed * moveDirection, GetComponent<Rigidbody2D>().velocity.y);
    }

    bool CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        return hit.collider != null;
    }

    bool CheckWall()
    {
        float direction = moveRight ? 1f : -1f;
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, new Vector2(direction, 0), wallCheckDistance, whatIsWall);
        return hit.collider != null;
    }
}
