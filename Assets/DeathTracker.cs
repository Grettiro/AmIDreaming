using UnityEngine;
using System.Collections;

public class DeathTracker : MonoBehaviour {

	private static DeathTracker instance = null;
	public static DeathTracker Instance 
	{
		get { return instance; }
	}
	private int nDeaths = 0;

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
		
	}

	[SerializeField]
	public int Deaths
	{
		get {return nDeaths; }
		set {nDeaths = value; }
	}
}