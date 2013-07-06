using UnityEngine;
using System.Collections;

public class SendMessage : MonoBehaviour {
	
	private string _receiverUsername = "Receiver";
	private string _message = "Hello there";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		//Display message GUI
		GUI.Label(new Rect(Screen.width/2-100, 70, 200, 30), "Receiver Username");
		_receiverUsername = GUI.TextField(new Rect(Screen.width/2-100, 100, 200, 50), _receiverUsername);
		GUI.Label(new Rect(Screen.width/2-100, 170, 200, 30), "Message");
		_message = GUI.TextField(new Rect(Screen.width/2-100, 200, 200, 100), _message);
		if (GUI.Button(new Rect(Screen.width/2-50, 320, 100, 50), "Send Message"))
		{
			//Send message to the server for further relay
			GetComponent<NetworkView>().RPC("SendGameMessage", RPCMode.Server, _receiverUsername, _message);
		}
	}
}
