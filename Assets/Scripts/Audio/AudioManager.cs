using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	public AudioSource m_backgroundMusic;
	public AudioClip[] m_musicTrack;

	public AudioSource m_SFX;
	public AudioClip[] m_SFXClips;

	private static AudioManager m_instance = null;
	public static AudioManager AudioManagerInstance
	{
		get { return m_instance; }
	}
	
	private void Awake()
	{
		if(m_instance != null && m_instance != this)
		{
			Destroy(this.gameObject);
			return;
		}

		m_backgroundMusic.clip = m_musicTrack[0];
		m_backgroundMusic.Play();

		m_instance = this;

		DontDestroyOnLoad(this.gameObject);
	}

	private void Update()
	{
		if(Application.loadedLevelName.Contains("Tutorial") || Application.loadedLevelName.Equals("TitleMenu"))
		{
			if(m_backgroundMusic.clip != m_musicTrack[0])
				ChangeTrack(0);
		}

		if(Application.loadedLevelName.Contains("Overworld"))
		{
			if(Application.loadedLevelName.Equals("Overworld1"))
			{
				if(m_backgroundMusic.clip != m_musicTrack[1])
					ChangeTrack(1);
			}
			else if(Application.loadedLevelName.Equals("Overworld2"))
			{
				if(m_backgroundMusic.clip != m_musicTrack[2])
					ChangeTrack(2);
			}
			else if(Application.loadedLevelName.Equals("Overworld3"))
			{
			}
			else if(Application.loadedLevelName.Equals("Overworld4"))
			{
			}
		}

		// If the level is within world 1, world 2, etc. Then load the desired music track.
		if(Application.loadedLevelName.Contains("World1"))
		{
			// Music for the levels in World 1 - Doubt
			if(m_backgroundMusic.clip != m_musicTrack[1])
				ChangeTrack(1);
		}
		else if(Application.loadedLevelName.Contains("World2"))
		{
			if(m_backgroundMusic.clip != m_musicTrack[2])
				ChangeTrack(2);
		}
		else if(Application.loadedLevelName.Contains("World3"))
		{
		}
		else if(Application.loadedLevelName.Contains("World4"))
		{
		}
	}

	private void ChangeTrack(int index)
	{
		m_backgroundMusic.clip = m_musicTrack[index];
		m_backgroundMusic.Play();
	}

	public void PlayNeuron()
	{
		m_SFX.clip = m_SFXClips[0];
		m_SFX.Play();
	}
}
