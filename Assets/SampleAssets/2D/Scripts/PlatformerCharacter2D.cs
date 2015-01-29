using UnityEngine;

namespace UnitySampleAssets._2D
{

    public class PlatformerCharacter2D : MonoBehaviour
    {
        private bool facingRight = true; // For determining which way the player is currently facing.

        [SerializeField] private float maxSpeed = 12f; // The fastest the player can travel in the x axis.
        [SerializeField] private float jumpForce = 900f; // Amount of force added when the player jumps.	

        [SerializeField] private bool airControl = false; // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
		[SerializeField] private LayerMask whatIsWall; // A mask determining what is a wall to the character

		// Checking if grounded.
        private Transform groundCheck; // A position marking where to check if the player is grounded.
        private float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool grounded = false; // Whether or not the player is grounded.
		// Checking if teleporting into a wall.
		private Transform wallCheck;	 // A position marking where to check if the player is inside a wall.
		private float wallRadius = .2f;  // Radius of the overlap circle to determine if inside a wall.
		private bool atWall = false; // Whether or not the player is inside a wall.
		// Checking if hitting a ceiling.
        //private Transform ceilingCheck; // A position marking where to check for ceilings
        //private float ceilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator anim; // Reference to the player's animator component.

		public int jumpCount = 0;

        private void Awake()
        {
            // Setting up references.
            groundCheck = transform.Find("GroundCheck");
			wallCheck = transform.Find("WallCheck");
            //ceilingCheck = transform.Find("CeilingCheck");
            anim = GetComponent<Animator>();
        }


        private void FixedUpdate()
        {
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
            anim.SetBool("Ground", grounded);

			atWall = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);

            // Set the vertical animation
			if (rigidbody2D.gravityScale < 0) 
			{
				Vector2 gScale = rigidbody2D.velocity;
				gScale.y *= -1;
				anim.SetFloat ("vSpeed", gScale.y);
			} 
			else 
			{
				anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
			}
        }


		public void Move(float move, bool jump, bool gravity, bool teleport)
        {
            //only control the player if grounded or airControl is turned on
            if (grounded || airControl)
            {
                // The Speed animator parameter is set to the absolute value of the horizontal input.
                anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                rigidbody2D.velocity = new Vector2(move*maxSpeed, rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !facingRight)
                    // ... flip the player.
                    Flip();
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight)
                    // ... flip the player.
                    Flip();
            }
            // If the player should jump...
			if (grounded)
				jumpCount = 0;
			if (jump) {
				if (jumpCount < 1) {
					jumpCount++;
					if(rigidbody2D.gravityScale > 0)
					{
						// Add a vertical force to the player.
						grounded = false;
						anim.SetBool("Ground", false);
						rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
						rigidbody2D.AddForce(new Vector2(0f, jumpForce));
						if (grounded) {
							anim.SetBool("Ground", false);                
							grounded = false;
						}
					}
					else
					{
						grounded = false;
						anim.SetBool("Ground", false);
						rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
						rigidbody2D.AddForce(new Vector2(0f, -jumpForce));
						if (grounded) {
							anim.SetBool("Ground", false);                
							grounded = false;
						}
					}

				}
			}
            if (grounded && jump && anim.GetBool("Ground"))
            {
				if(rigidbody2D.gravityScale > 0)
				{
	                // Add a vertical force to the player.
	                grounded = false;
	                anim.SetBool("Ground", false);
					rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
	                rigidbody2D.AddForce(new Vector2(0f, jumpForce));
				}
				else
				{
					grounded = false;
					anim.SetBool("Ground", false);
					rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
					rigidbody2D.AddForce(new Vector2(0f, -jumpForce));
				}
            }

			if(gravity)
			{
				Vector3 theScale = transform.localScale;

				theScale.y *= -1;
				transform.localScale = theScale;

				rigidbody2D.gravityScale *= -1;
				anim.SetBool("Ground", false);
			}
			if(teleport && !atWall)
			{
				Vector3 dashScale = new Vector3(10.0f, 0.0f);

				if(facingRight)
				{
					//if(Physics2D.OverlapCircle(wallCheck.position + dashScale, wallRadius, whatIsWall))
						//dashScale = wallCheck.position - (wallCheck.position - dashScale);
					transform.position += transform.right + dashScale;
					/*Debug.Log("Right:\n\tdashScale" + dashScale + 
					          "\n\twallCheck: "+ wallCheck.position +
					          "\n\ttransform: " + transform.position);*/
				}
				else
				{
					//if(Physics2D.OverlapCircle(wallCheck.position - dashScale, wallRadius, whatIsWall))
						//dashScale = wallCheck.position + (wallCheck.position + dashScale);
					transform.position -= transform.right + dashScale;
					/*Debug.Log("Left:\n\tdashScale" + dashScale + 
					          "\n\twallCheck: "+ wallCheck.position +
					          "\n\ttransform: " + transform.position);*/
				}
			}
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}