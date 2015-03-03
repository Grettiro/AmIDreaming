using UnityEngine;
using System.Collections;

public class LevelEnter : MonoBehaviour 
{
	public int levelNumber;
	public int neuronsRequired;
	private bool nextLevel;

	private NeuronCount getNeurons;

	void Awake()
	{
		getNeurons = GameObject.Find("PlayerNeurons").GetComponent<NeuronCount>();
	}

	void OnTriggerStay2D(Collider2D other)
	{
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
			if (Input.GetKeyDown ("return") || Input.GetButtonDown("Submit"))
			{
				getNeurons.GetPrevLevel = Application.loadedLevel;
				Application.LoadLevel(levelNumber);
			}
	}
}
