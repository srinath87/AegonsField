using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class MatchController : MonoBehaviour 
{
	
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
	private bool turnSubmitted;
	private bool showSubmitButton;
	public bool inAction;
	private bool hasPendingAction;
	private List<Action> pendingActions;
	private GameManager manager;
	
	// Use this for initialization
	void Start () 
	{
		//matchID = -1;
		opponentName = "";
		//playerName = "1";
		selectedUnit = null;
		actionsLeft = 5;
		myTurn = true;
		actionsInTurn = new List<Action>();
		pendingActions = new List<Action>();
		turnSubmitted = false;
		showSubmitButton = false;
		inAction = false;
		hasPendingAction = false;
	}
	
	public void Update()
	{
		if(myTurn)
		{
			if(actionsLeft <=0 && !turnSubmitted && !showSubmitButton)
			{
				showSubmitButton = true;
			}
		}
		if(!inAction && hasPendingAction)
		{
			Debug.Log(1);
			if(this.pendingActions.Count <= 0)
			{
				Debug.Log(3);
				Debug.Log(this.pendingActions.Count);
				hasPendingAction = false;
				StartTurn();
			}
			else
			{
				Debug.Log(2);
				PerformPendingActions();
			}
				
		}
		if(manager == null)
		{
			GameObject gameController = GameObject.Find("GameController");
			if(gameController != null)
			{
				manager = gameController.GetComponent<GameManager>();	
			}
		}
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
	
	public void Init(int matchID, bool myTurn)
	{
		this.matchID = matchID;
		this.myTurn = myTurn;
		if(this.myTurn)
		{
			actionsLeft = 5;
			actionsInTurn.Clear();
		}
		else
		{
			actionsLeft = 0;
			actionsInTurn.Clear();
		}
	}
	
	void OnGUI()
	{
		if(showSubmitButton)
		{
			if (GUI.Button (new Rect (5,1,150,100), "SubmitTurn")) 
			{
				if(manager != null)
				{
					manager.SubmitTurn(actionsInTurn);
					EndTurn();
				}
			}
		}
	}
	
	void EndTurn()
	{
		actionsInTurn.Clear();
		myTurn = false;
		showSubmitButton = false;
		actionsLeft = 0;
		turnSubmitted = true;
	}
	
	void StartTurn()
	{
		myTurn = true;
		showSubmitButton = false;
		actionsLeft = 5;
		turnSubmitted = false;
	}
	
	void TouchScreen()
	{
		RaycastHit[] hits;
		for (int i = 0; i < Input.touchCount; ++i) 
		{
			Debug.Log("Hit1");
			if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) 
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				hits = Physics.RaycastAll(ray);
				Debug.Log("Hit2");
//				foreach(RaycastHit hit in hits) 
//				{
//					if(hit.transform.gameObject.CompareTag("oPlayer"))
//					{
//						hit.transform.gameObject.SendMessage("OnMouseDown");
//						return;
//					}
//			 	}
				
				for( int j = 0; j < hits.Length; ++j ){
					Debug.Log("Hit3");
					RaycastHit hit = hits[ j ];
				//foreach(RaycastHit hit in hits) 
				//{
					var layerMask = 1 << 8;
					layerMask = ~layerMask;

					if (Physics.Raycast (transform.position,transform.TransformDirection (Vector3.forward),out hit, Mathf.Infinity, layerMask)) {

						if(hit.transform.gameObject.CompareTag("oTile"))
						{
							Debug.Log("Hit4");
							//hit.transform.gameObject.SendMessage("OnMouseDown");
							return;
						}
						else
						if(hit.transform.gameObject.CompareTag("oCard"))
						{
							//Debug.Log("Hit4");
							//GameObject objCard;
							//objCard = hit.transform.gameObject;
							//CardController script = objCard.GetComponent<CardController>();
						
							//uint g = 0;
							//script.OnTap( g );
							
							//hit.transform.gameObject.SendMessage("OnMouseDown");
						
							return;
						}
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
				inAction = true;
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
				inAction = true;
				//--.
					
			//}
		///}
		
	}
	
	public void SpawnUnit(string owner, string unitToSpawn, Vector3 spawnLocation, int unitId)
	{
		Debug.Log("Spawned : Common/Units/"+unitToSpawn);
		GameObject newUnit = Instantiate(Resources.Load("Common/Units/"+unitToSpawn)) as GameObject;		
		if(newUnit != null)
		{
			newUnit.transform.position = spawnLocation;
			UnitController cont = newUnit.GetComponent<UnitController>();
			if(cont != null)
			{
				cont.Init(owner, unitId, this);
			}
		}
	}
	
	public void SetPendingActions(List<Action> pendActions)
	{
		//Debug.Log(pendActions.Count);
		this.pendingActions = new List<Action>(pendActions);
		//Debug.Log(pendingActions.Count);
		//this.pendingActions.Reverse();
		//Debug.Log(pendingActions.Count);
		hasPendingAction = true;
	}
	
	public void PerformPendingActions()
	{
		//Debug.Log("In PerformPendingActions()");
		if( myTurn )
		{
			return;
		}
		inAction = true;
		int unitId, targetId;
		Vector3 targetLocation;
		Vector3 spawnLocation;
		string unitToSpawn;
		Action currentAction = (pendingActions.ToArray())[0];
		Actions act = currentAction.GetActionType();
		switch(act)
		{
			case Actions.MOVE: 
				unitId = currentAction.GetUnitID();
				targetLocation = currentAction.GetTargetLocation();
				PerformMoveAction(opponentName, unitId, targetLocation);
				pendingActions.Remove(currentAction);
				break;
				
			case Actions.ATTACK:
				unitId = currentAction.GetUnitID();
				targetId = currentAction.GetTargetId();
				PerformAttackAction(opponentName, unitId, targetId);
				pendingActions.Remove(currentAction);
				break;
			case Actions.SPAWN:
				unitToSpawn = currentAction.GetUnitToSpawn();
				spawnLocation = currentAction.GetSpawnLocation();
				unitId = currentAction.GetUnitID();
				PerformSpawnAction(opponentName, unitToSpawn, spawnLocation, unitId);
				pendingActions.Remove(currentAction);
				break;
		}
	}
		
	public void PerformMoveAction( string owner , int unitId , Vector3 targetLocation )
	{
		if( owner.Equals(playerName) )
		{
			if(actionsLeft <= 0)
			{
				return;
			}
			RecordMoveAction(owner, unitId, targetLocation );
			actionsLeft--;
		}
		MoveUnit( unitId , targetLocation );
	}
	
	public void PerformMoveAction( Vector3 targetLocation )
	{
		//generate path and list before moving
		GameObject pObj_;
		pObj_ = GameObject.FindWithTag("oAStar");
		AStarTest AStar;
		AStar = pObj_.GetComponent<AStarTest>();
		AStar._FindPath( selectedUnit.GetComponent<UnitController>().GetUnitDiag() );
		selectedUnit.GetComponent<UnitController>().CopyMovementArray( AStar.GetVectorArray());
		
		PerformMoveAction(playerName, selectedUnit.GetComponent<UnitController>().GetUnitId(), targetLocation);
	}	
	
	
	public void PerformAttackAction( string owner , int attackerId , int targetId )
	{
		if( owner.Equals(playerName) )
		{
			if(actionsLeft <= 0)
			{
				return;
			}
			RecordAttackAction(owner, attackerId, targetId);
			actionsLeft--;
		}
		AttackUnit( attackerId , targetId );
	}
	
	public void PerformAttackAction( int targetId )
	{
		PerformAttackAction(playerName, selectedUnit.GetComponent<UnitController>().GetUnitId(), targetId);
	}
	
	public void PerformSpawnAction(string owner, string unitToSpawn, Vector3 spawnLocation, int unitId)
	{
		if( owner.Equals(playerName) )
		{
			if(actionsLeft <= 0)
			{
				return;
			}
			RecordSpawnAction(owner, unitToSpawn, spawnLocation, unitId);
			actionsLeft--;
		}
		SpawnUnit( owner, unitToSpawn, spawnLocation, unitId );
	}
	
	public void PerformSpawnAction(string unitToSpawn, Vector3 spawnLocation)
	{
		PerformSpawnAction(playerName, unitToSpawn, spawnLocation, GenerateUnitId());
	}
	
	private void RecordMoveAction( string owner , int unitId , Vector3 targetLocation )
	{
		Action newAction = new Action();
		newAction.Init(1, unitId, targetLocation);
		actionsInTurn.Add(newAction);
		//actionsLeft--;
	}
	
	private void RecordAttackAction( string owner , int attackerId , int targetId)
	{
		Action newAction = new Action();
		newAction.Init(2, attackerId, targetId);
		actionsInTurn.Add(newAction);
		//actionsLeft--;
	}
	
	private void RecordSpawnAction(string owner, string unitToSpawn, Vector3 unitLocation, int unitId )
	{
		Action newAction = new Action();
		newAction.Init(4, unitToSpawn, unitLocation, unitId);
		actionsInTurn.Add(newAction);
	}
	
	public int GenerateUnitId()
	{
		return 1;
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
	
	public string GetOppenentName()
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
