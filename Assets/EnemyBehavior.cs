using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public Vector2 speed = new Vector2(8.0f, 8.0f);

	// Use this for initialization
	void Start () 
	{
		rigidbody2D.velocity = speed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.rigidbody2D.position.y > 10.5) 
		{
			rigidbody2D.velocity = speed * -1;
		} 
		else if(this.rigidbody2D.position.y < -3.5)
		{
			rigidbody2D.velocity = speed;
		}
	}
}
