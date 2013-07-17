using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
	
	public Vector3 rayHitPoint;
	public uint onTileIndex;
	
	public string unitType = "Melee";
	public bool selected = false;
	public bool inRange = false;
	public uint attackRange = 0;
	public uint damage = 0;
	public uint meleeArmour = 0;
	public uint rangedArmour = 0;
	public uint moveRangeLR = 1;
	public uint moveRangeUD = 1;
	public uint moveRangeDiag = 1;
	public int HitPoints = 10;
	
	public enum UnitState { NONE = 0  , CREATED = 1 , IDLE = 2 , MOVING = 3 , ATTACKING = 4 , TAKEHIT = 5 , DIEING = 6 , DEAD = 7 };
	
	private UnitState currentState = UnitState.NONE;
	private UnitState previousState = UnitState.NONE;
	
	public string unitOwner = "";
	private int unitID = -1;
	private Vector3 targetDestination = new Vector3( -1.0f , -1.0f , -1.0f );
	public GameObject target;
	
	private MatchController matchController;
	
	// Use this for initialization
	void Start () 
	{
		rayHitPoint = new Vector3( 0.0f , 0.0f , 0.0f );
		onTileIndex = 0;//maybe scan tile array at start ?
		renderer.material.color = new Color( 1 , 1 , 1 , 0.5f );
		gameObject.AddComponent("InputInterface");
		
		matchController = GameObject.Find("MatchControllerObj").GetComponent<MatchController>();
		gameObject.tag = "Unit";
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
		t_arraySize = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileSize;
		GameObject pObj_;
		float offsetXZ = 0.2f;
		
		for ( uint i = 0; i < t_arraySize; i++ ){
			
			
			//pObj_ = GameObject.FindWithTag("Scripts/UserScripts/JTS/JTSScene0").GetComponent<JTSScene0>().tileArray[ i ];
			pObj_ = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileArray[ i ];
			//Color colourDestination = Color.white;
			pObj_.renderer.material.color = new Color( 0.0f , 0.00f , 0.0f , 0.0f );//colourDestination;
			
			if (
				transform.position.x > pObj_.transform.position.x - offsetXZ &&
				transform.position.x < pObj_.transform.position.x + offsetXZ &&	
				transform.position.z > pObj_.transform.position.z - offsetXZ &&
				transform.position.z < pObj_.transform.position.z + offsetXZ	
			)
				
			
			{	
				//Set player tile colour.
				//Color colourDestination = Color.red;
				//pObj_.renderer.material.color = colourDestination;
				
				t_return = i;
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
		
		// Highlight tiles left and right
		for(int i=0; i<moveRangeLR; i++)
		{
			// get i+1th tile left of onTileIndex and call Highlight on it
			// get i+1th tile right of onTileIndex and call Highlight on it
			
			// for example, if onTileIndex = 3 tile on 3rd row, and i=0, highlight 2nd and 4th tile on 3rd row
			// and if i =1 the highlight 1st and 5th tile on 3rd row
			
			// I am not sure how you are storing and retrieving tiles so I will leave this part to you.
			// Try to do it if you are stuck somewhere wait for me to come online :)
		}
		
		// Highlight tiles up and down
		for(int i=0; i<moveRangeUD; i++)
		{
			// get i+1th tile top of onTileIndex and call Highlight on it
			// get i+1th tile bottom of onTileIndex and call Highlight on it
		}
		
		// Highlight tiles diagonal to current tile
		for(int i=0; i<moveRangeLR; i++)
		{
			// get i+1th tile left+up of onTileIndex and call Highlight on it
			// get i+1th tile right+up of onTileIndex and call Highlight on it
			// get i+1th tile left+down of onTileIndex and call Highlight on it
			// get i+1th tile right+down of onTileIndex and call Highlight on it
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