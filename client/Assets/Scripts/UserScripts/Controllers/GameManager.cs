using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	private static GameManager instance;
	
	private List<GameObject> matchList = new List<GameObject>();
		
	public GameObject match;
	
	// Network variables
	private string ip;
	private int port;
	private string playerName;
	
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject ("GameManager").AddComponent<GameManager> ();
			}
 
			return instance;
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(gameObject);
		
		StartCoroutine("LoadScene", "ServerConnect");
		
		ip = "localhost";
		port = 1000;
		playerName = "test";
		
		//GetComponent<ServerConnect>().ConnectToServer(ip, port);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
 
	public void OnApplicationQuit ()
	{
		instance = null;
	}
	
	public void StartMatch(int matchId)
	{
		foreach(GameObject obj in matchList)
		{
			Match match = obj.GetComponent<Match>();
			if(match.GetMatchId() == matchId)
			{
				StartCoroutine("LoadScene", "JasTestScene");
				while(!Application.loadedLevelName.Equals("JasTestScene"))
				{
					StartCoroutine("LoadScene", "None");
				}
				GameObject matchController = GameObject.Find("MatchController");
				matchController.GetComponent<MatchController>().Init(matchId, match.GetPlayerName(), match.GetPlayerTeam(), match.GetOpponentName(), match.GetOpponentTeam(), match.isFacingRight(), match.isMyTurn());
			}
		}
	}
	
	IEnumerator LoadScene(string sceneToLoad)
	{
		if(!sceneToLoad.Equals("None"))
		{
			Application.LoadLevel(sceneToLoad);
		}
		yield return new WaitForSeconds(5);
	}
	
	[RPC]
	protected void CreateMatch(int mId, string playerName, string playerTeam, string opponentName, string opponentTeam, bool facingRight, bool myTurn)
	{
		GameObject newMatch = (GameObject)GameObject.Instantiate((Object)match);
		newMatch.GetComponent<Match>().Init(mId, playerName, playerTeam, opponentName, opponentTeam, facingRight, myTurn);
		matchList.Add(newMatch);
	}
}
