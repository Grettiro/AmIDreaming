using UnityEngine;
using System.Collections;
	
public class LevelFinish : MonoBehaviour
{
	private PlatformerCharacter2D m_player;
	private Platformer2DUserControl m_control;
	private NeuronTracker m_neuronTracker;
	private DeathTracker m_difficulty;
	private LevelManager m_levelManager;
	private AudioManager m_audioManager;

	private bool m_levelFinished = false;

	private void Awake()
	{
		GameObject player = GameObject.Find("Player");
		GameObject gameManager = GameObject.Find("GameManager");

		m_player = player.GetComponent<PlatformerCharacter2D>();
		m_control = player.GetComponent<Platformer2DUserControl>();
		m_neuronTracker = gameManager.GetComponent<NeuronTracker>();
		m_difficulty = gameManager.GetComponent<DeathTracker>();
		m_levelManager = gameManager.GetComponent<LevelManager>();
		m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag.Equals("Player") && !m_levelFinished)
		{
			m_levelFinished = true;
			m_control.Move = false;

			if(m_difficulty.Deaths < m_difficulty.DeathMarker / 2)
				if(m_difficulty.Difficulty < 10)
					m_difficulty.Difficulty += 1;

			// So bad but works for now.
			m_levelManager.CheckpointReached = false;
			m_levelManager.CheckpointGravity = false;
			m_levelManager.ResetPosition();

			if(m_neuronTracker.isSet())
				m_neuronTracker.UpdateNeuronArray(true);

			//control.LogExit(true);

			other.GetComponent<Rigidbody2D>().isKinematic = true;
			StartCoroutine(DoAnimation());
		}
	}

	// Currently there is no animation for entering doors.
	private IEnumerator DoAnimation()
	{
		yield return new WaitForSeconds(0.3f);

		m_levelManager.CheckpointReached = false;

		if(Application.loadedLevelName.Contains("World2") || Application.loadedLevel == 14) 
			Application.LoadLevel(2);
		else if(Application.loadedLevel == 3 || Application.loadedLevel == 4)
			Application.LoadLevel(Application.loadedLevel + 1);
		else if(Application.loadedLevel == 13)
			Application.LoadLevel(14);
		else if(Application.loadedLevelName.Contains("Wind"))
			Application.LoadLevel(2);
		else
			Application.LoadLevel(1);
	}
}