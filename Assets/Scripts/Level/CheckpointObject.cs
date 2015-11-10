using UnityEngine;
using System.Collections;

public class CheckpointObject : MonoBehaviour
{
	/*
	 * TODO: Put gravity flip check, so if the checkpoint requires the character to start with gravity flipped
	 * it will, instead of starting with plummeting to their deaths.
	 * TODO: Maybe better to destroy the checkpoint object if it's been reached. Makes it easier when it comes
	 * to neurons, disable the possiblity of collecting a neuron and going back to the checkpoint. Could store the
	 * location on the player object itself. Update it when reaching a checkpoint and then resetting it to default
	 * upon exiting a level.
	 */
	private Vector3 m_checkpointPos = new Vector3(0, 0, 0);
	public float m_offsetY = 1;
	public float m_offsetX = 0;
	public bool m_gravityFlipped = false;
	private bool m_checkpoint = false;

	private PlatformerCharacter2D m_character;

	private static CheckpointObject m_instance = null;
	public static CheckpointObject Instance 
	{
		get { return m_instance; }
	}

	void Awake ()
	{
		m_character = GameObject.Find("Player").GetComponent<PlatformerCharacter2D>();

		if(m_instance != null && m_instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
			m_instance = this;
		
		DontDestroyOnLoad(this.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag.Equals("Player"))
		{
			// Set starting position of character to the position of the checkpoint, then destroy it.
			m_checkpoint = true;
			m_checkpointPos = this.transform.position;

			// Character of course gets destroyed and resets the starting position.. make a static game manager object to store
			// this and other level relevant data.
			m_character.StartingPosition = m_checkpointPos;//new Vector3(m_checkpointPos.x + m_offsetX, m_checkpointPos.y + m_offsetY, 0);

			DeathTracker difficulty = GameObject.Find("GameManager").GetComponent<DeathTracker>();
			difficulty.Deaths /= 2;

			NeuronTracker neuronTracker = GameObject.Find("GameManager").GetComponent<NeuronTracker>();

			// If neuron has been collected, update the neuron tracker
			if(neuronTracker.isSet())
				neuronTracker.UpdateNeuronArray();
		}
	}

	[SerializeField]
	public Vector3 Checkpoint
	{
		get { return new Vector3(m_checkpointPos.x + m_offsetX, m_checkpointPos.y + m_offsetY, 0); }
		set { m_checkpointPos = value; }
	}
	
	[SerializeField]
	public bool IsCheckpoint
	{
		get { return m_checkpoint; }
		set { m_checkpoint = value; }
	}
}
