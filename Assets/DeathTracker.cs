using UnityEngine;
using System.Collections;

public class DeathTracker : MonoBehaviour {

	private static DeathTracker instance = null;
	private static DifficultyAdjuster adjust;
	public static DeathTracker Instance 
	{
		get { return instance; }
	}
	private int nDeaths = 0;
	private int dLevel = 5;

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


	void Update () {
		if (nDeaths > 2) 
		{
			nDeaths = 0;
			dLevel -= 1;
		}
	}

	[SerializeField]
	public int Deaths
	{
		get {return nDeaths; }
		set {nDeaths = value; }
	}

	[SerializeField]
	public int Difficulty
	{
		get {return dLevel; }
		set {dLevel = value; }
	}
}