using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	[SerializeField] private Vector3 m_characterPosition;
	public AudioClip m_music;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 CharacterPosition
	{
		get { return m_characterPosition; }
		set { m_characterPosition = value; }
	}
}
