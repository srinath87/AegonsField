using UnityEngine;
using System.Collections;


public class Match : MonoBehaviour
{
	private int matchId;
	
	private string playerName;
	private string playerTeam;
	private bool facingRight;
	
	private string opponentName;
	
	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Init(int mId, string playerName, string opponentName, bool facingRight)
	{
		matchId = mId;
		this.playerName = playerName;
		this.opponentName = opponentName;
		this.facingRight = facingRight;
	}
}