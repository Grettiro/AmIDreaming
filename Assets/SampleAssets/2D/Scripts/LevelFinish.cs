using UnityEngine;
using System.Collections;

public class LevelFinish : MonoBehaviour
{
	private int nIndex;
	private bool nBool;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (nBool) 
		{
			var findNeurons = GameObject.Find("NeuronTracker");
			var updateNeurons = (NeuronTracker)findNeurons.GetComponent("NeuronTracker");
			updateNeurons.UpdateNeurons(nIndex);
			var findNeurons2 = GameObject.Find("PlayerNeurons");
			var updateNeurons2 = (NeuronCount)findNeurons2.GetComponent("NeuronCount");
			updateNeurons2.Neurons += 1;
		}
		if (other.tag == "Player") 
		{
				Application.LoadLevel(12);
		}
	}
	public void setNeuronStatus(int index)
	{
		nBool = true;
		nIndex = index;
	}
}
