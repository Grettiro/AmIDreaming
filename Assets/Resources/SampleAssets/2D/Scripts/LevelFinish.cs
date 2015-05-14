﻿using UnityEngine;
using System.Collections;

public class LevelFinish : MonoBehaviour
{
	private PlatformerCharacter2D player;
	private Platformer2DUserControl control;
	private int nIndex;
	private bool nBool;
	private int nProtection = 0;
	private Animator anim;
<<<<<<< HEAD
	private bool finished = false;
	private GameObject checkpoint;
	private CheckpointObject setPos;
=======
>>>>>>> parent of 38a5ae4... The great commit of commits

	void Awake()
	{
		control = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && !finished) 
		{
			finished = true;
			if((player = GameObject.Find("Player").GetComponent<PlatformerCharacter2D>()) != null)
			{
				anim = player.getAnimator();
				player.setDead (true);
			}
			if (nBool) 
			{
				NeuronTracker updateNeurons = GameObject.Find("NeuronTracker").GetComponent<NeuronTracker>();
				updateNeurons.UpdateNeurons(nIndex);
				NeuronCount updateNeurons2 = GameObject.Find("PlayerNeurons").GetComponent<NeuronCount>();
				if(nProtection == 0)
				{
					updateNeurons2.Neurons += 1;
					nProtection = 1;
				}
			}

			control.LogExit(true);

			other.GetComponent<Rigidbody2D>().isKinematic = true;
			StartCoroutine(DoAnimation());
		}
	}
	public void setNeuronStatus(int index)
	{
		nBool = true;
		nIndex = index;
	}

	private IEnumerator DoAnimation()
	{
		yield return new WaitForSeconds(0.3f); // wait for two seconds.
<<<<<<< HEAD
		checkpoint = GameObject.FindGameObjectWithTag ("Checkpoint");
		if (checkpoint != null) {
			setPos = checkpoint.GetComponent<CheckpointObject> ();
			setPos.IsCheckpoint = false;
		}
		if (Application.loadedLevelName.Contains ("Medium") || Application.loadedLevel == 14) {
			Application.LoadLevel (2);
		} 
		else if (Application.loadedLevel == 3 || Application.loadedLevel == 4) {
			Application.LoadLevel (Application.loadedLevel + 1);
		}
		else if (Application.loadedLevel == 13) {
			Application.LoadLevel(14);
=======
		if(Application.loadedLevelName.Contains ("Medium") || Application.loadedLevel == (9))
		{
			Application.LoadLevel(2);
>>>>>>> parent of 38a5ae4... The great commit of commits
		}
		else
			Application.LoadLevel (1);
	}
}