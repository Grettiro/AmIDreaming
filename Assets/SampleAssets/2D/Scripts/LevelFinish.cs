using UnityEngine;
using System.Collections;

public class LevelFinish : MonoBehaviour
{
	private int nIndex;
	private bool nBool;
	private int nProtection = 0;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player") 
		{
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
			if(Application.loadedLevelName.Contains ("Medium") || Application.loadedLevel == (9))
			{
				Application.LoadLevel(2);
			}
			else
				Application.LoadLevel (1);
		}
	}
	public void setNeuronStatus(int index)
	{
		nBool = true;
		nIndex = index;
	}
}
