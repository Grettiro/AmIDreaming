using UnityEngine;
using System.Collections;

//TODO: Still needs to be made more modular.
public class Switches : MonoBehaviour 
{
	private int count = 0;
	private int[] buttonArray = new int[4];

	private Doors doors;

	private void Awake()
	{
		doors = GameObject.Find("Doors").GetComponent<Doors>();
	}

	private void checkDoors()
	{
		int prev = 0;

		for(int i = 0; i < 4; i++)
			if((buttonArray[i] - 1) == prev)
				prev = buttonArray[i];
		
		if (prev == 4)
			doors.OpenDoors (false);

		else 
		{
			var buttons = this.GetComponentsInChildren<Button>();
			for(int i = 0; i < buttons.Length; i++)
			{
				buttons[i].renderer.material.color = buttons[i].getOriginalColor();
				buttons[i].enabled = true;
			}
		}
	}

	public void UpdateArray(int buttonNumber)
	{
		buttonArray[count] = buttonNumber;
		count++;
		if(count == 4)
		{
			count = 0;
			checkDoors();
		}
	}
}
