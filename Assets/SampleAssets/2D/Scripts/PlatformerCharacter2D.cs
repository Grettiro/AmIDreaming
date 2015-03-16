using UnityEngine;
using UnityEngine.UI;

public class PlatformerCharacter2D : MonoBehaviour
{
	[SerializeField] private float maxSpeed = 12f; // The fastest the player can travel in the x axis.

	[SerializeField] private bool airControl = false; // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
	[SerializeField] private LayerMask whatIsWall; // A mask determining what is a wall to the character
	[SerializeField] private LayerMask whatIsCeiling; // A mask determining what is a wall to the character
	[SerializeField] private LayerMask whatIsImpassable; // A mask determining what is impassable (teleporting)

	// Checking if grounded.
	private Transform groundCheck; // A position marking where to check if the player is grounded.
	private bool grounded = false; // Whether or not the player is grounded.
	// Checking if teleporting into a wall.
	private Transform wallCheckUpper;	 // A position marking where to check if the player is inside a wall.
	private Transform wallCheckLower;
	private bool atWall = false; // Whether or not the player is inside a wall.
	private Transform teleportCheck;
	// Checking if hitting a ceiling.
	private Transform ceilingCheck; // A position marking where to check for ceilings
	private bool atCeiling = false;

	public AudioClip audioJump;
	public AudioClip audioGravity;
	public AudioClip audioTeleport;

	private float transformRadius = .2f; // Radius of the overlap circle to determine if grounded

	private Animator anim; // Reference to the player's animator component.

	Slider slowTimeSlider;

	private bool facingRight = true; // For determining which way the player is currently facing.
	private bool jumping = false;
	private float jumpVelocity = 300f;
	private float jumpForce = 800f;

	public int jumpCount = 0;
	public int teleportCount = 0;
	private int slowTimeAllow = 1;
	private int slowTimeAllow2 = 1;
	private bool allowJumpSound = true;
	static private bool muted;

	private bool dead = false;

	// Classes to be slowed
	private Animator slow;
	private EnemyMovement enemy;
	private EnemyCircleMovement enemyC;
	private MovingObject enemyM;
	private PlatformMovement platform;
	private AudioControlLoop audioPitch;

	void Awake()
	{
	    // Setting up references.
	    groundCheck = transform.Find("GroundCheck");
		wallCheckUpper = transform.Find("WallCheckUpper");
		wallCheckLower = transform.Find("WallCheckLower");
		teleportCheck = transform.Find("TeleportCheck");
	    ceilingCheck = transform.Find("CeilingCheck");
	    anim = GetComponent<Animator>();

		audioPitch = GameObject.Find("AudioController").GetComponent<AudioControlLoop>();
	}

	public Animator getAnimator()
	{
		return anim;
	}

	public void setDead(bool isDead)
	{
		dead = isDead;
	}

	public void muteAudio(bool mute)
	{
		muted = mute;

		if(mute)
		{
			audioPitch.audioStart.mute = true;
			audioPitch.audioLoop.mute = true;
			audioPitch.audioStart2.mute = true;
			audioPitch.audioLoop2.mute = true;
		}
		else
		{
			audioPitch.audioStart.mute = false;
			audioPitch.audioLoop.mute = false;
			audioPitch.audioStart2.mute = false;
			audioPitch.audioLoop2.mute = false;
		}
	}

	private void FixedUpdate()
	{
	    // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
	    grounded = Physics2D.OverlapCircle(groundCheck.position, transformRadius, whatIsGround);
	    anim.SetBool("Ground", grounded);
		atWall = Physics2D.OverlapArea(wallCheckLower.position, wallCheckUpper.position, whatIsWall);
		atCeiling = Physics2D.OverlapCircle (ceilingCheck.position, transformRadius, whatIsCeiling);

    	// Set the vertical animation
		if(rigidbody2D.gravityScale < 0)
		{
			Vector2 gScale = rigidbody2D.velocity;
			gScale.y *= -1;
			anim.SetFloat ("vSpeed", gScale.y);
		} 
		else 
			anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
	}

