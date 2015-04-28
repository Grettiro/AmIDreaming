using UnityEngine;
using System.Collections;

public class EnemyCircleMovement : MonoBehaviour {

	public float speed;
	void Update()
	{
		transform.Rotate (Vector3.forward * (Time.deltaTime*speed));
	}

	public float Speed
	{
		get {return speed; }
		set {speed = value; }
	}

}
