using UnityEngine;
using System.Collections;

public class LevelEnter : MonoBehaviour 
{
	public int levelNumber;
	public int neuronsRequired;
	private bool nextLevel;

	private Platformer2DUserControl control;
	private NeuronCount getNeurons;

	void Awake()
	{
		control = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
		getNeurons = GameObject.Find("PlayerNeurons").GetComponent<NeuronCount>();
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
			if(Input.GetButtonDown("Enter")) // Enter or Circle button
			{
				control.startTimer();
				getNeurons.GetPrevLevel = Application.loadedLevel;
				Application.LoadLevel(levelNumber);
			}
	}
}
