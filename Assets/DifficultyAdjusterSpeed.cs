using UnityEngine;
using System.Collections;

public class DifficultyAdjusterSpeed : MonoBehaviour {

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
		EnemyMovement speed = gameObject.GetComponent<EnemyMovement> ();
		if (!scaled) {
			speed.speed.y -= ((10 - difficulty.Difficulty));
		}
		scaled = true;
	}
}
