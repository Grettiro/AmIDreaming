using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;
using System;

public class JSONGenerator : MonoBehaviour {

	private string m_InGameLog = "";
	private JSONClass I = new JSONClass();
	void P(string aText)
	{
		m_InGameLog += aText + "\n";
	}

	// Use this for initialization
	void Start () {
		I["player"] = "test";
		//FileStream fStream = new FileStream(@"", FileMode.Append);

		float[] testPath = {1.54f, 152.3f};
		logDeath(1, "level1", testPath);
		logDeath(2, "level1", testPath);
		//LevelExit("level1", true);
		//P(I.ToString(""));

		// Create the file, write the first parts including identification for the player.
	}

	public void LevelExit(string levelName, bool neuronCollected)
	{
		I[levelName]["neuronPickup"].AsBool = neuronCollected;
		I[levelName]["levelStart"].AsFloat = 2.151f;
		I[levelName]["levelEnd"].AsFloat = 15.21f;
		I[levelName]["pauseTime"].AsFloat = 25.151f;
		I[levelName]["levelCompleted"].AsBool = false;
		I[levelName]["averageSpeed"].AsFloat = 3.151f;
		P(I.ToString(""));
		Debug.Log(m_InGameLog);
		// do stuff
	}

	public void logDeath(int count, string levelName, float[] path)
	{
		I[levelName]["death" + count]["xCoord"].AsFloat = 4.625f;
		I[levelName]["death" + count]["yCoord"].AsFloat = -52.15612f;
		I[levelName]["death" + count]["time"].AsFloat = 10.512f;
		I[levelName]["death" + count]["teleportCounts"].AsInt = 4;
		I[levelName]["death" + count]["gravityCounts"].AsInt = 5;
		I[levelName]["death" + count]["slowtimeCounts"].AsInt = 2;
		I[levelName]["death" + count]["doubleJumps"].AsInt = 4;
		I[levelName]["death" + count]["latestAbility"] = "name of ability";
		I[levelName]["death" + count]["latestAbilityTime"].AsFloat = 12.5231f;
		for(int i = 0; i < path.Length; i += 2)
			I[levelName]["death" + count]["pathOfPlay"][-1] = "" + path[i] + ", " + path[i + 1];
		// do stuff
	}
	// On each level entry start level timer, when paused stop level timer and start pause timer.
	// Record timestamp of level entry. Record when neuron is picked up. Record average speed through
	// the level. If level end reached and exited through the final sign, level is completed, otherwise
	// if exited through menu, level is not finished. On level exit/finish, stop timers and append data
	// to file.
	//
	// For each death record position, timestamp, how many times each ability was used, name and time of
	// latest ability used as well as the rough path taken until death.

	// Update is called once per frame
	void Update () {
	
	}
}
