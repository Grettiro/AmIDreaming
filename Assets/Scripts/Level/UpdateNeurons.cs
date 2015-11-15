using UnityEngine;
using System.Collections;

public class UpdateNeurons : MonoBehaviour
{
	private Platformer2DUserControl m_control;
	private NeuronTracker m_neuronTracker;

	private AudioManager m_audioManager;

	public int m_neuronIndex;
	public AudioClip m_neuronCollected;

	// Temporary, will be removed when the tutorial has been integrated into the actual game. Won't have dedicated
	// tutorial levels.
	Rect winRect = new Rect(200, 200, 240, 100);
	public string tutText = "This is a neuron, collect these neurons to open up new levels and unlock new abilities!" ;
	public bool textAllow = false;

	void Start()
	{
		m_control = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
		m_neuronTracker = GameObject.Find("GameManager").GetComponent<NeuronTracker>();
		m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

		// If the neuron has been collected, destroy the game object.
		if(m_neuronTracker.ReturnNeurons(m_neuronIndex))
			Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name.Equals("Player"))
		{
			// Temporary for tutorial text.
			if(this.name.Equals("TutorialNeuron") && Application.loadedLevelName.Equals("Tutorial1"))
			{
				m_control.Move = false;
				textAllow = true;
				other.attachedRigidbody.isKinematic = true;

				StartCoroutine(Delay(other));
			}
			else
			{
				m_audioManager.PlayNeuron();
				// Reduntant check, but better be safe than sorry?
				if(this.name.Equals("Neuron"))
					m_neuronTracker.SetNeuron(m_neuronIndex);

				// Used for logging. Unused at the moment.
				//m_control.neuron = true;

				Destroy(this.gameObject);
			}
		}
	}

	// Coroutine and GUI methods used for displaying tutorial text.
	private IEnumerator Delay(Collider2D other)
	{
		yield return new WaitForSeconds(3f);
		m_audioManager.PlayNeuron();
		m_control.Move = true;
		other.attachedRigidbody.isKinematic = false;
		textAllow = false;
		Destroy(this.gameObject);
	}
	
	private void OnGUI()
	{
		if(textAllow)
		{
			winRect = GUILayout.Window( 0, winRect, WindowFunction, "Game Paused" );
			winRect.x = (int) ( Screen.width * 0.5f - winRect.width * 0.5f );
			winRect.y = (int) ( Screen.height * 0.5f - winRect.height * 1.5f );
			
			GUILayout.Window( 0, winRect, WindowFunction, "General Movement" );
		}
	}
	void WindowFunction(int windowID)
	{
		GUILayout.Label(tutText);
	}
}
