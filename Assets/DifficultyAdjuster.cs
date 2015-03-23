using UnityEngine;
using System.Collections;

public class DifficultyAdjuster : MonoBehaviour {

	private static DifficultyAdjuster instance = null;
	public static DifficultyAdjuster Instance 
	{
		get { return instance; }
	}
	private int dLevel = 1;
	// Use this for initialization
	void Awake () {
		if(instance != null && instance != this)
		{
			Destroy (this.gameObject);
			return;
		}
		else
			instance = this;
		
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[SerializeField]
	public int Difficulty
	{
		get {return dLevel; }
		set {dLevel = value; }
	}
}
