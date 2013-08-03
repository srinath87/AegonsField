using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class MatchController : MonoBehaviour {
	
	private int matchID;
	public string playerName;
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
		playerName = "1";
		selectedUnit = null;
		actionsLeft = 2;
		myTurn = false;
		actionsInTurn = new List<Action>();
	}
	
	public void Update()
	{
		//RaycastToMouseClick();
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
	
	void TouchScreen()
	{
		RaycastHit[] hits;
		for (int i = 0; i < Input.touchCount; ++i) 
		{
			if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) 
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				hits = Physics.RaycastAll(ray);
				foreach(RaycastHit hit in hits) 
				{
					if(hit.transform.gameObject.CompareTag("oPlayer"))
					{
						hit.transform.gameObject.SendMessage("OnMouseDown");
						return;
					}
			 	}
				foreach(RaycastHit hit in hits) 
				{
					if(hit.transform.gameObject.CompareTag("oTile"))
					{
						hit.transform.gameObject.SendMessage("OnMouseDown");
						return;
					}
			 	}
		   }
	   }
	}
	
	public GameObject GetUnit(int unitId )
	{
		// Needs to be implemented later.
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Unit");
		for(int i=0; i<objs.Length; i++)
		{
			//UnitController cont = null;
			//cont = obj.GetComponent<UnitController>();
			if(/*cont != null && (cont*/objs[i].GetComponent<UnitController>().GetUnitId() == unitId)
			{
				//Debug.Log("Returning Unit " + objs[i] ); 
				return objs[i];
			}
		}
		return null;
	}	
	
	public void MoveUnit( int unitId , Vector3 targetLocation )
	{
		GameObject unitToMove = GetUnit( unitId );
		if( unitToMove != null )
		{
			UnitController controller = unitToMove.GetComponent<UnitController>();
			if(controller != null)
			{
				controller.SetState( 3 );
				controller.SetTargetDestination( targetLocation );
			}
		}
	}
	
	public void AttackUnit( int attackerID , int targetID )
	{
		/*
		if( actionsLeft <= 0 )
		{
			return;
		}
		*/ 
		//^ ERRORING, WILL NOT EXECUTE PAST.
		

		// Later. I need to think on this a bit.
		GameObject unitAttacking = GetUnit( attackerID );
		GameObject unitBeingAttack = GetUnit( targetID );
		
		//Debug.Log( "#$#$" + attackerID );
		
		//if( unitAttacking != null && unitBeingAttack != null )
		//{
			

			
			UnitController enemy = unitBeingAttack.GetComponent<UnitController>();
			UnitController player = unitAttacking.GetComponent<UnitController>();
			
			//if(enemy != null && player != null)
			//{
				//--single call(s), not 24/7
				
				//rotate player to face enemy
				Vector3 targetLocation = unitBeingAttack.transform.position;
				player.setUnitRotation( targetLocation );
				
				//set unit state to attacking
				player.SetState( 4 );

				//take damage
				enemy.TakeDamage( player.damage , player.unitType );
				enemy.animation.Play("Walk");
				enemy.renderer.material.color = new Color( 1.0f , 0.0f , 0.0f , 0.5f );
				//--.
					
			//}
		///}
		
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
		
		if( owner.Equals(playerName) )
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
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("oTile");
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
		//Debug.Log(selectedUnit.ToString());
		return selectedUnit;
	}

}
