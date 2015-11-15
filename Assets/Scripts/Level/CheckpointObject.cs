using UnityEngine;
using System.Collections;

public class CheckpointObject : MonoBehaviour
{
	public bool m_gravityFlipped = false;
	private static bool m_checkpointReached = false;

	private PlatformerCharacter2D m_character;
	private LevelManager m_levelManager;
	private GameObject m_gameManager;

	private static CheckpointObject m_instance = null;
	public static CheckpointObject Instance 
	{
		get { return m_instance; }
	}

	void Awake ()
	{
		m_character = GameObject.Find("Player").GetComponent<PlatformerCharacter2D>();
		m_levelManager = m_character.GetLevelManager();
		m_gameManager = GameObject.Find("GameManager");

		if(m_levelManager.CheckpointReached)
		{
			Destroy(this.gameObject);
			return;
		}

		if(m_instance != null && m_instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
			m_instance = this;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag.Equals("Player"))
		{
			m_levelManager.CharacterPosition = this.transform.position;
			m_levelManager.CheckpointReached = true;
			m_levelManager.CheckpointGravity = m_gravityFlipped;

			// If neuron is set (has been collected), register it in the neuron array. bool flag is for a check if it's a level finish
			// event, since it't not we pass false to the method.
			NeuronTracker neuronTracker = m_gameManager.GetComponent<NeuronTracker>();
			if(neuronTracker.isSet())
				neuronTracker.UpdateNeuronArray(false);

			Destroy(this.gameObject);
		}
	}
}
