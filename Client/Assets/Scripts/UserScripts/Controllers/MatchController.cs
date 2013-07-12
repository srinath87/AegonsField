using UnityEngine;
using System.Collections;

public class MatchController : MonoBehaviour {
	
	private int matchID = -1;
	private string opponentName = "";
	private string playerName = "";
	private GameObject selectedUnit = null;
	private int actionsLeft = 0;
	private bool isMyTurn = false;
	private enum ActionState { MOVE , ATTACK , POWER };
	
	// Use this for initialization
	void Start () {
	
	}
	
public void MoveUnit( int unitId , Vector3 targetLocation )
{
	unitToMove = GetUnit( unitId );
	if( unitToMove != none )
	{
		unitToMove.SetState( MOVING );
		unitToMove.SetDestination( targetLocation );
	}
}

public void GetUnit( int unitId )
{
	// Return unit with id = unitId
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


private void RecordAction( ActionState actionType , string owner , int attackerId , int targetId , Vector3 targetLocation )
{
	switch( actionType )
	{
		case 0: break;
		// Later. Ill fill this up.
	}
	actionsLeft--;
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

public void Update()
{
	if( isMyTurn && actionsLeft <= 0 )
	{
		// Make Submit Button Visible. Just for logic of what happens. We will come to 	// this later. When Submit button is clicked, we make actionsLeft = 5, isMyturn =                                                                                             	// false; etc.
	}
}

}
