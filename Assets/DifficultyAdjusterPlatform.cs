using UnityEngine;
using System.Collections;

public class DifficultyAdjusterPlatform : MonoBehaviour {

	private bool scaled = false;
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DeathTracker difficulty = GameObject.Find ("DeathTracker").GetComponent<DeathTracker> ();
		if (difficulty.Difficulty < 10) {
			Scale ();
		}
	}

	private void Scale() 
	{
		if (!scaled) 
		{
			if(this.name.Contains("Killzone"))
			{
				this.transform.position = new Vector3(this.transform.position.x -1f, this.transform.position.y, 0);
				this.transform.localScale -= new Vector3(1f, 0, 0);
			}
			else
			{
				this.transform.position = new Vector3(this.transform.position.x -5f, this.transform.position.y, 0);
				this.transform.localScale += new Vector3(5f, 0, 0);
			}

		}
		scaled = true;
	}
}
