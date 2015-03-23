using UnityEngine;
using System.Collections;

public class CheckpointObject : MonoBehaviour {
	private Vector3 checkpointPos;
	private bool checkpoint = false;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			checkpoint = true;
			checkpointPos = this.transform.position;
		}
	}

	[SerializeField]
	public Vector3 Checkpoint
	{
		get {return new Vector3(checkpointPos.x + 5,checkpointPos.y + 5, 0);}
		set {checkpointPos = value; }
	}
	
	[SerializeField]
	public bool IsCheckpoint
	{
		get {return checkpoint; }
		set {checkpoint = value; }
	}
}
