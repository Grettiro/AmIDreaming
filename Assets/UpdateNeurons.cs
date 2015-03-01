using UnityEngine;
using System.Collections;

public class UpdateNeurons : MonoBehaviour
{
	public int nIndex;
	void Start()
	{
		var findNeuron = GameObject.Find("NeuronTracker");
		var getNeuron = (NeuronTracker)findNeuron.GetComponent("NeuronTracker");
		if (getNeuron.returnNeurons (nIndex))
						Destroy (this.gameObject);
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "Player")
		{
			var audioPlay = GameObject.Find("AudioController");
			var neuronAudio = (AudioControlLoop)audioPlay.GetComponent("AudioControlLoop");
			neuronAudio.playNeuron();
			var findNeurons = GameObject.Find("PlayerNeurons");
			var updateNeurons = (NeuronCount)findNeurons.GetComponent("NeuronCount");
			updateNeurons.Neurons += 1;
			var findNeurons2 = GameObject.Find("LevelFinish");
			var updateNeurons2 = (LevelFinish)findNeurons2.GetComponent("LevelFinish");
			updateNeurons2.setNeuronStatus(nIndex);
			Destroy (this.gameObject);
		}
	}
}
