using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public void LoadScene(int level)
	{
		Application.LoadLevel(level);
	}

	private void Update()
	{
		if (Input.GetKey("escape"))
			Application.Quit();
	}
}
