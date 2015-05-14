using UnityEngine;
using System.Collections;

public class UpdateNeurons : MonoBehaviour
{
	private Platformer2DUserControl control;
	public Rect winRect = new Rect(200, 200, 240, 100);
	public string tutText = "This is a neuron, collect these neurons to open up new levels and unlock new abilities!" ;
	public bool textAllow = false;

	public int nIndex;
	void Start()
	{
		control = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
		NeuronTracker getNeuron = GameObject.Find("NeuronTracker").GetComponent<NeuronTracker>();
		if (getNeuron.returnNeurons(nIndex))
						Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (this.name == "TutorialNeuron") {
			Platformer2DUserControl abilities = GameObject.Find ("Player").GetComponent<Platformer2DUserControl> ();
			abilities.Move = false;
			textAllow = true;
			other.attachedRigidbody.isKinematic = true;
			StartCoroutine(Delay(other));
		}
		else if(other.name == "Player")
		        {
			AudioControlLoop neuronAudio = GameObject.Find("AudioController").GetComponent<AudioControlLoop>();
			neuronAudio.playNeuron();
			
			LevelFinish updateNeurons = GameObject.Find("LevelFinishDoor").GetComponent<LevelFinish>();
			updateNeurons.setNeuronStatus(nIndex);

			control.neuron = true;

			Destroy(this.gameObject);
		}
	}

	private IEnumerator Delay(Collider2D other)
	{

			yield return new WaitForSeconds(3.5f);
			Platformer2DUserControl abilities = GameObject.Find ("Player").GetComponent<Platformer2DUserControl> ();
			abilities.Move = true;
			other.attachedRigidbody.isKinematic = false;
			textAllow = false;
			AudioControlLoop neuronAudio = GameObject.Find("AudioController").GetComponent<AudioControlLoop>();
			neuronAudio.playNeuron();
			Destroy (this.gameObject);
	}
	
	private void OnGUI()
	{
		if (textAllow) {
			winRect = GUILayout.Window( 0, winRect, WindowFunction, "Game Paused" );
			winRect.x = (int) ( Screen.width * 0.5f - winRect.width * 0.5f );
			winRect.y = (int) ( Screen.height * 0.5f - winRect.height * 1.5f );
			
			GUILayout.Window( 0, winRect, WindowFunction, "General Movement" );
		}
	}
	void WindowFunction(int windowID) {
		GUILayout.Label(tutText);
	}
}
