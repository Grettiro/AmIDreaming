﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Timers;
using System;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
	private PlatformerCharacter2D character;
	private static JSONGenerator json = new JSONGenerator();
	private DeathTracker difficulty;
	private GameObject checkpoint;
	private bool jump;
	private int doubleJump;
	private bool gravity;
	private bool teleport;
	private bool slowTime;
	private bool paused;
	private bool allowSlow = true;
	private bool allowTeleport = true;
	private bool allowGravity = true;
	private bool moveable = true;                                                                                                                             
	static private bool muted = false;
	private Animator anim;
	Slider slowTimeSlider;
	CheckpointObject setPos;
	public Rect winRect = new Rect(200, 200, 240, 100);
	private static bool levelStarted = false;

	// Timers
	private static Timer levelTimer = new Timer(10);
	private static Timer pauseTimer = new Timer(10);
	private Timer pathTimer = new Timer(1000);
	
	private static float globalTime = 0f;
	private static float levelTime = 0f;
	private static float deathTime = 0f;
	private static float pauseTime = 0f;
	
	// JSON log variables
	private DateTime timeNow;
	private static string levelBegin = "n/a";
	private static string levelEnd = "n/a";
	private static int diffLevelBegin;
	public bool neuron = false;
	private static List<float> playerPath = new List<float>();
	private int teleportCount;
	private int gravityCount;
	private int slowtimeCount;
	private int doubleJumps;
	private string timeOfDeath;
	private string latestAbility = "n/a";
	private string timeOfLatestAbility = "n/a";
	private static Vector2 charPos;
	
	public void LogExit(bool levelFinished)
	{
		levelEnd = timeNow.Hour + ":" + timeNow.Minute + ":" + timeNow.Second + "." + timeNow.Millisecond;
		json.LevelExit(neuron, levelFinished, levelBegin, levelEnd, diffLevelBegin, difficulty.Difficulty, pauseTime, playerPath.ToArray());
		neuron = false;
		levelTime = 0f;
		pauseTime = 0f;
		playerPath.Clear();
		stopTimer();
		levelStarted = false;
	}
	
	public void LogDeath(int deathCount)
	{
		timeOfDeath = timeNow.Hour + ":" + timeNow.Minute + ":" + timeNow.Second + "." + timeNow.Millisecond;
		json.logDeath(deathCount, character.transform.position.x, character.transform.position.y, timeOfDeath,
		              teleportCount, gravityCount, slowtimeCount, doubleJumps, latestAbility, timeOfLatestAbility,
		              playerPath.ToArray());
		deathTime = 0f;
		playerPath.Clear();
	}
	
	public void startTimer()
	{
		levelTimer.Enabled = true;
		pathTimer.Enabled = true;
	}
	
	public void stopTimer()
	{
		globalTime += levelTime;
		levelTimer.Enabled = false;
		pathTimer.Enabled = false;
	}
	
	static void levelTimerElapsed(object sender, ElapsedEventArgs e)
	{
		// increment level and death times by .01 seconds
		levelTime += .01f;
		deathTime += .01f;
	}
	
	static void pathTimerElapsed(object sender, ElapsedEventArgs e)
	{
		bool added = false;
		if(!added)
		{
			added = true;
			playerPath.Add(charPos.x);
			playerPath.Add(charPos.y);
		}
	}
	
	static void pauseTimerElapsed(object sender, ElapsedEventArgs e)
	{
		pauseTime += .01f;
	}
	
	private void Start()
	{
		levelTimer.Elapsed += new ElapsedEventHandler(levelTimerElapsed);
		pathTimer.Elapsed += new ElapsedEventHandler(pathTimerElapsed);
		pauseTimer.Elapsed += new ElapsedEventHandler(pauseTimerElapsed);
	}

	private void Awake()
	{
		timeNow = DateTime.Now;
		levelBegin = timeNow.Hour + ":" + timeNow.Minute + ":" + timeNow.Second + "." + timeNow.Millisecond;
		if(!Application.loadedLevelName.Equals("World1") && !Application.loadedLevelName.Equals("World2"))
		{
			if(!levelStarted)
			{
				levelStarted = true;
				difficulty = GameObject.Find("DeathTracker").GetComponent<DeathTracker>();
				startTimer();
				diffLevelBegin = difficulty.Difficulty;
			}
		}
		if (Application.loadedLevelName.Contains ("Easy") || Application.loadedLevelName.Contains ("World")) {
			allowSlow = false;
		}
		checkpoint = GameObject.FindGameObjectWithTag ("Checkpoint");
	    character = GetComponent<PlatformerCharacter2D>();
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
		timeNow = DateTime.Now;
		charPos = character.transform.position;

		if (Input.GetButtonDown("Pause")) // Escape key or start button
		{
			if (!paused)
			{
				Time.timeScale = 0f;
				paused = true;
				pauseTimer.Enabled = true;
			}
			else
			{
				Time.timeScale = 1f;
				paused = false;
				pauseTimer.Enabled = false;
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
			if(character.jumpCount >= 2)
				doubleJumps++;
			jump = true;
		}
		if (Input.GetButtonUp("Jump")) 
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
			jump = false;
		}
		if (allowGravity) {
			if (Input.GetButtonDown ("Gravity")) { // D or Square button
				latestAbility = "Gravity";
				gravityCount++;
				timeOfLatestAbility = timeNow.Hour + ":" + timeNow.Minute + ":" + timeNow.Second + "." + timeNow.Millisecond;
				GetComponent<Rigidbody2D> ().isKinematic = false;
				gravity = true;
			}
		}
		if (allowTeleport) {
			if (Input.GetButtonDown ("Teleport")) { // E or R1 button
				latestAbility = "Teleport";
				teleportCount++;
				timeOfLatestAbility = timeNow.Hour + ":" + timeNow.Minute + ":" + timeNow.Second + "." + timeNow.Millisecond;
				GetComponent<Rigidbody2D> ().isKinematic = false;
				teleport = true;
			}
		}
		if(allowSlow)
		{
			if(Input.GetButtonDown("Slow")) // S or L1 button
			{
				latestAbility = "Slow";
				slowtimeCount++;
				timeOfLatestAbility = timeNow.Hour + ":" + timeNow.Minute + ":" + timeNow.Second + "." + timeNow.Millisecond;
				slowTime = true;
			}
			if(Input.GetButtonUp("Slow"))
				slowTime = false;
		}
	}

	private void FixedUpdate()
	{
	    // Read the inputs.
		float h = Input.GetAxis("Horizontal");
	    // Pass all parameters to the character control script.
		if(moveable)
			character.Move(h, jump, gravity, teleport, slowTime);
		gravity = false;
		teleport = false;
	}

	[SerializeField]
	public bool Teleport
	{
		get {return allowTeleport; }
		set {allowTeleport = value; }
	}

	[SerializeField]
	public bool Slow
	{
		get {return allowSlow; }
		set {allowSlow = value; }
	}

	[SerializeField]
	public bool Gravity
	{
		get {return allowGravity; }
		set {allowGravity = value; }
	}

	public bool Move
	{
		get {return moveable; }
		set {moveable = value; }
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
				stopTimer();
				paused = false;
				pauseTimer.Enabled = false;
				NeuronCount setLevel = GameObject.Find("PlayerNeurons").GetComponent<NeuronCount>();
				LogExit(false);
				neuron = false;
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
