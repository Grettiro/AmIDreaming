using UnityEngine;
using System.Collections;

public class UpdateNeurons : MonoBehaviour
{
	public int nIndex;
	void Start()
	{
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

			Destroy(this.gameObject);
		}
	}
}
