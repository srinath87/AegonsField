using UnityEngine;
//using System.Collections;
using System.Collections.Generic;
public class UnitController : MonoBehaviour {
	
	public Vector3 rayHitPoint;
	public uint onTileIndex;
	private GameObject onTileObj;
	
	public string unitType = "Melee";
	public bool selected = false;
	public bool inRange = false;
	public uint attackRange = 2;
	public uint damage = 3;
	public uint meleeArmour = 1;
	public uint rangedArmour = 2;
<<<<<<< HEAD
	public int moveRangeLR = 5;
	public int moveRangeUD = 5;
	public int moveRangeDiag = 2;
=======
	public int moveRangeLR = 1;
	public int moveRangeUD = 1;
	public int moveRangeDiag;
>>>>>>> f7060d553739bce1804f68c3f6336cb79e8d20a7
	
	public bool unitDiag = false;
	
	public uint HitPoints = 10;
	public float moveSpeed = 1f;
	
	public enum UnitState { NONE = 0  , CREATED = 1 , IDLE = 2 , MOVING = 3 , ATTACKING = 4 , TAKEHIT = 5 , DIEING = 6 , DEAD = 7 };
	
	public UnitState currentState = UnitState.NONE;
	private UnitState previousState = UnitState.NONE;
	
	public string unitOwner = "1";
	public int unitID = -1;
	private Vector3 targetDestination = new Vector3( -1.0f , -1.0f , -1.0f );
	public GameObject target;
	
	private MatchController matchController;
	private JTSScene0 scene0Script;
	private GameObject[] tilesArray;
	private GameObject[] enemyArray;
	
	private List<Vector3> movementArray;// = new List<Vector3>();
	
	// GUI Window Rectangle
	private Rect _windowRect = new Rect(0, 0, 150, 40);
	private string _numColsString = "10", _numRowsString = "5";
	
	// Use this for initialization
	void Start () 
	{
		if ( unitDiag == false )
		moveRangeDiag = moveRangeLR - ( moveRangeLR - 1 );
		else moveRangeDiag = moveRangeLR;
		
		rayHitPoint = new Vector3( 0.0f , 0.0f , 0.0f );
		onTileIndex = 0;//maybe scan tile array at start ?
		//renderer.material.color = new Color( 1 , 1 , 1 , 0.5f );
		gameObject.AddComponent("InputInterface");
		
		matchController = GameObject.Find("MatchControllerObj").GetComponent<MatchController>();
		gameObject.tag = "Unit";
		
		movementArray = new List<Vector3>();
		
		GameObject pObj_;
		pObj_ = GameObject.FindWithTag("oScene0");
		scene0Script = pObj_.GetComponent<JTSScene0>();
		tilesArray = scene0Script.tileArray;
		enemyArray = scene0Script.enemyArray_debug;
		currentState = UnitState.CREATED;
	}
	
	public void CopyMovementArray( List<Vector3> vector3array_ ){
		movementArray = vector3array_;
	}

	
	
	void HighlightAll()	{

		/*
		string t_colour_movement = "white";
		
		foreach(GameObject obj in tilesArray)
		{
			TileController tc = obj.GetComponent<TileController>();
			//Debug.Log("tc.row = " + tc.row);
			//Debug.Log("tc.column = " + tc.column);
			tc.HighlightTile( t_colour_movement );		
			
			
		}*/
		
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
				//Debug.Log("Unit Moving");
				//Debug.Log(transform.position.x);
				//Debug.Log(targetDestination.x);
				//Debug.Log(transform.position.z);
				//Debug.Log(targetDestination.z);
				//HighlightAll();
			
		      	//animate walk
				animation.Play("Walk");
				//targetDestination = movementArray[ movementArrayIndex ];
				// Smoothly rotates towards target 
			
				setUnitRotation( targetDestination ); 
<<<<<<< HEAD
				Vector3 NextPositionDestination;
				if ( movementArray.Count > 1 ){
				NextPositionDestination = movementArray[ movementArray.Count - 1 ];
				}
				else NextPositionDestination = transform.position;
			
				if(transform.position.x != NextPositionDestination.x || transform.position.z != NextPositionDestination.z)
				{				
					transform.position = Vector3.MoveTowards(transform.position, new Vector3(NextPositionDestination.x, transform.position.y, NextPositionDestination.z), Time.deltaTime * moveSpeed);
				}
				else if ( movementArray.Count > 1 ){
					movementArray.RemoveAt( movementArray.Count - 1 );
					
=======
			
			
			
				
				if ( movementArray.Count > 0 ){
					Vector3 NextPositionDestination = movementArray[ movementArray.Count - 1 ];
					if(transform.position.x != NextPositionDestination.x || transform.position.z != NextPositionDestination.z)
					{				
						transform.position = Vector3.MoveTowards(transform.position, new Vector3(NextPositionDestination.x, transform.position.y, NextPositionDestination.z), Time.deltaTime * moveSpeed);
					}
					else 
					{  
						movementArray.RemoveAt( movementArray.Count - 1  ); 
					}
			
				}
				else
				if(transform.position.x != targetDestination.x || transform.position.z != targetDestination.z)
				{
					transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetDestination.x, transform.position.y, targetDestination.z), Time.deltaTime * moveSpeed);
>>>>>>> f7060d553739bce1804f68c3f6336cb79e8d20a7
				}
				else
				{
					animation.CrossFade("Idle");
					currentState = UnitState.IDLE;
					matchController.inAction = false;
					movementArray.Clear();

					GameObject pObj_;
					pObj_ = GameObject.FindWithTag("oAStar");
					AStarTest AStar;
					AStar = pObj_.GetComponent<AStarTest>();
					AStar.movementArray.Clear();
					AStar._ClearAStarGridMap();
					matchController.UnHighlightTiles();
				}
				break;
			case UnitState.ATTACKING: 
			
