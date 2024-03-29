﻿using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour 
{
	[SerializeField] private LayerMask whatIsWall; // A mask determining what is a wall to the character

	private float wallRadius = .1f;

	private Transform wallCheck;
	private Transform topCheck;
	private Transform bottomCheck;
	private bool atWall = false;
	private bool atTop = false;
	private bool atBottom = false;

	private bool facingRight = true;

	public Vector2 speed = new Vector2(0.0f, 0.0f);
	
	// Use this for initialization
	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = speed;
	}
	
	private void Awake()
	{

		// Setting up references.
		if(this.gameObject.layer == 11) // EnemyHorizontal layer
			wallCheck = transform.FindChild("WallCheck");
		if(this.gameObject.layer == 12) // EnemyVertical layer
		{
			topCheck = transform.FindChild("TopCheck");
			bottomCheck = transform.FindChild("BottomCheck");
		}
	}
	
	private void FixedUpdate()
	{
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		if(wallCheck != null)
			atWall = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);
		if(topCheck != null && bottomCheck != null)
		{
			atTop = Physics2D.OverlapCircle(topCheck.position, wallRadius, whatIsWall);
			atBottom = Physics2D.OverlapCircle(bottomCheck.position, wallRadius, whatIsWall);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if(wallCheck != null)
		{
			if(facingRight)
				GetComponent<Rigidbody2D>().velocity = speed;
			else if(!facingRight)
				GetComponent<Rigidbody2D>().velocity = speed * -1;
			if(atWall)
				Flip ();
		}
		else if(topCheck != null && bottomCheck != null)
		{
			if(atTop)
				GetComponent<Rigidbody2D>().velocity = speed * -1;
			if(atBottom)
				GetComponent<Rigidbody2D>().velocity = speed;
		}
	}
	
	private void Flip()
	{
		facingRight = !facingRight;
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
