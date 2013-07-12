using UnityEngine;
using System.Collections;

public class MatchController : MonoBehaviour {
	
	private int matchID = -1;
	private string opponentName = "";
	private string playerName = "";
	private GameObject selectedUnit = null;
	private int actionsLeft = 0;
	private bool isMyTurn = false;
	private enum ActionType { MOVE , ATTACK , POWER };
	
	// Use this for initialization
	void Start () 
	{
		
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
		if( isMyTurn )
		{
			return;
		}
		// Perform pending actions. 
		// And set my turn.
		isMyTurn = true;
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
		actionsLeft--;
	}
	
	public void RecordAttackAction( string owner , int attackerId , int targetId )
	{
		actionsLeft--;
	}
	
	public void Update()
	{
		if( isMyTurn && actionsLeft <= 0 )
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
