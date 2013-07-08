using UnityEngine;
using System.Collections;

public class ServerConnect : MonoBehaviour {
	
	private string _myUsername;
	// Use this for initialization
	void Start () 
	{
		Application.runInBackground = true;
		print("Test");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ConnectToServer(string _ip, int _port, string _username)
	{
		//Connect to server
		_myUsername = _username;
		uLink.Network.Connect(_ip, _port, _myUsername);
	} 
	
	void uLink_OnConnectedToServer()
	{
		//Connected, send details to player
		Debug.Log("Connected to Server");
		GetComponent<uLink.NetworkView>().RPC("UpdatePlayerList", uLink.RPCMode.Server, _myUsername);
		gameObject.AddComponent<SendMessage>();
	}
	
	[RPC]
	protected void ReceiveGameMessage(string _message)
	{
		//We received message, print it
		Debug.Log(_message);	
	}

}