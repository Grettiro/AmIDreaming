using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioControlLoop : MonoBehaviour {

	// Use this for initialization
	public AudioSource audioStart;
	public AudioSource audioLoop;
	public AudioSource audioStart2;
	public AudioSource audioLoop2;
	public AudioSource audioStart3;
	public AudioSource audioLoop3;
	public AudioSource audioStart4;
	public AudioSource audioLoop4;

	private bool changeSongs = false;
	public AudioClip neuronPickup;

	private static AudioControlLoop instance = null;
	public static AudioControlLoop Instance
	{
		get { return instance; }
	}

	void Awake()
	{
		if(instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			if(Application.loadedLevel == 1 || (Application.loadedLevel >= 6 && Application.loadedLevel <= 9))
				audioStart.Play();
			if(Application.loadedLevel == 2 || (Application.loadedLevel >= 6 && Application.loadedLevel <= 13))
				audioStart2.Play();
			if(Application.loadedLevel >= 3 && Application.loadedLevel <= 5 || Application.loadedLevel == 14)
				audioStart3.Play();
			if(Application.loadedLevel >= 20)
				audioStart4.Play();

			instance = this;
		}

		DontDestroyOnLoad(this.gameObject);	
	}

	
	// Update is called once per frame
	void Update()
	{

		//NeuronCount setLevel = GameObject.Find("PlayerNeurons").GetComponent<NeuronCount>();
		if (Application.loadedLevel == 0 || Application.loadedLevel == 24) 
		{
			Destroy (this.gameObject);
		}
		if (Application.loadedLevel == 1 || Application.loadedLevel >= 15 && Application.loadedLevel <= 23) {
			if (audioStart3.isPlaying) {
				audioStart3.Stop ();
				changeSongs = true;
				audioStart.Play ();
			} else if (audioLoop3.isPlaying) {
				audioLoop3.Stop ();
				changeSongs = true;
				audioStart.Play ();
			} else if (!audioStart.isPlaying) {
				if (!audioLoop.isPlaying) {
					audioStart.Stop ();
					audioLoop.loop = true;
					audioLoop.Play ();
				}
			}

		} else if (Application.loadedLevel == 14) {
			if (audioStart2.isPlaying) {
				audioStart2.Stop ();
				changeSongs = true;
				audioStart3.Play ();
			} else if (audioLoop3.isPlaying) {
				audioLoop2.Stop ();
				changeSongs = true;
				audioStart3.Play ();
			} else if (!audioStart3.isPlaying) {
				if (!audioLoop3.isPlaying) {
					audioStart3.Stop ();
					audioLoop3.loop = true;
					audioLoop3.Play ();
				}
			}
		}
		else if (Application.loadedLevel >= 15 && Application.loadedLevel <= 23) {
			if (audioStart3.isPlaying) {
				audioStart3.Stop ();
				changeSongs = true;
				audioStart2.Play ();
			} else if (audioLoop3.isPlaying) {
				audioLoop3.Stop ();
				changeSongs = true;
				audioStart2.Play ();
			} else if (!audioStart2.isPlaying) {
				if (!audioLoop2.isPlaying) {
					audioStart2.Stop ();
					audioLoop2.loop = true;
					audioLoop2.Play ();
				}
			}

		}
			
			
	}

	public void pitchChangeDown()
	{
		if(Application.loadedLevel >= 15 && Application.loadedLevel <= 23)
		{
			audioStart2.pitch = 0.6f;
			audioLoop2.pitch = 0.6f;
		}
	}

	public void pitchChangeUp()
	{
		if(Application.loadedLevel >= 15 && Application.loadedLevel <= 23)
		{
			audioStart2.pitch = 0.6f;
			audioLoop2.pitch = 0.6f;
		}
	}

	public void playNeuron()
	{
		GetComponent<AudioSource>().PlayOneShot(neuronPickup);
	}
}
