using UnityEngine;
using System.Collections;

public class PlayerWindZone : MonoBehaviour {

	public int windSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			Debug.Log ("Derp");
			other.GetComponent<Rigidbody2D>().AddForce(Vector2.right * windSpeed);
		}
	}
}
