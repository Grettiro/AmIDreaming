using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingObject : MonoBehaviour {
	private bool moving;

	public float speed = 0.0f;
	
	private List<Point> points = new List<Point>();
	private int listSize = 0;
	private Point[] pointArray;
	private int counter = 0;
	private bool reverse = false;
	// Use this for initialization
	void Awake () {
		points.AddRange(gameObject.GetComponentInParent<Platform>().GetComponentsInChildren<Point>());
		listSize = points.Count;
		pointArray = points.ToArray();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("d") || Input.GetKeyDown ("e") || Input.GetKeyDown ("right") || Input.GetButtonDown ("Jump") || Input.GetKeyDown("left")) 
		{
			moving = true;
		}
		if(moving)
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
}
