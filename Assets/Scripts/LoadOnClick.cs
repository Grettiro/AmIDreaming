using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{
	void Start()
	{
	}
	
	public void LoadScene(int level)
	{
		Application.LoadLevel(level);
	}

	void Update()
	{
		if (Input.GetKey("escape"))
			Application.Quit();
	}
}
