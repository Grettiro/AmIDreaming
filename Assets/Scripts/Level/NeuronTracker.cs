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
	private CheckpointObject m_checkpointObj;

	private static NeuronTracker m_instance = null;
	public static NeuronTracker m_Instance 
	{
		get { return m_instance; }
	}
	
	void Awake()
	{
		if(!Application.loadedLevelName.Contains("World") && !Application.loadedLevelName.Contains("Tutorial"))
		{
			m_neuron = GameObject.Find("Neuron").GetComponent<UpdateNeurons>();
			GameObject checkpoint = GameObject.FindGameObjectWithTag("Checkpoint");
			if(checkpoint != null)
				m_checkpointObj = GameObject.Find("CheckPoint").GetComponent<CheckpointObject>();
		}

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

	public void UpdateNeuronArray()
	{
		// Only update neuron count if it hasn't been increased already.
		if(!m_neuronIncreased)
		{
			m_neurons[m_neuronIndex] = true;
			m_collectedNeurons++;
			m_neuronIncreased = true;
			m_neuronSet = false;
			Debug.Log("Neuron count increased to " + m_collectedNeurons);
		}
	}

	public bool ReturnNeurons(int index)
	{
		if(index >= 0 && index < m_totalNeurons)
			return m_neurons[index];
		else
			return false;
	}

	public void ResetNeuron()
	{
		/* Do not reset neuron if it's been collected before reaching a checkpoint.
		 * Either figure out neuron's position relative to a checkpoint or just have a bool check?
		 * Start with bool check, maybe later on have it position based if we decide to have more
		 * than one neuron per level.
		 */

		// If the neuron count has been increased and a checkpoint has NOT been reached, 
		// reset the neuron and decrease the counter.
		bool checkpoint;
		if(m_checkpointObj == null)
			checkpoint = false;
		else
			checkpoint = m_checkpointObj.IsCheckpoint;

		if(m_neuron != null)
			return;
		else if(m_neuronSet)
		{
			m_neuronSet = false;
			m_neurons[m_neuron.m_neuronIndex] = false;
		}
		else if(m_neuronIncreased && !checkpoint)
		{
			m_neuronIncreased = false;
			m_neuronSet = false;
			m_collectedNeurons--;
			m_neurons[m_neuron.m_neuronIndex] = false;
			Debug.Log("Neuron count decreased to " + m_collectedNeurons);
		}
	}

	public bool isSet()
	{
		return m_neuronSet;
	}

	public int Neurons
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
