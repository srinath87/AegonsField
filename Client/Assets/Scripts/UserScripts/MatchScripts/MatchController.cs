using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class MatchController : MonoBehaviour {
	
	private int matchID;
	private string playerName;
	private string playerTeam;
	private string opponentName;
	private string opponentTeam;
	private GameObject selectedUnit;
	private int actionsLeft;
	private bool myTurn;
	private bool facingRight;
	private List<Action> actionsInTurn;
	
	
	// Use this for initialization
	void Start () 
	{
		matchID = -1;
		opponentName = "";
		playerName = "";
		selectedUnit = null;
		actionsLeft = 0;
		myTurn = false;
		actionsInTurn = new List<Action>();
	}
	
	public void Init(int matchID, string playerName, string playerTeam, string opponentName, string opponentTeam, bool facingRight, bool myTurn)
	{
		this.matchID = matchID;
		this.opponentName = opponentName;
		this.opponentTeam = opponentTeam;
		this.playerName = playerName;
		this.playerTeam = playerTeam;
		this.facingRight = facingRight;
		
		this.myTurn = myTurn;
		if(this.myTurn)
		{
			actionsLeft = 5;
			actionsInTurn.Clear();
		}
		
	}
	
	public void MoveUnit( int unitId , Vector3 targetLocation )
	{
		GameObject unitToMove = GetUnit( unitId );
		if( unitToMove != null )
		{
			unitToMove.GetComponent<UnitController>().SetState( 3 );
			unitToMove.GetComponent<UnitController>().SetTargetDestination( targetLocation );
		}
	}
	
	public GameObject GetUnit(int unitId )
	{
		return new GameObject();
	}
	
	public void AttackUnit( int AttackerID , int targetID )
	{
		if( actionsLeft <= 0 )
		{
			return;
		}
		// Later. I need to think on this a bit.
	}
	
	public void PerformPendingActions()
	{
		if( myTurn )
		{
			return;
		}
		// Perform pending actions. 
		// And set my turn.
		myTurn = true;
		actionsLeft = 5;
	}
		
	public void PerformMoveAction( string owner , int unitId , Vector3 targetLocation )
	{
		MoveUnit( unitId , targetLocation );
		
		if( owner == playerName )
		{
			//RecordAction( Enum actionType , string owner , int attackerId , int targetId, Vector3 targetLocation );
			actionsLeft--;
		}
	}
	
	public void PerformAttackAction( string owner , int attackerId , int targetId )
	{
		AttackUnit( attackerId , targetId );
		
		if( owner == playerName )
		{
			//RecordAction( Enum actionType , string owner , int attackerId , int targetId , Vector3 targetLocation );
			actionsLeft--;
		}
	}
	
	private void RecordMoveAction( string owner , int unitId , Vector3 targetLocation )
	{
		Action newAction = new Action();
		newAction.Init(1, unitId, targetLocation);
		actionsInTurn.Add(newAction);
		actionsLeft--;
	}
	
	public void RecordAttackAction( string owner , int attackerId , int targetId )
	{
		Action newAction = new Action();
		newAction.Init(2, attackerId, targetId);
		actionsInTurn.Add(newAction);
		actionsLeft--;
	}
	
	public void Update()
	{
		if( myTurn && actionsLeft <= 0 )
		{
			
		}
	}
	
	public void SetPlayerName(string name)
	{
		playerName = name;
	}
	
	public void SetOppenentName(string name)
	{
		opponentName = name;
	}
	
	public string GetPlayerName()
	{
		return playerName;
	}
	
	public string GetOppenentNameName()
	{
		return opponentName;
	}
	
	public void SetSelectedUnit(GameObject obj)
	{
		selectedUnit = obj;
	}
	
	public GameObject GetSelectedUnit()
	{
		return selectedUnit;
	}

}
