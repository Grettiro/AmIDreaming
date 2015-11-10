using UnityEngine;
using UnityEngine.UI;

public class PlatformerCharacter2D : MonoBehaviour
{
	[SerializeField] private float m_speed = 12f; 			// The speed of the player along the x axis.
	[SerializeField] private static Vector3 m_startingPosition;
	
	// Layer masks to determine what colliders to use.
	[SerializeField] private LayerMask whatIsGround; 		// A mask determining what is ground to the character
	[SerializeField] private LayerMask whatIsWall; 			// A mask determining what is a wall to the character
	[SerializeField] private LayerMask whatIsCeiling; 		// A mask determining what is a wall to the character
	[SerializeField] private LayerMask whatIsImpassable; 	// A mask determining what is impassable (teleporting)
	[SerializeField] private LayerMask whatIsCheckpoint; 	// A mask determining what is a checkpoint object

	private Platformer2DUserControl m_characterControl;

	// Audio clips used for ability SFXs.
	public AudioClip m_audioJump;
	public AudioClip m_audioGravity;
	public AudioClip m_audioTeleport;
	private bool m_allowJumpSound = true;
	static private bool m_isMuted;
	
	private Rigidbody2D m_rigidbody;						// Reference to the player's rigidbody2D component.
	private Animator m_animator; 							// Reference to the player's animator component.
	private LevelManager m_levelManager;

	// TODO: Figure out if this is needed.
	GameObject m_checkpoint;
	private CheckpointObject m_checkpointObj;

	// Checking if grounded.
	private Transform m_groundCheck; 						// A position marking where to check if the player is grounded.
	private bool m_isGrounded = false; 						// Whether or not the player is grounded.
	private float m_transformRadius = 0.1f; 					// Radius of the overlap circle to determine if grounded
	// Checking if teleporting into a wall.
	private Transform m_teleportCheck;						// A position to determine how far the player can teleport.
	private Transform m_wallCheckUpper;	 					// A position marking where to check if the player is inside a wall.
	private Transform m_wallCheckLower;						// Upper and lower checks determine the area to check for a collision.
	private bool m_isAtWall = false; 						// Whether or not the player is at or inside a wall.
	// Checking if hitting a ceiling.
	private Transform m_ceilingCheck; 						// A position marking where to check for ceilings.
	private bool m_isAtCeiling = false;

	// Ability and player related variables.
	private bool m_isFacingRight = true; 						// For determining which way the player is currently facing.
	private int m_jumpCount = 0;
	private float m_jumpVelocity = 300f;
	private float m_jumpForce = 800f;
	private bool m_teleportAllow = true;
	private bool m_slowToggle = false;

	// TODO: Rename these to make them more descriptive.
	// Classes to be slowed
	private Animator slow;
	private EnemyMovement enemy;
	private EnemyCircleMovement enemyC;
	private MovingObject enemyM;
	private PlatformMovement platform;

	private void Awake()
	{
	    // Setting up references.
		m_characterControl = GetComponent<Platformer2DUserControl>();
		m_animator = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();

	    m_groundCheck = transform.Find("GroundCheck");
		m_wallCheckUpper = transform.Find("WallCheckUpper");
		m_wallCheckLower = transform.Find("WallCheckLower");
		m_teleportCheck = transform.Find("TeleportCheck");
	    m_ceilingCheck = transform.Find("CeilingCheck");


		m_checkpoint = GameObject.FindGameObjectWithTag("Checkpoint");
		if(m_checkpoint != null)
		{
			m_checkpointObj = GameObject.Find("CheckPoint").GetComponent<CheckpointObject>();
			if(m_checkpointObj.IsCheckpoint)
			{
				Debug.Log("starting pos: " + m_startingPosition);
				if(m_checkpointObj.m_gravityFlipped)
				{
					// Reverse the y position so you don't start on the other end, in a way.
					Gravity(true);
				}
			}
			else
				m_startingPosition = m_levelManager.CharacterPosition;

			this.transform.position = m_startingPosition;
		}
	}

