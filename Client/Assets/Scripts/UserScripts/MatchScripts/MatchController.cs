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
	
	public void Update()
	{
		RaycastToMouseClick();
		//TouchScreen();
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
	
	void RaycastToMouseClick()
	{
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			int layerMask = ~( 1 << 8 );
			//Debug.Log ( layerMask );
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		    RaycastHit hit = new RaycastHit();
		    if ( Physics.Raycast ( ray , out hit , Mathf.Infinity , layerMask ) )
		    {
		        Debug.DrawLine (ray.origin, hit.point);
				Debug.Log( hit.point );
				//getTileIndexAtTouch( hit.point );
				hit.transform.gameObject.GetComponent<InputInterface>().Tapped();
		    }
		}
	}
	
	void TouchScreen()
	{
        int fingerCount = 0;
		int layerMask = ~( 1 << 8 );
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 0f));
		RaycastHit hit = new RaycastHit();
        foreach (Touch touch in Input.touches) 
		{
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			{
				if ( Physics.Raycast ( ray , out hit , Mathf.Infinity , layerMask ) )
			    {
			        //Debug.DrawLine (ray.origin, hit.point);
					//Debug.Log( hit.point );
					//getTileIndexAtTouch( hit.point );
					hit.transform.gameObject.GetComponent<InputInterface>().Tapped();
			    }
			}
               // fingerCount++;
        }
       // if (fingerCount > 0)
           // print("User has " + fingerCount + " finger(s) touching the screen");
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
		// Needs to be implemented later.
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
	public void PerformMoveAction( Vector3 targetLocation )
	{
		PerformMoveAction(playerName, selectedUnit.GetComponent<UnitController>().GetUnitId(), targetLocation);
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
	
	public void PerformAttackAction( int targetId )
	{
		PerformAttackAction(playerName, selectedUnit.GetComponent<UnitController>().GetUnitId(), targetId);
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
	
	public void UnHighlightTiles()
	{
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
		foreach(GameObject tile in tiles)
		{
			tile.GetComponent<TileController>().UnHighlightTile();
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
