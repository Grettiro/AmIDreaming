﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
	private PlatformerCharacter2D character;
	private JSONGenerator json;
	private GameObject checkpoint;
	private bool jump;
	private int doubleJump;
	private bool gravity;
	private bool teleport;
	private bool slowTime;
	private bool paused;
	static private bool muted = false;
	private Animator anim;
	Slider slowTimeSlider;
	CheckpointObject setPos;
	public Rect winRect = new Rect(200, 200, 240, 100);

	public void Log(string levelName, bool neuron)
	{
		json.LevelExit(levelName, neuron);
		// Do stuff
	}

	private void Awake()
	{
		checkpoint = GameObject.FindGameObjectWithTag ("Checkpoint");
	    character = GetComponent<PlatformerCharacter2D>();
		json = GetComponent<JSONGenerator>();
		if (checkpoint != null) {
			setPos = checkpoint.GetComponent<CheckpointObject> ();
			if (setPos.IsCheckpoint) {
				this.transform.position = setPos.Checkpoint;
				GetComponent<Rigidbody2D> ().isKinematic = true;
			}
		}
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Pause")) // Escape key or start button
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
		if (Input.GetKeyDown ("left")) {
			GetComponent<Rigidbody2D>().isKinematic = false;
		}
		if (Input.GetKeyDown ("right")) {
			GetComponent<Rigidbody2D>().isKinematic = false;
		}
		// Space bar or X button
		if (Input.GetButtonDown("Jump")) // Space bar or X button
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
			character.jumpCount += 1;
			jump = true;
		}
		if (Input.GetButtonUp("Jump")) 
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
			jump = false;
		}
		if (Input.GetButtonDown("Gravity")) // D or Square button
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
			gravity = true;
		}
		if (Input.GetButtonDown("Teleport")) // E or R1 button
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
			teleport = true;
		}
		if(Application.loadedLevelName != "World1")
		{
			if(Input.GetButtonDown("Slow")) // S or L1 button
				slowTime = true;	
			if(Input.GetButtonUp("Slow"))
				slowTime = false;
		}
	}

	private void FixedUpdate()
	{
	    // Read the inputs.
		float h = Input.GetAxis("Horizontal");
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
		if(!muted)
		{
			if(GUILayout.Button("Mute"))
			{
				character.muteAudio(true);
				muted = true;
			}
		}
		else
		{
			if(GUILayout.Button("Unmute"))
			{
				character.muteAudio(false);
				muted = false;
			}
		}
		if(Application.loadedLevelName != "World1" && Application.loadedLevelName != "World2")
		{
			if(GUILayout.Button ("Level Select"))
			{
				Time.timeScale = 1f;
				paused = false;
				NeuronCount setLevel = GameObject.Find("PlayerNeurons").GetComponent<NeuronCount>();
				Application.LoadLevel(setLevel.GetPrevLevel);
			}
		}
		if(Application.loadedLevelName == "World1" || Application.loadedLevelName == "World2")
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
