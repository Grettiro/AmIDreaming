using UnityEngine;
using System.Collections;

public class NeuronTracker : MonoBehaviour {

	// Use this for initialization
	private static int m_totalNeurons = 17;
	private bool[] m_neurons = new bool[m_totalNeurons];

	private int m_neuronIndex;
	private bool m_neuronSet = false;
	private bool m_neuronIncreased = false;

	private int m_collectedNeurons = 0;
	private int m_prevLevel;

	private UpdateNeurons m_neuron;
	private PlatformerCharacter2D m_character;
	private LevelManager m_levelManager;

	private static NeuronTracker m_instance = null;
	public static NeuronTracker m_Instance 
	{
		get { return m_instance; }
	}
	
	void Awake()
	{
		if(!Application.loadedLevelName.Contains("Overworld") && !Application.loadedLevelName.Contains("Tutorial"))
			m_neuron = GameObject.Find("Neuron").GetComponent<UpdateNeurons>();

		m_character = GameObject.Find("Player").GetComponent<PlatformerCharacter2D>();
		m_levelManager = m_character.GetLevelManager();

		if(m_instance != null && m_instance != this)
		{
			Destroy (this.gameObject);
			return;
		}
		else
			m_instance = this;

		DontDestroyOnLoad(this.gameObject);	
	}

	public void SetNeuron(int index)
	{
		if(index >= 0 && index < m_totalNeurons)
		{
			m_neuronIndex = index;
			m_neuronSet = true;
			m_neuronIncreased = false;
		}
		else
			Debug.Log("Index out of bounds! Neuron index: " + index);
	}

	public void UpdateNeuronArray(bool levelFinish)
	{
		// Only update neuron count if it hasn't been increased already. To avoid possible weird bugs.
		if(!m_neuronIncreased)
		{
			m_neurons[m_neuronIndex] = true;
			m_collectedNeurons++;

			// Only set neuron increased to true if the call did not come from a level finish event.
			// Avoids storing it as increased when you enter the next level.
			if(!levelFinish)
				m_neuronIncreased = true;

			m_neuronSet = false;
		}
	}

	public bool ReturnNeurons(int index)
	{
		if(index >= 0 && index < m_totalNeurons)
			return m_neurons[index];
		else
			return false;
	}

	public void ResetNeuron(bool levelSelect)
	{
		// If being called from a level select menu option, decrease neuron count if it has been increased.
		if(levelSelect)
		{
			if(m_neuronIncreased)
			{
				m_neuronIncreased = false;
				m_neuronSet = false;
				m_collectedNeurons--;
				m_neurons[m_neuronIndex] = false;
			}

			return;
		}

		// If the neuron has yet to be collected, do nothing.
		if(m_neuron != null)
			return;
		// If the neuron has been set but not registered, reset it!
		else if(m_neuronSet)
		{
			m_neuronSet = false;
			m_neurons[m_neuronIndex] = false;
		}
	}

	public bool isSet()
	{
		return m_neuronSet;
	}

	public int CollectedNeurons
	{
		get { return m_collectedNeurons; }
		set { m_collectedNeurons = value; }
	}
	
	public int GetPrevLevel
	{
		get { return m_prevLevel;}
		set { m_prevLevel = value;}
	}
}
