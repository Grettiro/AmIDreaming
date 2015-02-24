using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour 
{
	private bool facingRight = true;
	
	[SerializeField] private LayerMask whatIsWall; // A mask determining what is a wall to the character
	
	private Transform wallCheck;	 // A position marking where to check if the player is inside a wall.
	private float wallRadius = .1f;  // Radius of the overlap circle to determine if inside a wall.
	private bool atWall = false; // Whether or not the player is inside a wall.
	
	public Vector2 speed = new Vector2(0.0f, 0.0f);
	
	// Use this for initialization
	void Start()
	{
		rigidbody2D.velocity = speed;
	}
	
	private void Awake()
	{
		// Setting up references.
		wallCheck = transform.FindChild("WallCheck");
	}
	
	private void FixedUpdate()
	{
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		atWall = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);
	}
	
	// Update is called once per frame
	void Update()
	{
		if(facingRight)
		{
			rigidbody2D.velocity = speed;
		} 
		else if(!facingRight)
		{
			rigidbody2D.velocity = speed * -1;
		}
		
		if(atWall)
		{
			Debug.Log("Is hit");
			Flip();
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
