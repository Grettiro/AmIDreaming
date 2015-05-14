using UnityEngine;
using System.Collections;

public class DifficultyAdjusterCircle : MonoBehaviour {

	private bool scaled = false;
	private bool sizeScaled = false;
	
	
	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		DeathTracker difficulty = GameObject.Find ("DeathTracker").GetComponent<DeathTracker> ();
		if (difficulty.Difficulty < 5) {
			ScaleSpeed();
		}
		if (difficulty.Difficulty < 4) {
			ScaleObjects();
		}
	}

	private void ScaleSpeed() {
		EnemyCircleMovement speed = this.gameObject.GetComponent<EnemyCircleMovement> ();
		if (!scaled)
			speed.speed -= 30;
		scaled = true;
	}
	private void ScaleObjects()
	{
		if (!sizeScaled) {
			foreach (Transform child in transform) {
				if (child.name == "KillzoneOpeningBot") {
					Debug.Log ("BOT TRANSFORMED!");
					child.transform.position = new Vector3 (child.transform.position.x, child.transform.position.y  - 1f, 0);
				} else if (child.name == "KillzoneOpeningTop") {
					Debug.Log ("TOP TRANSFORMED!");
					child.transform.position = new Vector3 (child.transform.position.x, child.transform.position.y  + 1f, 0);
				}
			}
			sizeScaled = true;
		}
		}
}