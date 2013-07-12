using UnityEngine;
using System.Collections;

public class ServerConnect : MonoBehaviour {
	
	// Use this for initialization
	void Start () 
	{
		Application.runInBackground = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ConnectToServer(string _ip, int _port)
	{
		uLink.Network.Connect(_ip, _port);
	} 
	
	void uLink_OnConnectedToServer()
	{
		//Connected, send details to player
		Debug.Log("Connected to Server");
		//GetComponent<uLink.NetworkView>().RPC("UpdatePlayerList", uLink.RPCMode.Server, _myUsername);
		//gameObject.AddComponent<SendMessage>();
		
		GameObject controller = GameObject.Find("GameController");
		GameManager manager = controller.GetComponent<GameManager>();
		manager.StartCoroutine("LoadScene", "LoginScene");
	}
	
	[RPC]
	protected void ReceiveGameMessage(string _message)
	{
		//We received message, print it
		Debug.Log(_message);	
	}

}