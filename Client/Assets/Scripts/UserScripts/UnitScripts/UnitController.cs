using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
	
	public Vector3 rayHitPoint;
	public uint onTileIndex;
	private GameObject onTileObj;
	
	public string unitType = "Melee";
	public bool selected = false;
	public bool inRange = false;
	public uint attackRange = 0;
	public uint damage = 0;
	public uint meleeArmour = 0;
	public uint rangedArmour = 0;
	public int moveRangeLR = 1;
	public int moveRangeUD = 1;
	public int moveRangeDiag = 1;
	public int HitPoints = 10;
	
	public enum UnitState { NONE = 0  , CREATED = 1 , IDLE = 2 , MOVING = 3 , ATTACKING = 4 , TAKEHIT = 5 , DIEING = 6 , DEAD = 7 };
	
	private UnitState currentState = UnitState.NONE;
	private UnitState previousState = UnitState.NONE;
	
	public string unitOwner = "1";
	private int unitID = -1;
	private Vector3 targetDestination = new Vector3( -1.0f , -1.0f , -1.0f );
	public GameObject target;
	
	private MatchController matchController;
	private JTSScene0 scene0Script;
	private GameObject[] tilesArray;
	
	// Use this for initialization
	void Start () 
	{
		rayHitPoint = new Vector3( 0.0f , 0.0f , 0.0f );
		onTileIndex = 0;//maybe scan tile array at start ?
		renderer.material.color = new Color( 1 , 1 , 1 , 0.5f );
		gameObject.AddComponent("InputInterface");
		
		matchController = GameObject.Find("MatchControllerObj").GetComponent<MatchController>();
		gameObject.tag = "Unit";
		
		GameObject pObj_;
		pObj_ = GameObject.FindWithTag("oScene0");
		scene0Script = pObj_.GetComponent<JTSScene0>();
		tilesArray = scene0Script.tileArray;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(currentState)
		{
			case UnitState.CREATED:	// Creation stuff if applicable;
				SetState(2);
				break;
			case UnitState.IDLE: 
				break;
			case UnitState.MOVING: 
				// Movement Code.
				// On destination reached, set state to idle.
				// Debug.Log(currentState);
				break;
			case UnitState.ATTACKING: 
				break;
			case UnitState.TAKEHIT: 
				break;
			case UnitState.DIEING: 
				break;
			case UnitState.DEAD: 
				break;
		}
	}
	
	void LateUpdate()
	{
		if(matchController == null)
		{
			matchController = GameObject.Find("MatchControllerObj").GetComponent<MatchController>();
		}
		if(scene0Script == null)
		{
			GameObject pObj_;
			pObj_ = GameObject.FindWithTag("oScene0");
			scene0Script = pObj_.GetComponent<JTSScene0>();
		}
	}
	
	void OnMouseDown()
	{
		//Debug.Log("Unit Tapped!");
		if(matchController != null)
		{
			matchController.UnHighlightTiles();
			if(unitOwner.Equals(matchController.GetPlayerName()))
			{
				Debug.Log("Unit Tapped 2!");
				matchController.SetSelectedUnit(this.gameObject);
				HighlightMovementRange();
				HighlightAttackRange();
			}
			else if(matchController.GetSelectedUnit() != null)
			{
				if(inRange)
				{
					matchController.PerformAttackAction(unitID);
				}
			}
		}
	}

	
	public int GetUnitId()
	{
		return unitID;
	}

	uint getTileIndexAtPlayer()
	{
		uint t_return = 999;
		uint t_arraySize;
		//t_arraySize = GameObject.FindWithTag("Scripts/UserScripts/JTS/JTSScene0").GetComponent<JTSScene0>().tileSize;
		
		GameObject tileObj;
		float offsetXZ = 0.2f;
		
		t_arraySize = scene0Script.tileSize;		
		
		for ( uint i = 0; i < t_arraySize; i++ ){
			
			//pObj_ = GameObject.FindWithTag("Scripts/UserScripts/JTS/JTSScene0").GetComponent<JTSScene0>().tileArray[ i ];
			tileObj = scene0Script.tileArray[ i ];
			//Color colourDestination = Color.white;
			//pObj_.renderer.material.color = new Color( 0.0f , 0.00f , 0.0f , 0.0f );//colourDestination;
			
			if (
				transform.position.x > tileObj.transform.position.x - offsetXZ &&
				transform.position.x < tileObj.transform.position.x + offsetXZ &&	
				transform.position.z > tileObj.transform.position.z - offsetXZ &&
				transform.position.z < tileObj.transform.position.z + offsetXZ	
			)
				
			
			{	
				//Set player tile colour.
				//Color colourDestination = Color.red;
				//pObj_.renderer.material.color = colourDestination;
				
				t_return = i;
				onTileObj = tileObj;
				break;
				//Debug.Log ( pObj_.tag + " " + t_return );
			}
			
			
		}
		
		//setTileColour( t_return );
		
		return t_return;
		
	}
	
	float distancePoints( float u1 , float u2 , float v1 , float v2 )
	{
		return Mathf.Sqrt( ( ( u1 - v1 ) * ( u1 - v1 ) ) + ( ( u2 - v2 ) * ( u2 - v2 ) ) );
	}
		
	public void HighlightMovementRange()
	{
		onTileIndex = getTileIndexAtPlayer();
		TileController onTileController = onTileObj.GetComponent<TileController>();
		
		//Debug.Log("onTileController.row = " + onTileController.row);
		//Debug.Log("onTileController.column = " + onTileController.column);
		
		foreach(GameObject obj in tilesArray)
		{
			TileController tc = obj.GetComponent<TileController>();
			//Debug.Log("tc.row = " + tc.row);
			//Debug.Log("tc.column = " + tc.column);
					
			if( ( (tc.column <= onTileController.column + moveRangeLR) && (tc.column >= onTileController.column - moveRangeLR) ) && (tc.row == onTileController.row) )
			{
				tc.HighlightTile("red");
			}
			
			if( ( (tc.row <= onTileController.row + moveRangeUD) && (tc.row >= onTileController.row - moveRangeUD) ) && (tc.column == onTileController.column) )
			{
				tc.HighlightTile("red");
			}
			/*
			if( ( (tc.row <= onTileController.row + moveRangeDiag) && (tc.row > onTileController.row) )&& ( (tc.column <= onTileController.column + moveRangeDiag) && (tc.column >= onTileController.column - moveRangeDiag) && (tc.column != onTileController.column) ) )
			{
				tc.HighlightTile("red");
			}
			
			if( ( (tc.row >= onTileController.row - moveRangeDiag) && (tc.row < onTileController.row) ) && ( (tc.column <= onTileController.column + moveRangeDiag) && (tc.column >= onTileController.column - moveRangeDiag) && (tc.column != onTileController.column) ) )
			{
				tc.HighlightTile("red");
			}
			*/
		}
		
	}
	
	public void HighlightAttackRange()
	{
		// Later :)
	}
	
	
	public void SetState( int newState )
	{
		previousState = currentState;
		currentState = (UnitState)newState;
	}
	
	public void TakeDamage( uint damage_ , string unitType_ )
	{
		//HitPoints -= ( damage_ - Armour ); // Armour is respective to the type of attack.
		if ( HitPoints <= 0 )
		{
			SetState( 6 );
		}
	}	
	
	public void SetTargetDestination( Vector3 newDestination )
	{
		targetDestination = newDestination;
	}

}