				//Debug.Log( "HI" );
				animation.CrossFade("Idle");
				currentState = UnitState.IDLE;
				matchController.inAction = false;
				break;
			case UnitState.TAKEHIT: 
				break;
			case UnitState.DIEING: 
				break;
			case UnitState.DEAD: 
				Debug.Log( "dead" );
				Destroy( gameObject );
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
		//Debug.Log(":)");
		if(currentState != UnitState.IDLE)
		{
			//Debug.Log(currentState.ToString());
			return;
		}
		
		if(matchController != null)
		{
			//Debug.Log(1);
			matchController.UnHighlightTiles();
			//Debug.Log(2);
			if(unitOwner.Equals(matchController.GetPlayerName()))
			{
				matchController.SetSelectedUnit(this.gameObject);
				//Debug.Log(matchController.GetSelectedUnit().ToString());
				HighlightMovementRange();
				HighlightAttackRange();
				//Debug.Log("HHH"); // why is this not getting called?
			}
			else if(matchController.GetSelectedUnit() != null)
			{
				//Debug.Log(inRange.ToString());
				if(inRange)
				{
					//Debug.Log("inRange");
					matchController.PerformAttackAction(unitID);
				}
				matchController.SetSelectedUnit(null);
			}	
		}
		
	}
	
	public bool GetUnitDiag()
	{
		return unitDiag;
	}
	
	public int GetUnitId()
	{
		return unitID;
	}

	uint getTileIndexAtUnit()
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
	
	public GameObject getTileCR( int column_ , int row_ ){
	
		foreach(GameObject obj in tilesArray)
		{
			TileController tc = obj.GetComponent<TileController>();	
			if ( tc.column == column_ && tc.row == row_ ){
				return obj;
			}
		}
		return null;
		
	}
		
	public void HighlightMovementRange()
	{
		
		
		
		onTileIndex = getTileIndexAtUnit();
		TileController onTileController;
		onTileController = onTileObj.GetComponent<TileController>();
		string t_colour_movement = "highlight";
		
		if ( unitDiag == true )
		{
			foreach(GameObject obj in tilesArray)
			{
				TileController tc = obj.GetComponent<TileController>();
				
				if ( tc.column <= onTileController.column + moveRangeLR )
				if ( tc.column >= onTileController.column - moveRangeLR )
				if ( tc.row <= onTileController.row + moveRangeLR )
				if ( tc.row >= onTileController.row - moveRangeLR )
				tc.HighlightTile( t_colour_movement );				
			}
			
			onTileController.HighlightTile( "blue" );
		}
		else
		{
			
			
			
			
			
			foreach(GameObject obj in tilesArray)
			{
				TileController tc = obj.GetComponent<TileController>();

				
				
				
				int p_f = onTileController.column + onTileController.row;
				int t_f = tc.column + tc.row;
				
				if ( tc.column >= onTileController.column && tc.row >= onTileController.row )
				{
					int f = t_f - p_f;
					if ( f <= moveRangeLR && f >= -moveRangeLR )
					
					tc.HighlightTile( t_colour_movement );	
				}
				else
				if ( tc.column <= onTileController.column && tc.row <= onTileController.row )
				{
					int f = t_f - p_f;
					if ( f <= moveRangeLR && f >= -moveRangeLR )
					
					tc.HighlightTile( t_colour_movement );	
				}
				else
				{
					
					p_f = onTileController.column - onTileController.row;
					t_f = tc.column - tc.row;
					int f = t_f - p_f;
					if ( f <= moveRangeLR && f >= -moveRangeLR )
					
					tc.HighlightTile( t_colour_movement );	
					
					
				}
				//if ( tc.column <= onTileController.column + moveRangeLR )
				//if ( tc.column >= onTileController.column - moveRangeLR )
				//if ( tc.row <= onTileController.row + moveRangeLR )
				//if ( tc.row >= onTileController.row - moveRangeLR )
							
			}
			
			onTileController.HighlightTile( "blue" );		
		}

			
			
			
		
		
		/*
		onTileController = onTileObj.GetComponent<TileController>();
		onTileObj = getTileCR ( onTileController.column - moveRangeLR , onTileController.row - moveRangeLR );
		
		onTileController = onTileObj.GetComponent<TileController>();
		

		
		string t_colour_movement = "highlight";
		
		int i , j;
		int d = moveRangeLR * 2;
		
		for ( i = onTileController.column; i < ( 1 + onTileController.column + d ); ++i ){ 
			for ( j = onTileController.row; j < ( 1 + onTileController.row + d ); ++j ){ 
				
				GameObject t = getTileCR( i , j );
				if ( t != null ){
					TileController tscr = t.GetComponent<TileController>();;
					tscr.HighlightTile( t_colour_movement );
				}
			}	
			
		}*/
		
		//highlight start point
		//onTileObj = getTileCR ( onTileController.column + moveRangeLR , onTileController.row + moveRangeLR );
		//onTileController = onTileObj.GetComponent<TileController>();

		
	}
	
	public void HighlightAttackRange()
	{
		/*
		// Later :)
		onTileIndex = getTileIndexAtUnit();
		TileController onTileController = onTileObj.GetComponent<TileController>();
		
		//Debug.Log("onTileController.row = " + onTileController.row);
		//Debug.Log("onTileController.column = " + onTileController.column);
		
		string t_colour_movement = "red";
		//int attackRange
		//scene0Script
		//bool inRange //set on enemy.inrange
		
		
		foreach( GameObject obj in enemyArray )
		{
			if(obj == null)
			{
				continue;
			}
			//Debug.Log(1);	
			UnitController uc = obj.GetComponent<UnitController>();
			//Debug.Log(2);
			uc.inRange = false; // not sure if needed, ( GARBAGE COLLECT? )
			//Debug.Log(3);
			TileController tc = tilesArray[ uc.getTileIndexAtUnit() ].GetComponent<TileController>();
			//Debug.Log(4);
			
			if( ( (tc.column <= onTileController.column + attackRange) && (tc.column >= onTileController.column - attackRange) ) //column
			&& (tc.row >= onTileController.row - attackRange) && ( tc.row <= onTileController.row + attackRange ) )              //row
			{
			
				// if in range do this... {
				uc.inRange = true;
				tc.HighlightTile( t_colour_movement );
				// }
			
			}
			
		}
		/*
		
		foreach(GameObject obj in tilesArray)
		{
			TileController tc = obj.GetComponent<TileController>();
			//Debug.Log("tc.row = " + tc.row);
			//Debug.Log("tc.column = " + tc.column);

			if( ( (tc.column <= onTileController.column + moveRangeLR) && (tc.column >= onTileController.column - moveRangeLR) ) && (tc.row == onTileController.row) )
			{
				tc.HighlightTile( t_colour_movement );
			}
			
			if( ( (tc.row <= onTileController.row + moveRangeUD) && (tc.row >= onTileController.row - moveRangeUD) ) && (tc.column == onTileController.column) )
			{
				tc.HighlightTile( t_colour_movement );
			}
			
			if( ( (tc.row <= onTileController.row + moveRangeDiag) && (tc.row > onTileController.row) )&& ( (tc.column <= onTileController.column + moveRangeDiag) && (tc.column >= onTileController.column - moveRangeDiag) && (tc.column != onTileController.column) ) )
			{
				tc.HighlightTile( t_colour_movement );
			}
			
			if( ( (tc.row >= onTileController.row - moveRangeDiag) && (tc.row < onTileController.row) ) && ( (tc.column <= onTileController.column + moveRangeDiag) && (tc.column >= onTileController.column - moveRangeDiag) && (tc.column != onTileController.column) ) )
			{
				tc.HighlightTile( t_colour_movement );
			}
			
		}

		*/
		
	}
	
	public void setUnitRotation( Vector3 location )
	{
		Vector3 destination = new Vector3( location.x , transform.position.y , location.z ); // ignore Y axis in the rotation.
        Quaternion targetRotation = Quaternion.LookRotation(destination - transform.position, Vector3.up);
    	transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 6.0f );
	}
	
	
	public void SetState( int newState )
	{
		previousState = currentState;
		currentState = (UnitState)newState;
	}
	
	public void TakeDamage( uint damage_ , string unitType_ )
	{
		
		uint armour = 0;
		
		switch( unitType_ )
		{
		
			case "Melee": armour = meleeArmour; break;
			case "Ranged": armour = rangedArmour; break;
			
		}
		Debug.Log( HitPoints + " damage: " + damage_ + " armour: " + armour );
		HitPoints -= ( damage_ - armour ); // Armour is respective to the type of attack.
		Debug.Log( HitPoints + " d-a: " + ( damage_ - armour ) );
		if ( HitPoints <= 0 )
		{
			SetState( 7 );
		}
	}	
	
	public void SetTargetDestination( Vector3 newDestination )
	{
		targetDestination = newDestination;
	}

}