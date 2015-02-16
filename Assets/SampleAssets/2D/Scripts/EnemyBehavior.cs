using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public int speed = 8;
	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = new Vector2(speed,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (this.rigidbody2D.transform.position.x > 88) {
				rigidbody2D.velocity = new Vector2(speed *-1,0);
				} 
		else if(this.rigidbody2D.transform.position.x < 69.4)
		{
			rigidbody2D.velocity = new Vector2(speed,0);
		}
	
	}
}
