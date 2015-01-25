﻿using UnityEngine;

namespace UnitySampleAssets._2D
{

    public class PlatformerCharacter2D : MonoBehaviour
    {
        private bool facingRight = true; // For determining which way the player is currently facing.

        [SerializeField] private float maxSpeed = 10f; // The fastest the player can travel in the x axis.
        [SerializeField] private float jumpForce = 300f; // Amount of force added when the player jumps.	

        [SerializeField] private bool airControl = false; // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character

        private Transform groundCheck; // A position marking where to check if the player is grounded.
        private float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool grounded = false; // Whether or not the player is grounded.
        //private Transform ceilingCheck; // A position marking where to check for ceilings
        //private float ceilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator anim; // Reference to the player's animator component.


        private void Awake()
        {
            // Setting up references.
            groundCheck = transform.Find("GroundCheck");
            //ceilingCheck = transform.Find("CeilingCheck");
            anim = GetComponent<Animator>();
        }


        private void FixedUpdate()
        {
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
            anim.SetBool("Ground", grounded);

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


        public void Move(float move, bool jump, bool gravity, bool dash)
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
            if (grounded && jump && anim.GetBool("Ground"))
            {
				if(rigidbody2D.gravityScale > 0)
				{
	                // Add a vertical force to the player.
	                grounded = false;
	                anim.SetBool("Ground", false);
	                rigidbody2D.AddForce(new Vector2(0f, jumpForce));
				}
				else
				{
					grounded = false;
					anim.SetBool("Ground", false);
					rigidbody2D.AddForce(new Vector2(0f, -jumpForce));
				}
            }
			if(gravity)
			{
				Vector3 theScale = transform.localScale;
				theScale.y *= -1;
				transform.localScale= theScale;
				if(rigidbody2D.gravityScale > 0)
				{
					rigidbody2D.gravityScale *= -1;
					anim.SetBool("Ground", false);
				}
				else
				{
					rigidbody2D.gravityScale *= -1;
					anim.SetBool("Ground", false);
				}
			}
			else if(dash)// && (grounded || airControl))
			{
				anim.SetFloat("Speed", Mathf.Abs(move));
				// do stuff to "teleport" the character a short range
				if(facingRight)
					rigidbody2D.velocity = new Vector2(200.0f, rigidbody2D.velocity.y);
				else
					rigidbody2D.velocity = new Vector2(-200.0f, rigidbody2D.velocity.y);
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