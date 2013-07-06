using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServerCreate : MonoBehaviour {
	
	//This list is the list of online players
	public List<PlayerType> PlayerList;
	[System.Serializable]
	public class PlayerType
	{
		public string Username;
		public NetworkPlayer Player;
	}
	// Use this for initialization
	void Start () {
		Application.runInBackground = true;
		PlayerList = new List<PlayerType>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void StartServer(int _maxConnections, int _port)
	{
		//Create the server
		Network.InitializeServer(_maxConnections, _port);
	}
	
	private void OnPlayerConnected(NetworkPlayer _player)
	{
		Debug.Log("Player Connected");
	}
	
	private void OnPlayerDisconnected(NetworkPlayer _player)
	{
		//Remove player from online list
		PlayerType _playerToRemove = new PlayerType();
		foreach (PlayerType _onlinePlayer in PlayerList)
		{
			if (_onlinePlayer.Player == _player)
			{
				_playerToRemove = _onlinePlayer;	
			}
		}
		Debug.Log("Removing Player "+_playerToRemove.Username+" from the list of online players.");
		PlayerList.Remove(_playerToRemove);
	}
	
	[RPC]
	protected void UpdatePlayerList(string _name, NetworkMessageInfo _info)
	{
		//Add new player to online list
		PlayerType _newEntry = new PlayerType();
		_newEntry.Username = _name;
		_newEntry.Player = _info.sender;
		PlayerList.Add(_newEntry);
		Debug.Log("Player "+_name+" added to the list on online players.");
	}
	
	[RPC]
	protected void SendGameMessage(string _recName, string _message)
	{
		//Relay message to client
		foreach (PlayerType _onlinePlayer in PlayerList)
		{
			if (_onlinePlayer.Username == _recName)
			{
				GetComponent<NetworkView>().RPC("ReceiveGameMessage", _onlinePlayer.Player, _message);
			}
		}
	}


}
