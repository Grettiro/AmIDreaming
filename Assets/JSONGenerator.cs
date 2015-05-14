using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class JSONGenerator {

	gmailSender sender = new gmailSender();

	private static string name = "name";
	private string m_InGameLog = "";
	private static JSONClass I = new JSONClass();
	void P(string aText)
	{
		m_InGameLog += aText + "\n";
	}

	// Use this for initialization
	void Start () {
	}

	public void setName(string playerName)
	{
		name = playerName;
		I["player"] = name; // need to fix
	}

	public void LevelExit(bool neuronCollected, bool levelFinished, string levelBegin, string levelEnd, float pauseTime, float[] path)
	{
		I[Application.loadedLevelName]["neuronPickup"].AsBool = neuronCollected;
		I[Application.loadedLevelName]["levelStart"] = levelBegin;
		I[Application.loadedLevelName]["levelEnd"] = levelEnd;
		I[Application.loadedLevelName]["pauseTime"].AsFloat = pauseTime;
		I[Application.loadedLevelName]["levelCompleted"].AsBool = levelFinished;
		I[Application.loadedLevelName]["averageSpeed"].AsFloat = 3.151f;
		for(int i = 0; i < path.Length; i += 2)
			I[Application.loadedLevelName]["pathOfPlay"][-1] = "(" + path[i] + ", " + path[i + 1] + ")";
		P(I.ToString(""));

		sendMail();

		Debug.Log(m_InGameLog);
		m_InGameLog = "";
	}

	private void sendMail()
	{
		sender.sendMail(m_InGameLog);
	}

	public void logDeath(int count, float xCoord, float yCoord, string timeOfDeath, int teleportCount, int gravityCount,
	                     int slowtimeCount, int doubleJumps, string latestAbility, string timeOfLatestAbility, float[] path)
	{
		I[Application.loadedLevelName]["death" + count]["xCoord"].AsFloat = xCoord;
		I[Application.loadedLevelName]["death" + count]["yCoord"].AsFloat = yCoord;
		I[Application.loadedLevelName]["death" + count]["time"] = timeOfDeath;
		I[Application.loadedLevelName]["death" + count]["teleportCounts"].AsInt = teleportCount;
		I[Application.loadedLevelName]["death" + count]["gravityCounts"].AsInt = gravityCount;
		I[Application.loadedLevelName]["death" + count]["slowtimeCounts"].AsInt = slowtimeCount;
		I[Application.loadedLevelName]["death" + count]["doubleJumps"].AsInt = doubleJumps;
		I[Application.loadedLevelName]["death" + count]["latestAbility"] = latestAbility;
		I[Application.loadedLevelName]["death" + count]["latestAbilityTime"] = timeOfLatestAbility;
		for(int i = 0; i < path.Length; i += 2)
			I[Application.loadedLevelName]["death" + count]["pathOfPlay"][-1] = "(" + path[i] + ", " + path[i + 1] + ")";
		P(I.ToString(""));
	}

	// Update is called once per frame
	void Update () {
	
	}
}
