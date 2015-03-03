using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour
{
	private PlatformerCharacter2D player;
	private Animator anim;

	void Awake()
	{
		player = GameObject.Find("Player").GetComponent<PlatformerCharacter2D>();
		anim = player.getAnimator();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
    	if(other.tag == "Player") 
		{

			var audioStop = GameObject.Find("AudioController");
			var audioPitch = (AudioControlLoop)audioStop.GetComponent("AudioControlLoop");
			audioPitch.pitchChangeUp();

			/*
			 * Play the death animation and let it play until continuing.
			 */
			StartCoroutine(DoAnimation());
		}
		/*
		 * Check for other tags, i.e. enemies that would be killed by environment
		 * as well and could remove them from the level.
		 */
	}

	private IEnumerator DoAnimation()
	{
		anim.Play("Death");
		yield return new WaitForSeconds(1); // wait for two seconds.
		Application.LoadLevel(Application.loadedLevelName);
	}
}
