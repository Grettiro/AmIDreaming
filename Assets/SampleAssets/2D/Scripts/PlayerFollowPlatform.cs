using UnityEngine;
using System.Collections;

public class PlayerFollowPlatform : MonoBehaviour 
{
	public Vector3 previousPosition;
	public Vector3 currentPosition;
	public Vector2 v2CurrentPosition;

	void FixedUpdate()
	{
		previousPosition = transform.position;
	}
	
	void LateUpdate()
	{
		currentPosition = (previousPosition - transform.position);
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
			coll.gameObject.transform.position += -currentPosition;
	}

}
