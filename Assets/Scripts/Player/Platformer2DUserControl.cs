using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
	//TODO: organise class variables, make it tidy, idfk.
	private PlatformerCharacter2D m_character;
	private GameObject m_checkpoint;
	private DeathTracker m_difficulty;
	private LevelManager m_levelManager;
	private AudioManager m_audioManager;
	
	private bool m_isDead = false;
	private Animator m_animator;

	private bool m_jumping;
	private bool m_gravityEnabled;
	private bool m_teleporting;
	private bool m_slowTimeEnabled;
	private bool m_isPaused;
	private static bool m_levelStarted = false;

	private bool m_allowSlow = true;
	private bool m_allowTeleport = true;
	private bool m_allowGravity = true;
	private bool m_isMoveable = true;                                                                                                                             
	static private bool m_isMuted = false;

	public Rect m_winRect = new Rect(200, 200, 240, 100);

	// Timers
	private static Timer m_levelTimer;
	private static Timer m_pauseTimer;
	//private Timer m_pathTimer = new Timer(1000);
	
	private static float m_globalTime = 0f;
	private static float m_levelTime = 0f;
	private static float m_deathTime = 0f;
	private static float m_pauseTime = 0f;

	// UI variables for timers.
	// TODO: Move this to UIUpdate?
	Text m_timer;
	private string m_timerText;
	Text m_difficultyText;
	Text m_deathCountText;
	
	// JSON log variables
	/*
	private static JSONGenerator m_json = new JSONGenerator();
	private static int messageSendsLeft = 3;
	private DateTime m_timeNow;
	private static string m_levelBegin = "n/a";
	private static string m_levelEnd = "n/a";
	private static int m_diffLevelBegin;
	[HideInInspector]public bool m_neuron = false;
	private static List<float> m_playerPath = new List<float>();
	private int m_teleportCount;
	private int m_gravityCount;
	private int m_slowtimeCount;
	private int m_doubleJumps;
	private string m_timeOfDeath;
	private string m_latestAbility = "n/a";
	private string m_timeOfLatestAbility = "n/a";
	private static Vector2 m_charPos;
	*/

	private void Start()
	{
		if(!Application.loadedLevelName.Contains("Overworld") && !Application.loadedLevelName.Contains("Tutorial"))
		{
			m_levelTimer.Elapsed += new ElapsedEventHandler(levelTimerElapsed);
			//m_pathTimer.Elapsed += new ElapsedEventHandler(pathTimerElapsed);
			m_pauseTimer.Elapsed += new ElapsedEventHandler(pauseTimerElapsed);
		}
	}
	
	private void Awake()
	{
		m_levelTime = 0;
		//m_timeNow = DateTime.Now;
		//m_levelBegin = string.Format("{0:D2}:{1:D2}.{2:D2}", m_timeNow.Hour, m_timeNow.Minute, m_timeNow.Second);
		m_difficulty = GameObject.Find("GameManager").GetComponent<DeathTracker>();
		m_checkpoint = GameObject.FindGameObjectWithTag("Checkpoint");
		m_character = GetComponent<PlatformerCharacter2D>();
		m_animator = GetComponent<Animator>();
		m_levelManager = m_character.GetLevelManager();
		m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		
		if(!Application.loadedLevelName.Contains("Overworld") && !Application.loadedLevelName.Contains("Tutorial"))
		{
			m_levelTimer = new Timer(10);
			m_pauseTimer = new Timer(10);
			m_timer = GameObject.Find("LevelTimer").GetComponent<Text>();
			m_difficultyText = GameObject.Find("DifficultyLevel").GetComponent<Text>();
			m_deathCountText = GameObject.Find("DeathCount").GetComponent<Text>();
			
			if(!m_levelStarted)
			{
				m_levelStarted = true;
				startTimer();
				//m_diffLevelBegin = m_difficulty.Difficulty;
			}
		}
		if(Application.loadedLevelName.Contains("World1") || Application.loadedLevelName.Contains("Overworld"))
			m_allowSlow = false;
	}

	/*
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
	*/
	
	/*
	public void LogDeath(int deathCount)
	{
		timeOfDeath = timeNow.Hour + ":" + timeNow.Minute + ":" + timeNow.Second + "." + timeNow.Millisecond;
		json.logDeath(deathCount, character.transform.position.x, character.transform.position.y, timeOfDeath,
		              teleportCount, gravityCount, slowtimeCount, doubleJumps, latestAbility, timeOfLatestAbility,
		              playerPath.ToArray());
		deathTime = 0f;
		playerPath.Clear();
	}
	*/
	
	public void startTimer()
	{
		m_levelTimer.Enabled = true;
		//m_pathTimer.Enabled = true;
	}
	
	public void stopTimer()
	{
		m_globalTime += m_levelTime;
		m_levelTimer.Enabled = false;
		//m_pathTimer.Enabled = false;
	}
	
	static void levelTimerElapsed(object sender, ElapsedEventArgs e)
	{
		// increment level and death times by .01 seconds
		m_levelTime += .01f;
		m_deathTime += .01f;
	}

	/*
	static void pathTimerElapsed(object sender, ElapsedEventArgs e)
	{
		//TODO: Doesn't really do anything.. I think. Check it out.
		bool added = false;
		if (!added)
		{
			added = true;
			//m_playerPath.Add(charPos.x);
			//m_playerPath.Add(charPos.y);
		}
	}
	*/
	
	static void pauseTimerElapsed(object sender, ElapsedEventArgs e)
	{
		m_pauseTime += .01f;
	}

	private void Update()
	{
		//m_timeNow = DateTime.Now;
		//charPos = m_character.transform.position;

		if(!Application.loadedLevelName.Contains("Overworld") && !Application.loadedLevelName.Contains("Tutorial"))
		{
			var ts = TimeSpan.FromSeconds(m_levelTime);
			m_timerText = string.Format("{0:D2}:{1:D2}.{2:D3}", ts.Minutes, ts.Seconds, ts.Milliseconds);

			// Update HUD texts
			m_timer.text = m_timerText;
			m_difficultyText.text = "Difficulty: " + m_difficulty.Difficulty.ToString();
			m_deathCountText.text = "Deaths: " + m_levelManager.TotalDeaths.ToString();
		}

		// Escape key or start button
		if(Input.GetButtonDown("Pause"))
		{
			// If not paused, pause the game, stop the level timer and freeze the game.
			if(!m_isPaused)
			{
				Time.timeScale = 0f;
				m_isPaused = true;
				m_pauseTimer.Enabled = true;
				stopTimer();
			}
			else
			{
				Time.timeScale = 1f;
				m_isPaused = false;
				m_pauseTimer.Enabled = false;
				startTimer();
			}
		}

		// Space bar or X button
		if(Input.GetButtonDown("Jump")) // Space bar or X button
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
			m_character.JumpCount += 1;
			//if(character.jumpCount >= 2)
				//doubleJumps++;
			m_jumping = true;
		}
		if(Input.GetButtonUp("Jump"))
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
			m_jumping = false;
		}

		if(m_allowGravity)
		{
			// D or Square button
			if(Input.GetButtonDown("Gravity"))
			{
				/*
				//m_latestAbility = "Gravity";
				//m_gravityCount++;
				//m_timeOfLatestAbility = String.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", m_timeNow.Hour, m_timeNow.Minute, m_timeNow.Second, m_timeNow.Millisecond);
				*/
				GetComponent<Rigidbody2D>().isKinematic = false;
				m_gravityEnabled = true;
			}
		}

		if(m_allowTeleport)
		{
			// E or R1 button
			if(Input.GetButtonDown("Teleport"))
			{
				/*
				//m_latestAbility = "Teleport";
				//m_teleportCount++;
				//m_timeOfLatestAbility = String.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", m_timeNow.Hour, m_timeNow.Minute, m_timeNow.Second, m_timeNow.Millisecond);
				*/
				GetComponent<Rigidbody2D>().isKinematic = false;
				m_teleporting = true;
			}
		}

		if(m_allowSlow)
		{
			// S or L1 button
			if(Input.GetButtonDown("Slow"))
			{
				/*
				//m_latestAbility = "Slow";
				//m_slowtimeCount++;
				//m_timeOfLatestAbility = String.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", m_timeNow.Hour, m_timeNow.Minute, m_timeNow.Second, m_timeNow.Millisecond);
				*/
				m_slowTimeEnabled = true;
			}
			if(Input.GetButtonUp("Slow"))
				m_slowTimeEnabled = false;
		}
	}

	private void FixedUpdate()
	{
	    // Read the inputs.
		float h = Input.GetAxis("Horizontal");
	    // Pass all parameters to the character control script.
		if(m_isMoveable && !m_isDead)
		{
			m_character.Move(h);
			m_character.Jump(m_jumping);
			if(m_gravityEnabled)
				m_character.Gravity(false);
			if(m_teleporting)
				m_character.Teleport();
			if(m_allowSlow)
				m_character.Slow(m_slowTimeEnabled);
		}
		m_gravityEnabled = false;
		m_teleporting = false;
	}

	/*void OnApplicationQuit()
	{
		if(Application.loadedLevelName.Contains("Easy") || Application.loadedLevelName.Contains("Medium"))
			LogExit(false);
		m_json.sendMail(false);
	}*/

	// TODO: Look into moving all this GUI stuff to UIUpdate, at least keep all GUI stuff in a dedicated script.
	private void OnGUI()
	{
		if(m_isPaused) 
		{
			m_winRect = GUILayout.Window(0, m_winRect, WindowFunction, "Game Paused");
			m_winRect.x = (int)(Screen.width * 0.5f - m_winRect.width * 0.5f);
			m_winRect.y = (int)(Screen.height * 0.5f - m_winRect.height * 0.5f);
			
			// Added
			GUILayout.Window(0, m_winRect, WindowFunction, "Game Paused");
		}
	}

	void WindowFunction(int windowID)
	{
		if(GUILayout.Button("Continue")) 
		{
			Time.timeScale = 1f;
			m_isPaused = false;
		}
		if(!m_isMuted)
		{
			if(GUILayout.Button("Mute"))
			{
				m_character.muteAudio(true);
				m_isMuted = true;
			}
		}
		else
		{
			if(GUILayout.Button("Unmute"))
			{
				m_character.muteAudio(false);
				m_isMuted = false;
			}
		}

		if(!Application.loadedLevelName.Contains("Overworld"))
		{
			if(GUILayout.Button("Level Select"))
			{
				// Reset checkpoint if it's there.
				m_levelManager.CheckpointReached = false;
				m_levelManager.ResetPosition();

				Time.timeScale = 1f;
				stopTimer();
				m_isPaused = false;
				m_pauseTimer.Enabled = false;

				NeuronTracker neuronTracker = GameObject.Find("GameManager").GetComponent<NeuronTracker>();
				neuronTracker.ResetNeuron(true);

				//LogExit(false);
				//m_neuron = false;
				Application.LoadLevel(neuronTracker.GetPrevLevel);
			}
		}

		if(Application.loadedLevelName.Contains("Overworld"))
		{
			// TODO: Look more into this, this is broken as shit.
			if(GUILayout.Button("Menu Screen"))
			{
				m_isPaused = false;
				Time.timeScale = 1f;
				Application.LoadLevel(0);
			}
		}

		/*
		if(messageSendsLeft > 0)
		{
			if(GUILayout.Button("Send log (" + m_messageSendsLeft + ")"))
			{
				m_json.sendMail(true);
				m_messageSendsLeft--;
			}
		}
		*/
		if(GUILayout.Button("Exit Game"))
			Application.Quit();
	}

	public Animator getAnimator()
	{
		return m_animator;
	}

	public bool Teleport
	{
		get { return m_allowTeleport; }
		set { m_allowTeleport = value; }
	}
	
	public bool Slow
	{
		get { return m_allowSlow; }
		set { m_allowSlow = value; }
	}
	
	public bool Gravity
	{
		get { return m_allowGravity; }
		set { m_allowGravity = value; }
	}
	
	public bool Move
	{
		get { return m_isMoveable; }
		set { m_isMoveable = value; }
	}

	public bool Dead 
	{
		get { return m_isDead;}
		set { m_isDead = value;}
	}
}
