using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioControlLoop : MonoBehaviour {

	// Use this for initialization
	public AudioSource[] audioSource;
	public AudioSource audioStart;
	public AudioSource audioLoop;

	private static AudioControlLoop instance = null;
	public static AudioControlLoop Instance {
		get { return instance; }
	}
	void Awake()
	{
		if (instance != null && instance != this) {
				Destroy (this.gameObject);
				return;
		} else {
				instance = this;
		}
		DontDestroyOnLoad (this.gameObject);	
	}
	IEnumerator Start()
	{
		audioStart.Play();
		yield return new WaitForSeconds (audioStart.clip.length);
		audioLoop.loop = true;
		audioLoop.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevel == 0)
		{
			Destroy (this.gameObject);
		}
	}

	public void pitchChangeDown()
	{
		audioStart.pitch = 0.6f;
		audioLoop.pitch = 0.6f;
		}
	public void pitchChangeUp()
	{
		audioStart.pitch = 1;
		audioLoop.pitch = 1;
	}
}
