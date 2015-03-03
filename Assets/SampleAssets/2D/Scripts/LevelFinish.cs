using UnityEngine;
using System.Collections;

public class LevelFinish : MonoBehaviour
{
	private int nIndex;
	private bool nBool;
	private int nProtection = 0;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player 1") 
		{
			if (nBool) 
			{
				var findNeurons = GameObject.Find ("NeuronTracker");
				var updateNeurons = (NeuronTracker)findNeurons.GetComponent ("NeuronTracker");
				updateNeurons.UpdateNeurons (nIndex);
				var findNeurons2 = GameObject.Find ("PlayerNeurons");
				var updateNeurons2 = (NeuronCount)findNeurons2.GetComponent ("NeuronCount");
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
