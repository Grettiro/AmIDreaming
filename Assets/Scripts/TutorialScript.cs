using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {
	public Rect winRect = new Rect(200, 200, 240, 100);
	public string tutText;
	public bool textAllow = false;

	public bool allowGravity;
	public bool allowTeleport;
	public bool allowSlow;

	void Start() {
		Platformer2DUserControl abilities = GameObject.Find ("Player").GetComponent<Platformer2DUserControl> ();
			abilities.Gravity = false;
			abilities.Teleport = false;
			abilities.Slow = false;

	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		Platformer2DUserControl abilities = GameObject.Find ("Player").GetComponent<Platformer2DUserControl> ();
		if (other.tag == "Player") {
			textAllow = true;
			if(allowGravity)
				abilities.Gravity = true;
			if(allowTeleport)
				abilities.Teleport = true;
			if(allowSlow)
				abilities.Slow = true;
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
			textAllow = false;
	}

	private void OnGUI()
	{
		if (textAllow) {
			winRect = GUILayout.Window( 0, winRect, WindowFunction, "Game Paused" );
			winRect.x = (int) ( Screen.width * 0.5f - winRect.width * 0.5f );
			winRect.y = (int) ( Screen.height * 0.5f - winRect.height * 1.5f );

			GUILayout.Window( 0, winRect, WindowFunction, "General Movement" );
		}
	}
	void WindowFunction(int windowID) {
		GUILayout.Label(tutText);
	}
}
