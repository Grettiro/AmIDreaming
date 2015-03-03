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
	private bool paused;
	private Animator anim;
	Slider slowTimeSlider;
	public Rect winRect = new Rect(200, 200, 240, 100);

	private void Awake()
	{
	    character = GetComponent<PlatformerCharacter2D>();
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		if (Input.GetKeyDown ("escape")) 
		{
			if (!paused)
			{
				Time.timeScale = 0f;
				paused = true;
			}
			else
			{
				Time.timeScale = 1f;
				paused = false;
			}
		}
		if (Input.GetKeyDown ("m")) 
		{
			anim.SetTrigger ("Die");
		}
		if (Input.GetButtonDown("Jump")) 
		{
			character.jumpCount += 1;
			jump = true;
		}
		if (Input.GetButtonUp("Jump")) 
		{
			jump = false;
		}
		if (Input.GetKeyDown("d") || Input.GetButtonDown("Fire3"))
		{
			gravity = true;
		}
		if (Input.GetKeyDown("e") || Input.GetButtonDown("Fire2"))
		{
			teleport = true;
		}
		if(Application.loadedLevelName != "World1")
		{
			if(Input.GetKeyDown ("s") || Input.GetButtonDown("Fire1"))
				slowTime = true;	
			if(Input.GetKeyUp ("s") || Input.GetButtonUp("Fire1"))
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

	private void OnGUI()
	{
		if (paused) 
		{
			winRect = GUILayout.Window( 0, winRect, WindowFunction, "Game Paused" );
			winRect.x = (int) ( Screen.width * 0.5f - winRect.width * 0.5f );
			winRect.y = (int) ( Screen.height * 0.5f - winRect.height * 0.5f );
			
			// Added
			GUILayout.Window( 0, winRect, WindowFunction, "Game Paused" );
		}
	}

	void WindowFunction(int windowID) {
		if (GUILayout.Button ("Continue")) 
		{
			Time.timeScale = 1f;
			paused = false;
		}
		if(Application.loadedLevelName != "World1")
		{
			if(GUILayout.Button ("Level Select"))
			{
				Time.timeScale = 1f;
				paused = false;
				NeuronCount setLevel = GameObject.Find("PlayerNeurons").GetComponent<NeuronCount>();
				Application.LoadLevel(setLevel.GetPrevLevel);
			}
		}
		if(Application.loadedLevelName == "World1")
		{
			if(GUILayout.Button ("Menu Screen"))
			{
				paused = false;
				Time.timeScale = 1f;
				Application.LoadLevel (0);
			}
		}
		if(GUILayout.Button ("Exit Game"))
				Application.Quit();
		
	}
}
