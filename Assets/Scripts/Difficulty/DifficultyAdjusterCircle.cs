using UnityEngine;
using System.Collections;

public class DifficultyAdjusterCircle : MonoBehaviour {

	private bool scaled = false;
	private bool sizeScaled = false;
	private EnemyCircleMovement speed;
	
	// Use this for initialization
	void Awake () {
		ScaleSpeed ();
		ScaleObjects();
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void ScaleSpeed()
	{
		DeathTracker difficulty = GameObject.Find("GameManager").GetComponent<DeathTracker> ();
		speed = gameObject.GetComponent<EnemyCircleMovement> ();
		if (!scaled) {
			speed.speed -= (10 - difficulty.Difficulty) * 5;
		}
		scaled = true;
	}
	private void ScaleObjects()
	{
		DeathTracker difficulty = GameObject.Find("GameManager").GetComponent<DeathTracker> ();
		if (!sizeScaled) {
			foreach (Transform child in transform) {
				if (child.name == "KillzoneOpeningBot") {
					child.transform.position = new Vector3 (child.transform.position.x, child.transform.position.y  - ((10-difficulty.Difficulty)/4), 0);
				} else if (child.name == "KillzoneOpeningTop") {
					child.transform.position = new Vector3 (child.transform.position.x, child.transform.position.y  + ((10-difficulty.Difficulty)/4), 0);
				}
			}
			sizeScaled = true;
		}
		}
}