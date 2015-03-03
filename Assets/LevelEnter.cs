using UnityEngine;
using System.Collections;

public class LevelEnter : MonoBehaviour 
{
	public int levelNumber;
	public int neuronsRequired;
	bool nextLevel;

	void OnTriggerEnter2D(Collider2D other)
	{
		var findNeurons = GameObject.Find("PlayerNeurons");
		var getNeurons = (NeuronCount)findNeurons.GetComponent("NeuronCount");

		if(getNeurons.Neurons >= neuronsRequired)
			nextLevel = true;
		else
			Debug.Log("Not enough Neurons");
	}

	void OnTriggerStay2D(Collider2D other)
	{
		var findNeurons = GameObject.Find("PlayerNeurons");
		var getNeurons = (NeuronCount)findNeurons.GetComponent("NeuronCount");

		if (getNeurons.Neurons >= neuronsRequired)
			nextLevel = true;
		else
			Debug.Log("Not enough Neurons");
	}

	void OnTriggerExit2D(Collider2D other)
	{
		nextLevel = false;
	}

	void Update()
	{
		if(nextLevel)
			if (Input.GetKeyDown ("return"))
			{
				var findNeurons = GameObject.Find("PlayerNeurons");
				var setLevel = (NeuronCount)findNeurons.GetComponent("NeuronCount");
				setLevel.GetPrevLevel = Application.loadedLevel;
				Application.LoadLevel (levelNumber);
			}
	}
}
