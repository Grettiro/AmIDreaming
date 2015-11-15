using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour
{
	private Platformer2DUserControl m_control;
	private PlatformerCharacter2D m_character;
	private LevelManager m_levelManager;
	private NeuronTracker m_neuronTracker;
	private DeathTracker m_deathCount;
	// Used for logging purposes.
	//private static int m_counter = 0;

	void Awake()
	{
		GameObject player = GameObject.Find("Player");
		GameObject gameManager = GameObject.Find("GameManager");

		m_control = player.GetComponent<Platformer2DUserControl>();
		m_character = player.GetComponent<PlatformerCharacter2D>();
		m_levelManager = m_character.GetLevelManager();
		m_neuronTracker = gameManager.GetComponent<NeuronTracker>();
		m_deathCount = gameManager.GetComponent<DeathTracker> ();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
    	if(other.tag == "Player" && !m_control.Dead) 
			m_control.Dead = true;

		// Play the death animation and let it play until continuing.
		StartCoroutine(DoAnimation(other));/*
		 * Check for other tags, i.e. enemies that would be killed by environment
		 * as well and could remove them from the level. Play their death animations.
		 */	
	}

	private IEnumerator DoAnimation(Collider2D other)
	{
		if(other.tag.Equals("Player"))
		{
			other.GetComponent<Rigidbody2D>().isKinematic = true;
			other.GetComponent<Animator>().SetTrigger("Die");

			yield return new WaitForSeconds(0.5f);

			if(!Application.loadedLevelName.Contains("Overworld") && !Application.loadedLevelName.Contains("Tutorial"))
			{
				// Reset neuron if the neuron has been collected and a checkpoint hasn't been reached.
				// bool flag is for whether it is a level select menu option or not, since it's a death reset, we put false.
				m_neuronTracker.ResetNeuron(false);
				// Increase death counters
				m_deathCount.Deaths += 1;
				m_levelManager.TotalDeaths += 1;
				// Logging
				//m_counter++;
				//control.LogDeath(m_counter);
			}

			Application.LoadLevel(Application.loadedLevelName);
		}
	}
}
