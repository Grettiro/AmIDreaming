using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIUpdate : MonoBehaviour
{
	Text txt;
	Text enoughNeuronsTxt;

	private NeuronTracker updateNeurons;

	public void notEnoughNeurons(int neurons)
	{
		enoughNeuronsTxt = GameObject.Find("Enough").GetComponent<Text>();
		enoughNeuronsTxt.text = "Not enough neurons!\n" + neurons + " Are needed to enter";
		StartCoroutine(Wait());
	}

	// Use this for initialization
	void Start()
	{
		updateNeurons = GameObject.Find("GameManager").GetComponent<NeuronTracker>();

		txt = GameObject.Find("Neurons").GetComponent<Text>(); 
		txt.text="Neurons: " + (0 + updateNeurons.CollectedNeurons);
	}
	
	// Update is called once per frame
	void Update()
	{
		txt = GameObject.Find("Neurons").GetComponent<Text>(); 
		txt.text="Neurons: " + (0 + updateNeurons.CollectedNeurons);
	}

	private IEnumerator Wait()
	{
		yield return new WaitForSeconds(1f); // wait for two seconds.
		enoughNeuronsTxt.text = "";
	}
}
