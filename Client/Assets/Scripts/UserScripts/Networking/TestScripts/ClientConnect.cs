using UnityEngine;
using System.Collections;

public class ClientConnect : MonoBehaviour {
	
	public string connectToIP = "127.0.0.1";
	public int connectPort = 25001;
	public int numberOfConnections = 32;
	public bool serverConnected = false;
	private int test = 0;
	
	// Use this for initialization
	void Start () 
	{
		Network.Connect(connectToIP, connectPort);
		serverConnected = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!serverConnected)
		{
			Network.Connect(connectToIP, connectPort);
			serverConnected = true;
		}
	}
	
	void OnConnectedToServer() 
	{
		Debug.Log("This CLIENT has connected to a server");
		//networkView.RPC ("SubmitTurn", RPCMode.Server);
	}
	
	void OnFailedToConnect(NetworkConnectionError error)
	{
		serverConnected = false;
	}
	
	[RPC]
	void SubmitTurn()
	{
		test = 1;
		Debug.Log("Test = " + test);
	}
	
	/*
	[RPC]
	void ReceivePendingMessages(string playerName, PendingMessages* msgs)
	{
	}
	*/
}
