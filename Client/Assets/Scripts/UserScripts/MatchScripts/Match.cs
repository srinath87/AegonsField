using UnityEngine;
using System.Collections;


public class Match : MonoBehaviour
{
	private int matchId;
	
	private string playerName;
	private string playerTeam;
	private bool facingRight;
	private bool myTurn;
	
	private string opponentName;
	private string opponentTeam;
	
	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Init(int mId, string playerName, string playerTeam, string opponentName, string opponentTeam, bool facingRight, bool isMyTurn)
	{
		matchId = mId;
		this.playerName = playerName;
		this.playerTeam = playerTeam;
		this.opponentName = opponentName;
		this.opponentTeam = opponentTeam;
		this.facingRight = facingRight;
		this.myTurn = myTurn;
	}
	
	public int GetMatchId()
	{
		return matchId;
	}
	
	public string GetPlayerName()
	{
		return playerName;
	}
	
	public string GetPlayerTeam()
	{
		return playerTeam;
	}
	
	public string GetOpponentName()
	{
		return opponentName;
	}
	
	public string GetOpponentTeam()
	{
		return opponentTeam;
	}
	
	public bool isFacingRight()
	{
		return facingRight;
	}
	
	public bool isMyTurn()
	{
		return myTurn;
	}
}