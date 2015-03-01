using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
	private PlatformerCharacter2D character;
	private bool jump;
	private int doubleJump;
	private bool gravity;
	private bool teleport;
	private bool slowTime;
	private Animator anim;
	Slider slowTimeSlider;

	private void Awake()
	{
	    character = GetComponent<PlatformerCharacter2D>();
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		if (Input.GetKey("escape"))
			Application.Quit();

		if (Input.GetKeyDown ("m")) 
		{
			anim.SetTrigger ("Die");
		}
		if (Input.GetButtonDown("Jump")) 
		{
			var jumpCount = GameObject.Find("Player");
			var updateJumps = (PlatformerCharacter2D)jumpCount.GetComponent("PlatformerCharacter2D");
			updateJumps.jumpCount += 1;
			jump = true;
		}
		if (Input.GetButtonUp("Jump")) 
		{
			jump = false;
		}
		if (Input.GetKeyDown("d"))
		{
			gravity = true;
		}
		if (Input.GetKeyDown("e"))
		{
			teleport = true;
		}
		if(Application.loadedLevelName != "World1")
		{
			if(Input.GetKeyDown ("s"))
				slowTime = true;	
			if(Input.GetKeyUp ("s"))
				slowTime = false;
		}
	}

	private void FixedUpdate()
	{
	    // Read the inputs.
		float h = Input.GetAxis ("Horizontal");
	    // Pass all parameters to the character control script.
		character.Move(h, jump, gravity, teleport, slowTime);
		gravity = false;
		teleport = false;
	}
}
