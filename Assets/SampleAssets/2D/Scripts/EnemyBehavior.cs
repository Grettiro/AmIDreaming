using UnityEngine;
using System.Collections;

namespace UnitySampleAssets._2D
{
	public class EnemyBehavior : MonoBehaviour {
		public Vector2 speed = new Vector2(0.0f, 0.0f);
		// Use this for initialization
		void Start () {
			rigidbody2D.velocity = speed;
		}
		
		// Update is called once per frame
		void Update () {
			if (this.rigidbody2D.transform.position.x > 88) {
					rigidbody2D.velocity = speed * -1;
					} 
			else if(this.rigidbody2D.transform.position.x < 69.4)
			{
				rigidbody2D.velocity = speed;
			}
		
		}
	}
}
