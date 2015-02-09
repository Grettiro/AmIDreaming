using UnityEngine;
using System.Collections;

public class PlayerFollowPlatform : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			//Not Working atm
		}
	}

}
