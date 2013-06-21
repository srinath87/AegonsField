using UnityEngine;
using System.Collections;

public class ClientConnect : MonoBehaviour {
	
	public string connectToIP = "127.0.0.1";
	public int connectPort = 25001;
	public int numberOfConnections = 32;
	// Use this for initialization
	void Start () {
		Debug.Log("1");
		Network.Connect(connectToIP, connectPort);
		Debug.Log("2");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnConnectedToServer() {
		Debug.Log("This CLIENT has connected to a server");	
	}
}
