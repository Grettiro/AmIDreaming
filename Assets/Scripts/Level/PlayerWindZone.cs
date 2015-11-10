using UnityEngine;
using System.Collections;

public class PlayerWindZone : MonoBehaviour {

	public float windSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DeathTracker difficulty = GameObject.Find ("GameManager").GetComponent<DeathTracker> ();
		/*Debug.Log (1 - ((float)difficulty.Difficulty / 10));
		if(windSpeed >= 0)
			windSpeed = windSpeed - (1 - (float)difficulty.Difficulty / 10) * 100;
		else
			windSpeed = windSpeed + (float)difficulty.Difficulty / 10 * 100;*/
	}
	
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			Debug.Log(windSpeed);
			other.GetComponent<Rigidbody2D>().AddForce(Vector2.right * windSpeed);
		}
	}
}
