using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIUpdate : MonoBehaviour
{
	Text txt;

	private NeuronCount updateNeurons;

	// Use this for initialization
	void Start()
	{
		updateNeurons = GameObject.Find ("PlayerNeurons").GetComponent<NeuronCount>();

		txt = gameObject.GetComponent<Text>(); 
		txt.text="Neurons: " + (0 + updateNeurons.Neurons);
	}
	
	// Update is called once per frame
	void Update()
	{
		txt = gameObject.GetComponent<Text>(); 
		txt.text="Neurons: " + (0 + updateNeurons.Neurons);
	}
}
