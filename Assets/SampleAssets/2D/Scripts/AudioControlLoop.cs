using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioControlLoop : MonoBehaviour {

	// Use this for initialization
	public AudioSource audioStart;
	public AudioSource audioLoop;
	public AudioSource audioStart2;
	public AudioSource audioLoop2;
	private bool changeSongs;
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
			audioStart.Play();
			instance = this;
		}

		DontDestroyOnLoad(this.gameObject);	
	}

	
	// Update is called once per frame
	void Update()
	{
		var findNeurons = GameObject.Find("PlayerNeurons");
		var setLevel = (NeuronCount)findNeurons.GetComponent("NeuronCount");
		if (Application.loadedLevel == 2 && setLevel.GetPrevLevel == 1) 
		{
			if (audioStart.isPlaying) 
			{
				audioStart.Stop ();
			} 
			else if (audioLoop.isPlaying) 
			{
				audioLoop.Stop ();
			}
			changeSongs = true;
			audioStart2.Play();
		} 
		else
		{
			if(!changeSongs)
			{
				if(!audioStart.isPlaying)
					if(!audioLoop.isPlaying)
				{
					audioStart.Stop();
					audioLoop.loop = true;
					audioLoop.Play();
				}
			}
			else
			{
				if(!audioStart2.isPlaying)
					if(!audioLoop2.isPlaying)
				{
					audioStart2.Stop();
					audioLoop2.loop = true;
					audioLoop2.Play();
				}
			}
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

	public void playNeuron()
	{

		audio.PlayOneShot (neuronPickup);
	}
}
