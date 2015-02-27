using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	private Switches switches;
	private Color originalColor;

	public int buttonNumber;

	private void Awake()
	{
		switches = GetComponentInParent<Switches>();
	}

	public Color getOriginalColor()
	{
		return originalColor;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" && this.enabled)
		{
			// Disable and hide the button.
			this.enabled = false;
			originalColor = renderer.material.color;
			renderer.material.color = Color.clear;
			// Send value of buttonNumber to the array in switches.
			switches.UpdateArray(buttonNumber);
		}
	}
}
