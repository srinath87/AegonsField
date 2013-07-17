using UnityEngine;
using System.Collections;

public class ServerCreateGUI : MonoBehaviour {
	
	private int _serverPort = 1000;
	private int _maximumConnections = 10;
	// Use this for initialization
	void Start () 
	{
		//Debug.Log("Test");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI()
	{
		//Display server creation details
		GUI.Label(new Rect(Screen.width/2-100, 70, 200, 30), "Server Port");
		int.TryParse(GUI.TextField(new Rect(Screen.width/2-100, 100, 200, 50), _serverPort.ToString()), out _serverPort);
		GUI.Label(new Rect(Screen.width/2-100, 170, 200, 30), "Maximum Connections");
		int.TryParse(GUI.TextField(new Rect(Screen.width/2-100, 200, 200, 50), _maximumConnections.ToString()), out _maximumConnections);
		if (GUI.Button(new Rect(Screen.width/2-50, 260, 100, 50), "Start Server"))
		{
			//Access main_ServerCreate to create server
			GetComponent<ServerCreate>().StartServer(_maximumConnections, _serverPort);
		}
	}
	
	void uLink_OnServerInitialized()
	{
		//Connected, destroy GUI
		Destroy(this);	
	}
}
