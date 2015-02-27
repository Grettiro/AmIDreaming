using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{
	public AudioSource audioStart;
	public AudioSource audioLoop;
		
	void Start()
	{
		audioStart.Play();
	}
	
	public void LoadScene(int level)
	{
		Application.LoadLevel(level);
	}

	void Update()
	{
		if (Input.GetKey("escape"))
			Application.Quit();

		if(!audioStart.isPlaying) 
			if(!audioLoop.isPlaying)
			{
				audioLoop.loop = true;
				audioLoop.Play();
			}
	}
}
