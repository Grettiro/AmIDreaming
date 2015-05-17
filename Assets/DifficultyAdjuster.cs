using UnityEngine;
using System.Collections;

public class DifficultyAdjuster : MonoBehaviour {

	private bool scaled = false;
	// Use this for initialization
	void Awake () {
		Scale ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void Scale(){
		if (!scaled) {
			DeathTracker difficulty = GameObject.Find ("DeathTracker").GetComponent<DeathTracker> ();
			if(this.name == "KillzoneTop")
			{
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + ((10-difficulty.Difficulty)/4), 0);
			}
			else if(this.name == "KillzoneBot")
			{
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - ((10-difficulty.Difficulty)/4), 0);
			}
			else if(this.name == "KillzoneLeft")
			{
				this.transform.position = new Vector3 (this.transform.position.x - ((10-difficulty.Difficulty)/4) , this.transform.position.y, 0);
			}
			else if(this.name == "KillzoneRight")
			{
				this.transform.position = new Vector3 (this.transform.position.x + ((10-difficulty.Difficulty)/4), this.transform.position.y, 0);
			}
			
			scaled = true;
		}
	}
}

