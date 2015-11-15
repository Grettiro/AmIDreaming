using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	private Vector3 m_startingPosition = new Vector3(-30.0f, -4.0f);
	[SerializeField] private Vector3 m_characterPosition;
	private int m_totalDeaths = 0;
	private bool m_checkpointReached = false;
	private bool m_checkpointGravity;

	private static LevelManager m_instance = null;
	public static LevelManager LevelManagerInstance
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
	}

	// Use this for initialization
	private void Start()
	{
	}
	
	// Update is called once per frame
	private void Update()
	{
	}

	public void ResetPosition()
	{
		m_characterPosition = m_startingPosition;
	}

	public Vector3 CharacterPosition
	{
		get { return m_characterPosition; }
		set { m_characterPosition = value; }
	}

	public int TotalDeaths
	{
		get { return m_totalDeaths; }
		set { m_totalDeaths = value; }
	}

	public bool CheckpointReached
	{
		get { return m_checkpointReached; }
		set { m_checkpointReached = value; }
	}

	public bool CheckpointGravity
	{
		get { return m_checkpointGravity; }
		set { m_checkpointGravity = value; }
	}
}
