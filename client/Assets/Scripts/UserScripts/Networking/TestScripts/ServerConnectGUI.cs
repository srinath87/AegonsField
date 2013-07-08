using UnityEngine;
using System.Collections;

public class ServerConnectGUI : MonoBehaviour {
	
	private string _username = "Username";
	//private string _ip = "127.0.0.1";
	//private string _ip = "86.19.209.49";
	private string _ip = "192.168.0.10";
	private int _port = 1000;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		//Display connection info
		GUI.Label(new Rect(Screen.width/2-100, 70, 200, 30), "Username");
		_username = GUI.TextField(new Rect(Screen.width/2-100, 100, 200, 50), _username);
		GUI.Label(new Rect(Screen.width/2-100, 170, 200, 30), "Server IP");
		_ip = GUI.TextField(new Rect(Screen.width/2-100, 200, 200, 50), _ip);
		GUI.Label(new Rect(Screen.width/2-100, 270, 200, 30), "Server Port");
		int.TryParse(GUI.TextField(new Rect(Screen.width/2-100, 300, 200, 50), _port.ToString()), out _port);
		if (GUI.Button(new Rect(Screen.width/2-50, 360, 100, 50), "Connect to Server"))
		{
			//Access main_ServerConnect to create server
			GetComponent<ServerConnect>().ConnectToServer(_ip, _port, _username);
		}
	}
	
	void uLink_OnConnectedToServer()
	{
		//Connected, destroy the GUI
		Destroy(this);
		print("Tests");
	}
}
