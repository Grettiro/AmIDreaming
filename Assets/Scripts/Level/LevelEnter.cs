using UnityEngine;
using System.Collections;

public class LevelEnter : MonoBehaviour 
{
	public int levelNumber;
	public int neuronsRequired;
	private bool nextLevel;

	private NeuronTracker getNeurons;

	void Awake()
	{
		getNeurons = GameObject.Find("GameManager").GetComponent<NeuronTracker>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (getNeurons.Neurons < neuronsRequired)
		{
			UIUpdate update = GameObject.Find("Enough").GetComponent<UIUpdate>();
			update.notEnoughNeurons(neuronsRequired);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (getNeurons.Neurons >= neuronsRequired)
			nextLevel = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		nextLevel = false;
	}

	void Update()
	{
		if(nextLevel)
			if (Input.GetButtonDown("Enter")) // Enter or Circle button
			{
				getNeurons.GetPrevLevel = Application.loadedLevel;
				Application.LoadLevel(levelNumber);
			}
	}
}
