using UnityEngine;
using System.Collections;

public class LevelFinish : MonoBehaviour
{
	public GameObject other;

	private void OnTriggerEnter2D(Collider2D other)
	{
		/*int currLevel = Application.loadedLevel;

		Destroy(other);
		if (other.tag == "Player") 
		{
			if(Application.levelCount == currLevel+1)
			{
				Application.LoadLevel (0);
			}
			else if(Application.loadedLevel == 6)
			{
				Application.LoadLevel (0);
			}
			else
				Application.LoadLevel (13);
		}
		/*if (other.tag == "Player") 
		{
			if(Application.levelCount == currLevel+1)
				Application.LoadLevel(0);
			else if(Application.loadedLevel == 6)
				Application.LoadLevel(0);
			else if(Application.loadedLevel == 10)
				Application.LoadLevel(0);
			else
				Application.LoadLevel(currLevel + 1);
		}*/
		if (other.tag == "Player") 
		{
				Application.LoadLevel(12);
		}
	}
}
