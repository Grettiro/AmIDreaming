using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public int speed = 8;
	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = new Vector2(0, speed);
	}
	
	// Update is called once per frame
	void Update () {
		if (this.rigidbody2D.position.y > 16) {
				rigidbody2D.velocity = new Vector2(0, speed *-1);
				} 
		else if(this.rigidbody2D.position.y < 1.4)
		{
			rigidbody2D.velocity = new Vector2(0, speed);
		}
	
	}
}
