using UnityEngine;
using System.Collections;

public class CheckpointObject : MonoBehaviour {
	private Vector3 checkpointPos = new Vector3 (0, 0, 0);
	public float offsetY = 1;
	public float offsetX = 0;
	private bool checkpoint = false;
	// Use this for initialization
	private static CheckpointObject instance = null;
	public static CheckpointObject Instance 
	{
		get { return instance; }
	}

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
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			checkpoint = true;
			checkpointPos = this.transform.position;
			DeathTracker difficulty = GameObject.Find ("DeathTracker").GetComponent<DeathTracker> ();
			difficulty.Deaths /= 2;
			Destroy(this.gameObject);
		}
	}

	[SerializeField]
	public Vector3 Checkpoint
	{
		get {return new Vector3(checkpointPos.x + offsetX,checkpointPos.y + offsetY, 0);}
		set {checkpointPos = value; }
	}
	
	[SerializeField]
	public bool IsCheckpoint
	{
		get {return checkpoint; }
		set {checkpoint = value; }
	}
}