	private void OnEnable()
	{
		m_rigidbody.isKinematic = false;
	}

	private void OnDisable()
	{
		m_rigidbody.isKinematic = true;
	}

	private void FixedUpdate()
	{
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		m_isGrounded = Physics2D.OverlapCircle(m_groundCheck.position, m_transformRadius, whatIsGround);
		m_animator.SetBool("Ground", m_isGrounded);
		m_isAtWall = Physics2D.OverlapArea(m_wallCheckLower.position, m_wallCheckUpper.position, whatIsWall);
		m_isAtCeiling = Physics2D.OverlapCircle(m_ceilingCheck.position, m_transformRadius, whatIsCeiling);

		// Set the vertical animation
		if(m_rigidbody.gravityScale < 0)
			m_animator.SetFloat("vSpeed", (m_rigidbody.velocity.y * -1));
		else
			m_animator.SetFloat("vSpeed", m_rigidbody.velocity.y);

	}
	
	public void muteAudio(bool mute)
	{
		m_isMuted = mute;
	}

	public void Move(float move)
	{
		if(m_isGrounded)
		{
			m_jumpCount = 0;
			m_teleportAllow = true;
		}

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		m_animator.SetFloat("Speed", Mathf.Abs(move));

		// To prevent getting stuck at a wall.
		if(!m_isGrounded && m_isAtWall)
			if((m_isFacingRight && move > 0.0f) || (!m_isFacingRight && move < 0.0f))
				move = 0.0f;

		// Move the character
		m_rigidbody.velocity = new Vector2(move * m_speed, m_rigidbody.velocity.y);
		if(move > 0.0f && !m_isFacingRight)
			Flip();
		else if(move < 0.0f && m_isFacingRight)
			Flip();
	}

