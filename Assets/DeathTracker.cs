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
	private int deathMarker;
	private int dLevel = 10;

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
		if (nDeaths > deathMarker) 
		{
			nDeaths = 0;
			if(dLevel > 0)
				dLevel -= 1;
		}
	}
	void LateUpdate() {
		deathMarker = 20 + (10 - dLevel * 5);
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

	public int DeathMarker
	{
		get {return deathMarker; }
		set {deathMarker = value; }
	}
}