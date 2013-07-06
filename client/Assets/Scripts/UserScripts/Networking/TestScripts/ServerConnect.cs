using UnityEngine;
using System.Collections;

public class ServerConnect : MonoBehaviour {
	
	private string _myUsername;
	// Use this for initialization
	void Start () {
		Application.runInBackground = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ConnectToServer(string _ip, int _port, string _username)
	{
		//Connect to server
		_myUsername = _username;
		Network.Connect(_ip, _port);
	}
	
	private void OnConnectedToServer()
	{
		//Connected, send details to player
		GetComponent<NetworkView>().RPC("UpdatePlayerList", RPCMode.Server, _myUsername);
		gameObject.AddComponent<SendMessage>();
	}
	
	[RPC]
	protected void ReceiveGameMessage(string _message)
	{
		//We received message, print it
		Debug.Log(_message);	
	}
	
	[RPC]
	protected void UpdatePlayerList(string _name, NetworkMessageInfo _info)
	{
		
	}
	
	[RPC]
	protected void SendGameMessage(string _recName, string _message)
	{
		
	}

}