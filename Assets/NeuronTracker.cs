﻿using UnityEngine;
using System.Collections;

public class NeuronTracker : MonoBehaviour {

	// Use this for initialization
	private bool[] neurons = new bool[6];

	private static NeuronTracker instance = null;
	public static NeuronTracker Instance 
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
	
	// Update is called once per frame
	public void UpdateNeurons(int index)
	{
		for(int i = 0; i < 6; i++)
		{
			if(i == index)
			{
				neurons[i] = true;
			}
		}
	}
	public bool returnNeurons(int index)
	{
		for (int i = 0; i < 6; i++) 
		{
			if(i == index)
			{
				return neurons[i];
			}
		}
		return false;
	}
}
