using UnityEngine;
using System.Collections;

public class DifficultyAdjusterPlatform : MonoBehaviour {

	private bool scaled = false;
	// Use this for initialization
	void Awake () {
		Scale ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void Scale() 
	{
		if (!scaled) 
		{
			DeathTracker difficulty = GameObject.Find ("DeathTracker").GetComponent<DeathTracker> ();
			if(this.name.Contains("Killzone"))
			{
				this.transform.position = new Vector3(this.transform.position.x -((10-difficulty.Difficulty)/10), this.transform.position.y, 0);
				this.transform.localScale -= new Vector3(((10-difficulty.Difficulty)/10), 0, 0);
			}
			else
			{
				this.transform.position = new Vector3(this.transform.position.x -((10-difficulty.Difficulty)/5), this.transform.position.y, 0);
				this.transform.localScale += new Vector3(((10-difficulty.Difficulty)/5), 0, 0);
			}

		}
		scaled = true;
	}
}
