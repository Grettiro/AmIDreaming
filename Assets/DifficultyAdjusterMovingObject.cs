using UnityEngine;
using System.Collections;

public class DifficultyAdjusterMovingObject : MonoBehaviour {

	private bool scaled = false;

	// Use this for initialization
	void Awake () {
		ScaleSpeed ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	private void ScaleSpeed() {
		DeathTracker difficulty = GameObject.Find ("DeathTracker").GetComponent<DeathTracker> ();
		MovingObject speed = gameObject.GetComponent<MovingObject> ();
		if (!scaled) {
			speed.speed -= ((10 - difficulty.Difficulty)/1.5f);
		}
		scaled = true;
	}
}
