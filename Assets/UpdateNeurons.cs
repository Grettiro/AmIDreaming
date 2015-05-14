using UnityEngine;
using System.Collections;

public class UpdateNeurons : MonoBehaviour
{
	private Platformer2DUserControl control;
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
