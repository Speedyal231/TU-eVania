using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour {

	public float moveSpeed;
	public bool moveRight;

	public Transform wallCheck;
	public float wallCheckradius;
	public LayerMask whatIsWall;
	private bool walled;

	private bool atEdge;
	public Transform edgeCheck;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		walled = Physics2D.OverlapCircle(wallCheck.position, wallCheckradius, whatIsWall);
		atEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckradius, whatIsWall);

		if (atEdge || walled)
		{
			moveRight = !moveRight;
		}

		float moveDirection = moveRight ? 1f : -1f;

		transform.localScale = new Vector3(moveDirection, 1f, 1f);

		GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed * moveDirection, GetComponent<Rigidbody2D>().velocity.y);
			
		}
}