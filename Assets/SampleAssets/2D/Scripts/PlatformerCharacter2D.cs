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
		[SerializeField] private LayerMask whatIsImpassable; // A mask determining what is impassable (teleporting)

		// Checking if grounded.
        private Transform groundCheck; // A position marking where to check if the player is grounded.
        private float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool grounded = false; // Whether or not the player is grounded.
		// Checking if teleporting into a wall.
		private Transform wallCheck;	 // A position marking where to check if the player is inside a wall.
		private float wallRadius = .1f;  // Radius of the overlap circle to determine if inside a wall.
		private bool atWall = false; // Whether or not the player is inside a wall.
		// Checking if hitting a ceiling.
        private Transform ceilingCheck; // A position marking where to check for ceilings
        //private float ceilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator anim; // Reference to the player's animator component.

		public int jumpCount = 0;

        private void Awake()
        {
            // Setting up references.
            groundCheck = transform.Find("GroundCheck");
			wallCheck = transform.Find("WallCheck");
            ceilingCheck = transform.Find("CeilingCheck");
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


		public void Move(float move, bool jump, bool gravity, bool teleport, bool slowTime)
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
			if (slowTime)
			{
				var audioStop = GameObject.Find("AudioController");
				var audioPitch = (AudioControlLoop)audioStop.GetComponent("AudioControlLoop");
				audioPitch.pitchChangeDown();
			}
			if (!slowTime) 
			{
				var audioStop = GameObject.Find("AudioController");
				var audioPitch = (AudioControlLoop)audioStop.GetComponent("AudioControlLoop");
				audioPitch.pitchChangeUp();
			}
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
			if(teleport)
			{
				/*
				 * Still needs some cleaning up to do, proooobably don't need 3 vectors..
				 */
				Vector3 dashScale = new Vector3(10.0f, 0.0f);
				Vector3 loopCheck = new Vector3(0.0f, 0.0f);
				Vector3 wallEdge = new Vector3(0.0f, 0.0f);

				float wallCounter = 0.0f;
				float distanceCounter = 0.0f;
				bool impassableObject = false;

				if(facingRight)
				{
					// Check how many collisions there will be along the way (how many units into the wall,
					// if there is a wall, the character would travel).
					for(loopCheck.x = 0.0f; loopCheck.x < dashScale.x; loopCheck.x += 0.1f)
					{
						if(Physics2D.OverlapCircle((wallCheck.position + loopCheck), wallRadius, whatIsImpassable))
						{
							impassableObject = true;
							break;
						}
						if(Physics2D.OverlapCircle((wallCheck.position + loopCheck), wallRadius, whatIsWall))
							wallCounter++;
						else
							distanceCounter++;
					}

					// Update how long the teleport will take the character, full distance - how many units 
					// into the wall).
					if(impassableObject)
						wallEdge.x = distanceCounter / 10.0f - 1.0f; // Reduce by 1.0f because of the door.
					else
						wallEdge.x = (dashScale.x - wallCounter / 10.0f);

					// Move the character!
					if((impassableObject && distanceCounter <= 0.0f) || (atWall && wallEdge.x < (wallCounter / 10.0f))) {} // do nothing.
					else
						transform.position += transform.right + wallEdge;
				}
				else
				{
					for(loopCheck.x = 0.0f; loopCheck.x < dashScale.x; loopCheck.x += 0.1f)
					{
						if(Physics2D.OverlapCircle((wallCheck.position - loopCheck), wallRadius, whatIsImpassable))
						{
							impassableObject = true;
							break;
						}
						if(Physics2D.OverlapCircle((wallCheck.position - loopCheck), wallRadius, whatIsWall))
							wallCounter++;
						else
							distanceCounter++;
					}

					if(impassableObject)
						wallEdge.x = distanceCounter / 10.0f - 1.0f; // Reduce by 1.0f because of the door.
					else
						wallEdge.x = (dashScale.x - wallCounter / 10.0f);

					if((impassableObject && distanceCounter <= 0.0f) || (atWall && wallEdge.x < (wallCounter / 10.0f))){} // do nothing.
					else
						transform.position -= transform.right + wallEdge;
				}

				wallCounter = 0;
				distanceCounter = 0;
				impassableObject = false;
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