	public void Move(float move, bool jump, bool gravity, bool teleport, bool slowTime)
	{
		if(!dead)
		{
		    //only control the player if grounded or airControl is turned on
		    if(grounded || airControl)
		    {
		        // The Speed animator parameter is set to the absolute value of the horizontal input.
		        anim.SetFloat("Speed", Mathf.Abs(move));

				// To prevent getting stuck at a wall.
				if(!grounded && atWall)
				{
					if((facingRight && move > 0.0f) || (!facingRight && move < 0.0f))
						move = 0.0f;
				}
				// Move the character
		        rigidbody2D.velocity = new Vector2(move*maxSpeed, rigidbody2D.velocity.y);

		        if (move > 0.0f && !facingRight)
		            Flip();
		        else if (move < 0.0f && facingRight)
		            Flip();

		    }

			if(grounded)
			{
				jumpCount = 0;
				teleportCount = 0;
			}
	    
			if(slowTime)
			{
				GameObject[] slowable = GameObject.FindGameObjectsWithTag("Slowable");
				GameObject slowTimeBar = GameObject.Find("SlowTimeBar");

				if(slowTimeBar != null)
				{
					slowTimeSlider = slowTimeBar.GetComponent<Slider>();

					if(slowTimeSlider.value > 0.010f)
						slowTimeSlider.value -= 0.005f;
					else
						slowTimeSlider.value = 0.0f;
				}
				if(slowTimeSlider.value >= 0.1)
				{
					if(slowable != null)
					{
						if(slowTimeAllow == 1)
						{
							audioPitch.pitchChangeDown();

							foreach(GameObject enemies in slowable)
							{
								slowTimeAllow = 2;
								slowTimeAllow2 = 1;

								if((slow = GameObject.Find(enemies.name).GetComponent<Animator>()) != null)
									slow.speed /= 2.5f;
								if((enemy = enemies.GetComponent<EnemyMovement>()) != null)
								{
									enemy.speed /= 2.5f;
									if(enemy.rigidbody2D.velocity.x > 0.0f || enemy.rigidbody2D.velocity.y > 0.0f)
										enemy.rigidbody2D.velocity = enemy.speed;
									else
										enemy.rigidbody2D.velocity = -enemy.speed;
								}
								if((enemyC = enemies.GetComponent<EnemyCircleMovement>()) != null)
								{
									enemyC.speed /= 2.5f;
								}
								if((enemyM = enemies.GetComponent<MovingObject>()) != null)
								{
									enemyM.speed /= 2.5f;
								}
								if((platform = enemies.GetComponent<PlatformMovement>()) != null)
									platform.speed /= 2.5f;
								// add more checks for any further things to be slowed, if any.
							}
						}
					}
				}
				if(slowTimeSlider.value < 0.01)
				{
					if(slowTimeAllow2 == 1)
					{
						audioPitch.pitchChangeUp();
						foreach(GameObject enemies in slowable)
						{
							slowTimeAllow2 = 2;
							slowTimeAllow = 1;

							if((slow = GameObject.Find(enemies.name).GetComponent<Animator>()) != null)
								slow.speed *= 2.5f;
							if((enemy = enemies.GetComponent<EnemyMovement>()) != null)
							{
								enemy.speed *= 2.5f;
								if(enemy.rigidbody2D.velocity.x > 0.0f || enemy.rigidbody2D.velocity.y > 0.0f)
									enemy.rigidbody2D.velocity = enemy.speed;
								else
									enemy.rigidbody2D.velocity = -enemy.speed;
							}
							if((enemyC = enemies.GetComponent<EnemyCircleMovement>()) != null)
							{
								enemyC.speed *= 2.5f;
							}
							if((enemyM = enemies.GetComponent<MovingObject>()) != null)
							{
								enemyM.speed *= 2.5f;
							}
							if((platform = enemies.GetComponent<PlatformMovement>()) != null)
								platform.speed *= 2.5f;

						}
					}
				}
			}
			if(!slowTime)
			{
				GameObject[] slowable = GameObject.FindGameObjectsWithTag("Slowable");
				GameObject slowTimeBar = GameObject.Find("SlowTimeBar");

				audioPitch.pitchChangeUp();

				if(slowTimeBar != null)
				{
					slowTimeSlider = slowTimeBar.GetComponent<Slider>();

					if(slowTimeSlider.value < 0.95f)
						slowTimeSlider.value += 0.002f;
					else
						slowTimeSlider.value = 1;
				}
				if(slowable != null)
				{
					if(slowTimeAllow == 2)
					{				
						foreach(GameObject enemies in slowable)
						{
							slowTimeAllow = 1;
							if((slow = GameObject.Find(enemies.name).GetComponent<Animator>()) != null)
								slow.speed *= 2.5f;
							if((enemy = enemies.GetComponent<EnemyMovement>()) != null)
							{
								enemy.speed *= 2.5f;
								if(enemy.rigidbody2D.velocity.x > 0.0f || enemy.rigidbody2D.velocity.y > 0.0f)
									enemy.rigidbody2D.velocity = enemy.speed;
								else
									enemy.rigidbody2D.velocity = -enemy.speed;
							}
							if((enemyC = enemies.GetComponent<EnemyCircleMovement>()) != null)
							{
								enemyC.speed *= 2.5f;
							}
							if((enemyM = enemies.GetComponent<MovingObject>()) != null)
							{
								enemyM.speed *= 2.5f;
							}
							if((platform = enemies.GetComponent<PlatformMovement>()) != null)
								platform.speed *= 2.5f;
						}
					}
				}
			}

			// If the player should jump...
			if(jump)
			{
				if(jumpCount < 2)
				{
					if(allowJumpSound && !muted)
					{
						audio.PlayOneShot(audioJump);
					}
					if(jumpVelocity <= jumpForce)
					{
						allowJumpSound = false;
						jumping = true;
						jumpVelocity += 100f;
						grounded = false;
						anim.SetBool("Ground", false);
						rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
						if(rigidbody2D.gravityScale > 0)
							rigidbody2D.AddForce(new Vector2(0f, jumpVelocity));
						else
							rigidbody2D.AddForce(new Vector2(0f, -jumpVelocity));
					}
				}
			}
			if (!jump) 
			{
				if(jumping == true)
				{
					allowJumpSound = true;
					jumpVelocity = 300f;
					rigidbody2D.AddForce(new Vector2(0f, 1f));
				}
				jumping = false;
			}

			if(gravity)
			{
				Vector3 theScale = transform.localScale;

				theScale.y *= -1;

				if(!muted)
				{
					audio.Stop();
					audio.PlayOneShot(audioGravity);
				}

				transform.localScale = theScale;
				rigidbody2D.gravityScale *= -1;
				anim.SetBool("Ground", false);
			}

			if(teleport && (teleportCount == 0))
			{
				teleportCount++;
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
						if(Physics2D.OverlapCircle((teleportCheck.position + loopCheck), transformRadius, whatIsImpassable))
						{
							impassableObject = true;
							break;
						}
						if(Physics2D.OverlapCircle((teleportCheck.position + loopCheck), transformRadius, whatIsWall))
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
					{
						if(!muted)
						{
							audio.PlayOneShot(audioTeleport, 1f);
						}
						transform.position += transform.right + wallEdge;
					}
				}
				else
				{
					for(loopCheck.x = 0.0f; loopCheck.x < dashScale.x; loopCheck.x += 0.1f)
					{
						if(Physics2D.OverlapCircle((teleportCheck.position - loopCheck), transformRadius, whatIsImpassable))
						{
							impassableObject = true;
							break;
						}
						if(Physics2D.OverlapCircle((teleportCheck.position - loopCheck), transformRadius, whatIsWall))
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
					{
						if(!muted)
						{
							audio.PlayOneShot(audioTeleport, 1f);
						}
						transform.position -= transform.right + wallEdge;
					}
				}

				wallCounter = 0;
				distanceCounter = 0;
				impassableObject = false;
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

	[SerializeField]
	public int JumpCount
	{
		get {return jumpCount; }
		set {jumpCount = value; }
	}
}