	public void Jump(bool jump)
	{
		if(jump)
		{
			if(m_jumpCount < 2)
			{
				if(m_allowJumpSound && !m_isMuted)
					GetComponent<AudioSource>().PlayOneShot(m_audioJump);

				if(m_jumpVelocity <= m_jumpForce)
				{
					m_allowJumpSound = false;
					m_isGrounded = false;
					m_jumpVelocity += 100f;
					m_animator.SetBool("Ground", false);
					m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, 0);

					// Apply the jump force to the character depending on the current gravity scale
					if(m_rigidbody.gravityScale > 0)
						m_rigidbody.AddForce(new Vector2(0f, m_jumpVelocity));
					else
						m_rigidbody.AddForce(new Vector2(0f, -m_jumpVelocity));
				}
			}
		}
		if(!jump)
		{
			// Reset the variables changed while jumping.
			m_allowJumpSound = true;
			m_jumpVelocity = 300f;
		}
	}

	public void Gravity(bool checkpointFlip)
	{
		if(!m_isMuted && !checkpointFlip)
		{
			GetComponent<AudioSource>().Stop();
			GetComponent<AudioSource>().PlayOneShot(m_audioGravity);
		}

		// Change the horizontal position of the character using temp variables.
		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;
		// Change the gravity of the character.
		m_rigidbody.gravityScale *= -1;

		m_animator.SetBool("Ground", false);
	}

	public void Teleport()
	{
		if(m_teleportAllow)
		{
			m_teleportAllow = false;

			Vector3 loopCheck = new Vector3(0.0f, 0.0f);
			Vector3 wallEdge = new Vector3(0.0f, 0.0f);

			float wallCounter = 0.0f;
			float distanceCounter = 0.0f;
			bool impassableObject = false;
		
			if(m_isFacingRight)
			{
				// Check how many collisions there will be along the way (how many units into the wall,
				// if there is a wall, the character would travel).
				for(loopCheck.x = 0.0f; loopCheck.x < 10.0f; loopCheck.x += 0.1f)
				{
					// Check for checkpoints while teleporting. Is only accessed if a checkpoint is detected.
					if(Physics2D.OverlapCircle((m_teleportCheck.position + loopCheck), m_transformRadius, whatIsCheckpoint))
					{
						m_checkpointObj.IsCheckpoint = true;
						// What is the purpose of this?
						m_checkpointObj.Checkpoint = m_checkpointObj.transform.position;
					}

					if(Physics2D.OverlapCircle((m_teleportCheck.position + loopCheck), m_transformRadius, whatIsImpassable))
					{
						impassableObject = true;
						break;
					}
					if(Physics2D.OverlapCircle((m_teleportCheck.position + loopCheck), m_transformRadius, whatIsWall))
						wallCounter++;
					else
						distanceCounter++;
				}
			
				// Update how long the teleport will take the character, full distance - how many units 
				// into the wall, if any).
				if(impassableObject)
					wallEdge.x = distanceCounter / 10.0f - 1.0f; // Reduce by 1.0f because of the door.
				else
					wallEdge.x = (10.0f - wallCounter / 10.0f);
			

				if((impassableObject && distanceCounter <= 0.0f) || (m_isAtWall && wallEdge.x < (wallCounter / 10.0f))){} // do nothing.
				else
				{
					if(!m_isMuted)
						GetComponent<AudioSource>().PlayOneShot(m_audioTeleport, 1f);
					// Move the character!
					transform.position += transform.right + wallEdge;
				}
			}
			else
			{
				for(loopCheck.x = 0.0f; loopCheck.x < 10.0f; loopCheck.x += 0.1f)
				{
					if(Physics2D.OverlapCircle((m_teleportCheck.position - loopCheck), m_transformRadius, whatIsCheckpoint))
					{
						m_checkpointObj.IsCheckpoint = true;
						m_checkpointObj.Checkpoint = m_checkpointObj.transform.position;
					}

					if(Physics2D.OverlapCircle((m_teleportCheck.position - loopCheck), m_transformRadius, whatIsImpassable))
					{
						impassableObject = true;
						break;
					}
					if(Physics2D.OverlapCircle((m_teleportCheck.position - loopCheck), m_transformRadius, whatIsWall))
						wallCounter++;
					else
						distanceCounter++;
				}
				if(impassableObject)
					wallEdge.x = distanceCounter / 10.0f - 1.0f; // Reduce by 1.0f because of the door.
				else
					wallEdge.x = (10.0f - wallCounter / 10.0f);
			
				if((impassableObject && distanceCounter <= 0.0f) || (m_isAtWall && wallEdge.x < (wallCounter / 10.0f))){} // do nothing.
				else
				{
					if(!m_isMuted)
						GetComponent<AudioSource>().PlayOneShot(m_audioTeleport, 1f);
					transform.position -= transform.right + wallEdge;
				}
			}
		}
	}

	public void Slow(bool slowTime)
	{
		Slider slowTimeSlider = GameObject.Find("SlowTimeBar").GetComponent<Slider>();
		GameObject[] slowable = GameObject.FindGameObjectsWithTag("Slowable");

		if(slowTime)
		{
			if(slowTimeSlider.value > 0.0f)
			{
				// Slowly decrease the time bar.
				slowTimeSlider.value -= 0.005f;

				// Slow any slowable game objects that were found.
				if(slowable != null)
				{
					if(!m_slowToggle)
					{
						m_slowToggle = true;
						foreach(GameObject enemies in slowable)
						{
							if((slow = GameObject.Find(enemies.name).GetComponent<Animator>()) != null)
								slow.speed /= 2.5f;
							if((enemy = enemies.GetComponent<EnemyMovement>()) != null)
							{
								enemy.speed /= 2.5f;
								if(Mathf.Abs(enemy.GetComponent<Rigidbody2D>().velocity.x) > 0.0f)
									enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(enemy.speed.x, 0.0f);
								else
									enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-enemy.speed.x, 0.0f);

								if(Mathf.Abs(enemy.GetComponent<Rigidbody2D>().velocity.y) > 0.0f)
									enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, enemy.speed.y);
								else
									enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -enemy.speed.y);
							}
							if((enemyC = enemies.GetComponent<EnemyCircleMovement>()) != null)
								enemyC.speed /= 2.5f;
							if((enemyM = enemies.GetComponent<MovingObject>()) != null)
								enemyM.speed /= 2.5f;
							if((platform = enemies.GetComponent<PlatformMovement>()) != null)
								platform.speed /= 2.5f;
							// add more checks for any further things to be slowed, if any.
						}
					}
					if(slowTimeSlider.value == 0.0f && m_slowToggle)
					{
						m_slowToggle = false;
						foreach(GameObject enemies in slowable)
						{
							if((slow = GameObject.Find(enemies.name).GetComponent<Animator>()) != null)
								slow.speed *= 2.5f;
							if((enemy = enemies.GetComponent<EnemyMovement>()) != null)
							{
								enemy.speed *= 2.5f;
							if(Mathf.Abs(enemy.GetComponent<Rigidbody2D>().velocity.x) > 0.0f)
								enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(enemy.speed.x, 0.0f);
							else
								enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-enemy.speed.x, 0.0f);
							
							if(Mathf.Abs(enemy.GetComponent<Rigidbody2D>().velocity.y) > 0.0f)
								enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, enemy.speed.y);
							else
								enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -enemy.speed.y);
							}
							if((enemyC = enemies.GetComponent<EnemyCircleMovement>()) != null)
								enemyC.speed *= 2.5f;
							if((enemyM = enemies.GetComponent<MovingObject>()) != null)
								enemyM.speed *= 2.5f;
							if((platform = enemies.GetComponent<PlatformMovement>()) != null)
								platform.speed *= 2.5f;
						}
					}
				}
			}
		}
		if(!slowTime)
		{
			if(slowTimeSlider.value < 1.0f)
				slowTimeSlider.value += 0.002f;

			if(slowable != null)
			{
				if(slowTimeSlider.value < 1.0f && m_slowToggle)
				{
					m_slowToggle = false;
					foreach(GameObject enemies in slowable)
					{
						if((slow = GameObject.Find(enemies.name).GetComponent<Animator>()) != null)
							slow.speed *= 2.5f;
						if((enemy = enemies.GetComponent<EnemyMovement>()) != null)
						{
							enemy.speed *= 2.5f;
								
							if(Mathf.Abs(enemy.GetComponent<Rigidbody2D>().velocity.x) > 0.0f)
								enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(enemy.speed.x, 0.0f);
							else
								enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-enemy.speed.x, 0.0f);
							
							if(Mathf.Abs(enemy.GetComponent<Rigidbody2D>().velocity.y) > 0.0f)
								enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, enemy.speed.y);
							else
								enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -enemy.speed.y);
						}
						if((enemyC = enemies.GetComponent<EnemyCircleMovement>()) != null)
							enemyC.speed *= 2.5f;
						if((enemyM = enemies.GetComponent<MovingObject>()) != null)
							enemyM.speed *= 2.5f;
						if((platform = enemies.GetComponent<PlatformMovement>()) != null)
							platform.speed *= 2.5f;
					}
				}
			}
		}
	}

	private void Flip()
	{
	    // Switch the way the player is labelled as facing.
	    m_isFacingRight = !m_isFacingRight;

	    // Flip the way the character is facing.
	    Vector3 theScale = transform.localScale;
	    theScale.x *= -1;
	    transform.localScale = theScale;
	}

	[SerializeField]
	public int JumpCount
	{
		get { return m_jumpCount; }
		set { m_jumpCount = value; }
	}

	public Vector3 StartingPosition
	{
		get { return m_startingPosition; }
		set { m_startingPosition = value; }
	}
}
