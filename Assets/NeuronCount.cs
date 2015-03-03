using UnityEngine;
using System.Collections;

public class NeuronCount : MonoBehaviour
{
	private int nNeurons = 6;
	private int prevLevel;
	private static NeuronCount instance = null;
	public static NeuronCount Instance 
	{
		get { return instance; }
	}

	void Awake()
	{
		if(instance != null && instance != this)
		{
			Destroy (this.gameObject);
			return;
		}
		else
			instance = this;

		DontDestroyOnLoad (this.gameObject);	
	}
	
	[SerializeField]
	public int Neurons
	{
		get {return nNeurons; }
		set {nNeurons = value; }
	}

	public int GetPrevLevel
	{
		get {return prevLevel;}
		set {prevLevel = value;}
	}
}
