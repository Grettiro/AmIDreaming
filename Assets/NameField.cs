using UnityEngine;
using System.Collections;

public class NameField : MonoBehaviour {

	private JSONGenerator json = new JSONGenerator();

	public Rect winRect = new Rect(200, 200, 240, 100);
	public string nameOfPlayer = "";

	void OnGUI() {
		winRect = GUILayout.Window( 0, winRect, WindowFunction, "");
		winRect.x = (int) ( Screen.width * 0.65f - winRect.width * 0.5f );
		winRect.y = (int) ( Screen.height * 0.48f - winRect.height * 0.5f );
		
		// Added
		GUILayout.Window( 0, winRect, WindowFunction, "" );
	}

	void WindowFunction(int windowID) {
		GUILayout.TextField("Need to put in your name!");
		nameOfPlayer = GUILayout.TextField(nameOfPlayer);
		if (GUILayout.Button("New Game") && nameOfPlayer != "")
		{
			json.setName(nameOfPlayer);
			Application.LoadLevel(1);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
