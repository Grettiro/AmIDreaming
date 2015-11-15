using UnityEngine;
using System.Collections;

public class LevelEnter : MonoBehaviour 
{
	public int m_levelNumber;
	public int m_neuronsRequired;

	private NeuronTracker m_neuronTracker;

	private bool m_isAccessible = false;

	private void Awake()
	{
		m_neuronTracker = GameObject.Find("GameManager").GetComponent<NeuronTracker>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag.Equals("Player"))
		{
			if(m_neuronTracker.CollectedNeurons < m_neuronsRequired)
			{
				UIUpdate update = GameObject.Find("Enough").GetComponent<UIUpdate>();
				update.notEnoughNeurons(m_neuronsRequired);
			}
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag.Equals("Player"))
			if(m_neuronTracker.CollectedNeurons >= m_neuronsRequired)
				m_isAccessible = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag.Equals("Player"))
			m_isAccessible = false;
	}

	private void Update()
	{
		if(m_isAccessible)
		{
			// Enter or circle.
			if(Input.GetButtonDown("Enter"))
			{
				m_neuronTracker.GetPrevLevel = Application.loadedLevel;
				Application.LoadLevel(m_levelNumber);
			}
		}
	}
}
