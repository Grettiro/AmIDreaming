using UnityEngine;
using System.Collections;

public class DeathTracker : MonoBehaviour
{
	/*
	 * Not much of a deathtracker, more a difficulty adjuster. Rename and move to Difficulty directory?
	 */
	private static DeathTracker m_instance = null;
	public static DeathTracker Instance 
	{
		get { return m_instance; }
	}

	private int m_numDeaths = 0;
	private int m_deathMarker = 20;
	private int m_diffLevel = 10;

	private void Awake()
	{
		if(m_instance != null && m_instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
			m_instance = this;
	}


	private void Update()
	{
		if(m_numDeaths > m_deathMarker)
		{
			m_numDeaths = 0;
			if(m_diffLevel > 0)
				m_diffLevel -= 1;
		}
	}

	private void LateUpdate()
	{
		m_deathMarker = 20 + ((10 - m_diffLevel) * 5);
	}

	public int Deaths
	{
		get { return m_numDeaths; }
		set { m_numDeaths = value; }
	}

	public int Difficulty
	{
		get { return m_diffLevel; }
		set { m_diffLevel = value; }
	}

	public int DeathMarker
	{
		get { return m_deathMarker; }
	}
}