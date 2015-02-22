using UnityEngine;
using System.Collections;


namespace UnitySampleAssets._2D
{
	public class Doors : MonoBehaviour 
	{
		[SerializeField] private LayerMask reachedTop;

		private Transform topCheck;
		private float topRadius = .2f;
		private bool topped = false;

		private void Awake()
		{
			topCheck = transform.Find("TopCheck");
		}

		// Use this for initialization
		void Start() 
		{
		}

		void FixedUpdate()
		{
			topped = Physics2D.OverlapCircle(topCheck.position, topRadius, reachedTop);
		}
		
		// Update is called once per frame
		void Update() 
		{
			// When the doors have reached the top, make them stop opening.
			if(topped)
				this.OpenDoors (true);
		}

		public void OpenDoors(bool isTopped)
		{
			var doors = GameObject.Find("Doors");
			if (doors != null) 
			{
				if(!isTopped)
					doors.rigidbody2D.velocity = new Vector2(0.0f, 1.0f);
				else
					doors.rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
			}
		}
	}
}
