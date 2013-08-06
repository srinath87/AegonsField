using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class GameManager : MonoBehaviour {
	
	private static GameManager instance;
	
	private List<GameObject> matchList = new List<GameObject>();
		
	public GameObject match;
	
	// Network variables
	private string ip;
	private int port;
	private string playerName;
	private string enemyName;
	private bool nameSet;
	
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
		nameSet = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!nameSet)
		{
			if(Application.loadedLevelName.Equals("JasTestScene"))
			{
				SetThePlayerName();
			}
		}
	}
 
	public void OnApplicationQuit ()
	{
		instance = null;
	}
	
	public void StartMatch(int matchId)
	{
		/*
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
		*/
		StartCoroutine("LoadScene", "JasTestScene");
	}
	
	IEnumerator LoadScene(string sceneToLoad)
	{
		if(!sceneToLoad.Equals("None"))
		{
			Application.LoadLevel(sceneToLoad);
		}
		yield return new WaitForSeconds(5);
	}
	
	public void SetPlayerName(string playerName)
	{
		this.playerName = playerName;
	}
	
	public void SetEnemyName(string enemyName)
	{
		this.enemyName = enemyName;
	}
	
	public void SetThePlayerName()
	{
		GameObject matchControllerObj = GameObject.Find("MatchControllerObj");
		if(matchControllerObj != null)
		{
			MatchController mc = matchControllerObj.GetComponent<MatchController>();
			if(mc != null)
			{
				mc.SetPlayerName(playerName);
				if(playerName.Equals("player1"))
				{
					mc.SetOppenentName("player2");
				}
				else
				{
					mc.SetOppenentName("player1");
				}
				mc.Init(1, IsMyTurn());
				nameSet = true;
			}
		}
	}
	
	public bool IsMyTurn()
	{
		if(playerName.Equals("player1"))
		{
			return true;
		}
		return false;
	}
	
	public void SubmitTurn(List<Action> actionsToSubmit)
	{
		SendMessage("SendActions", actionsToSubmit);
	}
	
	public void PerformTurn(List<Action> pendingActions)
	{
		GameObject matchControllerObj = GameObject.Find("MatchControllerObj");
		if(matchControllerObj != null)
		{
			MatchController mc = matchControllerObj.GetComponent<MatchController>();
			if(mc != null)
			{
				Debug.Log(12345);
				mc.SetPendingActions(pendingActions);
			}
		}
	}
	
	protected void CreateMatch(int mId, string playerName, string playerTeam, string opponentName, string opponentTeam, bool facingRight, bool myTurn)
	{
		GameObject newMatch = (GameObject)GameObject.Instantiate((Object)match);
		newMatch.GetComponent<Match>().Init(mId, playerName, playerTeam, opponentName, opponentTeam, facingRight, myTurn);
		matchList.Add(newMatch);
	}
	
}
