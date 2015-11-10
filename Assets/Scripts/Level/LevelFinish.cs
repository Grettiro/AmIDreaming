using UnityEngine;
using System.Collections;
	
public class LevelFinish : MonoBehaviour
{
	private PlatformerCharacter2D player;
	private Platformer2DUserControl control;
	private NeuronTracker neuronTracker;

	private Rigidbody2D m_rigidbody;
	private bool finished = false;

	private GameObject checkpoint;
	private CheckpointObject setPos;

	private void Awake()
	{
		control = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
		neuronTracker = GameObject.Find("GameManager").GetComponent<NeuronTracker>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag.Equals("Player") && !finished)
		{
			finished = true;
			if((player = GameObject.Find("Player").GetComponent<PlatformerCharacter2D>()) != null)
			{
				control.Move = false;

				DeathTracker difficulty = GameObject.Find ("GameManager").GetComponent<DeathTracker> ();

				if(difficulty.Deaths < difficulty.DeathMarker / 2)
					if(difficulty.Difficulty < 10)
						difficulty.Difficulty += 1;
			}

			if(neuronTracker.isSet()) 
				neuronTracker.UpdateNeuronArray();

			//control.LogExit(true);

			other.GetComponent<Rigidbody2D>().isKinematic = true;
			StartCoroutine(DoAnimation());
		}
	}
	
	private IEnumerator DoAnimation()
	{
		yield return new WaitForSeconds(0.3f); // wait for two seconds.
		checkpoint = GameObject.FindGameObjectWithTag("Checkpoint");

		if (checkpoint != null)
		{
			setPos = checkpoint.GetComponent<CheckpointObject>();
			setPos.IsCheckpoint = false;
			Destroy(checkpoint);
		}

		if (Application.loadedLevelName.Contains("Medium") || Application.loadedLevel == 14) 
			Application.LoadLevel (2);
		else if (Application.loadedLevel == 3 || Application.loadedLevel == 4)
			Application.LoadLevel(Application.loadedLevel + 1);
		else if (Application.loadedLevel == 13)
			Application.LoadLevel(14);
		else if (Application.loadedLevelName.Contains("Wind"))
			Application.LoadLevel(2);
		else
			Application.LoadLevel (1);
	}
}