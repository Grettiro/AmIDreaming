using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour
{
	private PlatformerCharacter2D player;
	private Platformer2DUserControl control;
	private CheckpointObject checkpoint;
	private DeathTracker deathCount;
	private bool dead = false;
	private Animator anim;
	private static int counter = 0;

	void Start ()
	{
		dead = false;
	}
	void Awake()
	{
		control = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
    	if(other.tag == "Player" && !dead) 
		{
			dead = true;
			if((player = GameObject.Find("Player").GetComponent<PlatformerCharacter2D>()) != null)
			{
				anim = player.getAnimator();
			}
			player.setDead(true);
			AudioControlLoop audioPitch = GameObject.Find("AudioController").GetComponent<AudioControlLoop>();
			audioPitch.pitchChangeUp();

			/*
			 * Play the death animation and let it play until continuing.
			 */


			StartCoroutine(DoAnimation(other));
		}
		/*
		 * Check for other tags, i.e. enemies that would be killed by environment
		 * as well and could remove them from the level.
		 */	
	}

	private IEnumerator DoAnimation(Collider2D other)
	{

		other.GetComponent<Rigidbody2D>().isKinematic = true;
		anim.SetTrigger("Die");
		//checkpoint = GameObject.Find ("CheckPoint").GetComponent<CheckpointObject> ();
		yield return new WaitForSeconds(0.5f); // wait for two seconds.
		if(!Application.loadedLevelName.Contains("World"))
		{
			deathCount = GameObject.Find ("DeathTracker").GetComponent<DeathTracker> ();
			deathCount.Deaths += 1;
			counter++;
			control.LogDeath(counter);
		}
		/*if (checkpoint.GetComponent<CheckpointObject>().IsCheckpoint) 
		{
			other.transform.position = checkpoint.GetComponent<CheckpointObject>().Checkpoint;
		}*/
		Application.LoadLevel(Application.loadedLevelName);
	}
}
