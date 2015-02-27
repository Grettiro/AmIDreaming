using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIUpdate : MonoBehaviour
{
	Text txt;

	// Use this for initialization
	void Start()
	{
		var findNeurons = GameObject.Find ("PlayerNeurons");
		var updateNeurons = (NeuronCount)findNeurons.GetComponent("NeuronCount");

		txt = gameObject.GetComponent<Text>(); 
		txt.text="Neurons: " + (0 + updateNeurons.Neurons);
	}
	
	// Update is called once per frame
	void Update()
	{
		var findNeurons = GameObject.Find ("PlayerNeurons");
		var updateNeurons = (NeuronCount)findNeurons.GetComponent("NeuronCount");
		
		txt = gameObject.GetComponent<Text>(); 
		txt.text="Neurons: " + (0 + updateNeurons.Neurons);
	}
}
