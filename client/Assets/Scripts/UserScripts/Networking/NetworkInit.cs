using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerIOClient;
using AssemblyCSharp;

public class NetworkInit : MonoBehaviour {
	
	private Connection pioconnection;
	private List<PlayerIOClient.Message> msgList = new List<PlayerIOClient.Message>(); //  Messsage queue implementation
	private List<Action> pendingActions = new List<Action>();
	//private bool joinedroom = false;
	
	// Use this for initialization
	void Start() 
	{
		// Required to setup the Player.IO Client in Unity3D
		PlayerIOClient.PlayerIO.UnityInit(this);

		// create a random userid 
		System.Random random = new System.Random();
		string userid = "Guest" + random.Next(0, 10000);

		Debug.Log("Starting");

		PlayerIOClient.PlayerIO.Connect(
			"aegonsfield-eblb0xtckc4iin5myfh1a",	// Game id (Get your own at playerio.com. 1: Create user, 2:Goto admin pannel, 3:Create game, 4: Copy game id inside the "")
			"public",						// The id of the connection, as given in the settings section of the admin panel. By default, a connection with id='public' is created on all games.
			userid,							// The id of the user connecting. This can be any string you like. For instance, it might be "fb10239" if youŽre building a Facebook app and the user connecting has id 10239
			null,							// If the connection identified by the connection id only accepts authenticated requests, the auth value generated based on UserId is added here
			null,
			delegate(Client client) 
			{
				Debug.Log("Successfully connected to Player.IO");
				//infomsg = "Successfully connected to Player.IO";

				//target.transform.Find("NameTag").GetComponent<TextMesh>().text = userid;
				//target.transform.name = userid;

				// Uncoment the line below to use the Development Server
				//client.Multiplayer.DevelopmentServer = new ServerEndpoint("localhost",8184);

				//Create or join the room 
				client.Multiplayer.CreateJoinRoom(
					"TestId",							//Room id. If set to null a random roomid is used
					"AegonsFieldEntry",						//The room type started on the server
					true,								//Should the room be visible in the lobby?
					null,
					null,
					delegate(Connection connection) 
					{
						Debug.Log("Joined Room.");
						//infomsg = "Joined Room.";
						// We successfully joined a room so set up the message handler
						pioconnection = connection;
						pioconnection.OnMessage += handlemessage;
						//joinedroom = true;
					},
					delegate(PlayerIOError error) 
					{
						Debug.Log("Error Joining Room: " + error.ToString());
						//infomsg = error.ToString();
					}
				);
			},
			delegate(PlayerIOError error) 
			{
				Debug.Log("Error connecting: " + error.ToString());
				//infomsg = error.ToString();
			}
		);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void handlemessage(object sender, PlayerIOClient.Message m) 
	{
		msgList.Add(m);
		Debug.Log(m.Type);
		if(m.Type.Equals("TurnEnd") || m.Type.Equals("PlayerName") || m.Type.Equals("EnemyName"))
		{
			Debug.Log("ProcessMessages : "+ m.Type);
			ProcessMessages();
		}
	}
	
	void ProcessMessages()
	{
		foreach(PlayerIOClient.Message m in msgList) 
		{
			switch(m.Type) 
			{
				case "PlayerName": SendMessage("SetPlayerName", m.GetString(0));
					SendMessage("StartMatch", 1);
					pendingActions.Clear();
					break;
				case "EnemyName": SendMessage("SetEnemyName", m.GetString(0));
					pendingActions.Clear();
					break;
				case "TurnSubmit": Action action = new Action();
					action.SetActionType(m.GetInt(0));
					Debug.Log(action.GetActionType().ToString());
					switch(action.GetActionType())
					{
						case Actions.MOVE: 
							//Debug.Log("MOVE...");
							action.SetUnitId(m.GetInt(1));
							action.SetTargetLocation(m.GetFloat(2), m.GetFloat(3), m.GetFloat(4));
							pendingActions.Add(action);
							break;
						case Actions.ATTACK:
							//Debug.Log("ATTACK...");
							action.SetUnitId(m.GetInt(1));
							action.SetTargetId(m.GetInt(2));
							pendingActions.Add(action);
							break;
						case Actions.SPAWN:
							//Debug.Log("SPAWN...");
							action.SetUnitToSpawn(m.GetString(1));
							action.SetUnitId(m.GetInteger(2));
							action.SetSpawnLocation(m.GetFloat(3), m.GetFloat(4), m.GetFloat(5));
							pendingActions.Add(action);
							break;					
					}
					break;
				case "TurnEnd": 
					SendMessage("PerformTurn", pendingActions);
					pendingActions.Clear();
					break;
			}
		}
		// clear message queue after it's been processed
		msgList.Clear();
	}
	
	void SendActions(List<Action> actionsToSubmit)
	{
		//Debug.Log("*");
		foreach(Action action in actionsToSubmit)
		{
			//Debug.Log("**");
			if(action == null)
			{
				continue;
			}
			Message m = Message.Create("TurnSubmit");
			m.Add((int)action.GetActionType());
			Debug.Log(m.GetInt(0).ToString());
			Debug.Log(action.GetActionType().ToString());
			switch(action.GetActionType())
			{
				case Actions.MOVE: 
					m.Add(action.GetUnitID());
					m.Add(action.GetTargetLocation().x);
					m.Add(action.GetTargetLocation().y);
					m.Add(action.GetTargetLocation().z);
					//Debug.Log(action.GetTargetLocation().x);
					//Debug.Log(action.GetTargetLocation().y);
					//Debug.Log(action.GetTargetLocation().z);
					pioconnection.Send(m);
					//Debug.Log(action.GetActionType().ToString());
					break;
				
				case Actions.ATTACK: 
					m.Add(action.GetUnitID());
					m.Add(action.GetTargetId());
					//Debug.Log("****");
					pioconnection.Send(m);
					//Debug.Log(action.GetActionType().ToString());
					break;
					
				case Actions.SPAWN: 
					m.Add(action.GetUnitToSpawn());
					m.Add(action.GetUnitID());
					m.Add(action.GetSpawnLocation().x);
					m.Add(action.GetSpawnLocation().y);
					m.Add(action.GetSpawnLocation().z);
					pioconnection.Send(m);
					break;
			}
			//actionsToSubmit.Remove(action);
		}
		Message m2 = Message.Create("TurnEnd");
		//actionsToSubmit.Clear();
		pioconnection.Send(m2);
		//Debug.Log(m2.Type);
	}
}
