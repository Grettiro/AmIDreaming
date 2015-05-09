using UnityEngine;
using System.Collections;
using UnityEditor;

public class LevelFinish : MonoBehaviour
{
	private PlatformerCharacter2D player;
	private Platformer2DUserControl control;
	private int nIndex;
	private bool nBool;
	private int nProtection = 0;
	private Animator anim;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
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
			control = GameObject.Find("Player").GetComponent<Platformer2DUserControl>();
			control.Log(Application.loadedLevelName, nBool);
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
		if(Application.loadedLevelName.Contains ("Medium") || Application.loadedLevel == (9))
		{
			Application.LoadLevel(2);
		}
		else
			Application.LoadLevel (1);
	}
}
