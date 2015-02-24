using UnityEngine;
using System.Collections;

public class LevelFinish : MonoBehaviour
{
	public GameObject other;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
				Application.LoadLevel(12);
		}
	}
}
