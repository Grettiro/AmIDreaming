using UnityEngine;
using System.Collections;

public class UpdateNeurons : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		var findNeurons = GameObject.Find ("PlayerNeurons");
		var updateNeurons = (NeuronCount)findNeurons.GetComponent("NeuronCount");
		updateNeurons.Neurons += 1;
		Destroy (this.gameObject);
	}
}
