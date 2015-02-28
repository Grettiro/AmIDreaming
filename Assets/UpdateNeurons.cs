using UnityEngine;
using System.Collections;

public class UpdateNeurons : MonoBehaviour
{
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
			Destroy (this.gameObject);
		}
	}
}
