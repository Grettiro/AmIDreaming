using UnityEngine;

public class Restarter : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
    	if (other.tag == "Player") 
		{
			Application.LoadLevel (Application.loadedLevelName);
		}
		/*
		 * Check for other tags, i.e. enemies that would be killed by environment
		 * as well and could remove them from the level.
		 */
	}
}
