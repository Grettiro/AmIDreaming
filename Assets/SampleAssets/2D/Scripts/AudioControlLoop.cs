using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioControlLoop : MonoBehaviour {

	// Use this for initialization
	public AudioSource[] audioSource;
	AudioSource audioStart;
	AudioSource audioLoop;

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
		audioSource = GetComponents<AudioSource>();
		audioStart = audioSource[0];
		audioStart.Play();
		yield return new WaitForSeconds (audioStart.clip.length);
		audioLoop = audioSource[1];
		audioLoop.loop = true;
		audioLoop.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevel == 0)
		{
			audioLoop.Stop ();
		}
	}
}
