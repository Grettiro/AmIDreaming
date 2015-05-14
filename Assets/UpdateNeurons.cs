using UnityEngine;
using System.Collections;

public class UpdateNeurons : MonoBehaviour
{
<<<<<<< HEAD
	private Platformer2DUserControl control;
	public Rect winRect = new Rect(200, 200, 240, 100);
	public string tutText = "This is a neuron, collect these neurons to open up new levels and unlock new abilities!" ;
	public bool textAllow = false;

=======
>>>>>>> parent of 38a5ae4... The great commit of commits
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
		if(other.name == "Player")
		{
			AudioControlLoop neuronAudio = GameObject.Find("AudioController").GetComponent<AudioControlLoop>();
			neuronAudio.playNeuron();

			LevelFinish updateNeurons = GameObject.Find("LevelFinish").GetComponent<LevelFinish>();
			updateNeurons.setNeuronStatus(nIndex);

			control.neuron = true;

			Destroy(this.gameObject);
		}
	}
}
