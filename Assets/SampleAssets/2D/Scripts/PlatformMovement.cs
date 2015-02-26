using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformMovement : MonoBehaviour {
	public float speed = 0.0f;

	private List<Point> points = new List<Point>();
	private int listSize = 0;
	private Point[] pointArray;
	private int counter = 0;
	private bool reverse = false;

	// Use this for initialization
	void Start()
	{
	}

	private void Awake()
	{
		points.AddRange(gameObject.GetComponentInParent<Platform>().GetComponentsInChildren<Point>());
		listSize = points.Count;
		pointArray = points.ToArray();
	}

	void FixedUpdate()
	{
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.tag == "Player")
			other.transform.parent = this.transform;
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if(other.collider.tag == "Player")
			other.transform.parent = null;
	}

	// Update is called once per frame
	void Update()
	{
		if(!reverse)
		{
			this.transform.position = Vector3.MoveTowards (this.transform.position, pointArray[counter + 1].transform.position, (speed * Time.deltaTime));

			if(this.transform.position == pointArray [counter + 1].transform.position)
			{
				if(counter + 1 < listSize - 1)
					counter++;
				else
					reverse = true;
			}
		}
		if(reverse)
		{
			this.transform.position = Vector3.MoveTowards (this.transform.position, pointArray[counter].transform.position, (speed * Time.deltaTime));
			
			if(this.transform.position == pointArray [counter].transform.position)
			{
				if(counter != 0)
					counter--;
				else
					reverse = false;
			}
		}
	}
}
