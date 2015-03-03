using UnityEngine;
using System.Collections;

public class EnemyCircleMovement : MonoBehaviour {

	public float speed;
	void Update()
	{
		transform.Rotate (Vector3.forward * (Time.deltaTime*speed));
	}/*public float speed;
	float timeCounter = 0;
	public float width;
	public float height;
	Vector2 originalPosition;
	
	// Use this for initialization
	void Start()
	{
		originalPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		timeCounter += Time.deltaTime*speed;
		Debug.Log (Mathf.Sin (timeCounter * 0.5f * Mathf.PI) * width);
		float x = Mathf.Sin (timeCounter * 0.5f * Mathf.PI) * width;
		float y = Mathf.Cos (timeCounter * 0.5f * Mathf.PI) * height;

		transform.position = new Vector2 (originalPosition.x + x, originalPosition.y + y);
	}*/
}
