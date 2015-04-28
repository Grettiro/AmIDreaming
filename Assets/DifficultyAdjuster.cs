using UnityEngine;
using System.Collections;

public class DifficultyAdjuster : MonoBehaviour {

	private bool scaled = false;
	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
		DeathTracker difficulty = GameObject.Find ("DeathTracker").GetComponent<DeathTracker> ();
		if (difficulty.Difficulty < 5) {
			Scale ();
		}
	}

	private void Scale(){
		if (!scaled) {

			if(this.name == "KillzoneTop")
			{
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 1f, 0);
			}
			else if(this.name == "KillzoneBot")
			{
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - 1f, 0);
			}
			else if(this.name == "KillzoneLeft")
			{
				this.transform.position = new Vector3 (this.transform.position.x - 1f , this.transform.position.y, 0);
			}
			else if(this.name == "KillzoneRight")
			{
				this.transform.position = new Vector3 (this.transform.position.x + 1f, this.transform.position.y, 0);
			}
			
			scaled = true;
		}
	}
}

