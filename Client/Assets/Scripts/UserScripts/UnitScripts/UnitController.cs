using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
	
	public Vector3 rayHitPoint;
	public uint onTileIndex;
	
	public string unitType = "Melee";
	public bool isSelected = false;
	public uint attackRange = 0;
	public uint damage = 0;
	public uint meleeArmour = 0;
	public uint rangedArmour = 0;
	public uint moveRangeLR = 0;
	public uint moveRangeUD = 0;
	public uint moveRangeDiag = 0;
	public int HitPoints = 10;
	
	public enum UnitState { NONE = 0  , CREATED = 1 , IDLE = 2 , MOVING = 3 , ATTACKING = 4 , TAKEHIT = 5 , DIEING = 6 , DEAD = 7 };
	
	private UnitState currentState = UnitState.NONE;
	private UnitState previousState = UnitState.NONE;
	
	private string unitOwner = "none";
	private int unitID = -1;
	private Vector3 targetDestination = new Vector3( -1.0f , -1.0f , -1.0f );
	
	
	// Use this for initialization
	void Start () {
		rayHitPoint = new Vector3( 0.0f , 0.0f , 0.0f );
		onTileIndex = 0;//maybe scan tile array at start ?
		renderer.material.color = new Color( 1 , 1 , 1 , 0.5f );
		
	}
	
	// Update is called once per frame
	void Update () {
		
		OnTap();
		

		onTileIndex = getTileIndexAtPlayer();

	}
	
	void OnTap()
	{
		
		RaycastToTerrain(); //Windows
		touch(); //Mobile device
		
	}
	
	public int GetUnitId()
	{
		return unitID;
	}

	void RaycastToTerrain()
	{
		
		if ( Input.GetMouseButtonDown( 0 ) ){
			int layerMask = ~( 1 << 8 );
			Debug.Log ( layerMask );
		    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		    RaycastHit hit = new RaycastHit();
		    if ( Physics.Raycast ( ray , out hit , Mathf.Infinity , layerMask ) )
		    {
		        Debug.DrawLine (ray.origin, hit.point);
				Debug.Log( hit.point );
				getTileIndexAtTouch( hit.point );
				
		    }
		}
	}
	
	void touch(){
        int fingerCount = 0;
        foreach (Touch touch in Input.touches) {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                fingerCount++;
            
        }
        if (fingerCount > 0)
            print("User has " + fingerCount + " finger(s) touching the screen");
	}
	
	uint getTileIndexAtTouch( Vector3 touchVector_ ){
		
		
		uint t_return = 999;
		uint t_arraySize;
		t_arraySize = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileSize;
		GameObject pObj_;
		float offsetXZ = 0.5f;
		
		for ( uint i = 0; i < t_arraySize; i++ ){
			
			pObj_ = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileArray[ i ];
			
			if (
				touchVector_.x > pObj_.transform.position.x - offsetXZ &&
				touchVector_.x < pObj_.transform.position.x + offsetXZ &&	
				touchVector_.z > pObj_.transform.position.z - offsetXZ &&
				touchVector_.z < pObj_.transform.position.z + offsetXZ	
			)
				
			
			{	
				//Set player tile colour.
				//Color colourDestination = Color.red;
				//pObj_.renderer.material.color = colourDestination;
				
				//Lazy i know!, if only equal to red.
				if ( pObj_.renderer.material.color.g == 0 ){
					//float t_y;
					//t_y = pObj_.transform.position.y;
					
					transform.position = new Vector3( pObj_.transform.position.x , transform.position.y , pObj_.transform.position.z );
				}
				t_return = i;
				Debug.Log ( pObj_.renderer.material.color.g + " " + t_return );
			}
			
			
			//Color colourDestination = Color.white;
			pObj_.renderer.material.color = new Color( 0.0f , 0.0f , 0.0f , 0.0f );//colourDestination;
			
		}
		
		return t_return;
		
	}
		
	uint getTileIndexAtPlayer(){
		
		
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
		
		setTileColour( t_return );
		
		return t_return;
		
	}
	
	float distancePoints( float u1 , float u2 , float v1 , float v2 ){
		return Mathf.Sqrt( ( ( u1 - v1 ) * ( u1 - v1 ) ) + ( ( u2 - v2 ) * ( u2 - v2 ) ) );
	}
	
	
	
		
	public void HighlightMovementRange()
	{
	// Go through all the 3 movement ranges and highlight the squares using 				// TileController.HighlightTile(color). Make it green or blue instead of red. We can use red 	// for attack range.
	
	}
	
	public void HighlightAttackRange()
	{
	// Go through the attack range, and if any units are standing on the blocks within that 	// range and their unitOwner is not same as the selected unit's unitOwner, then 	
	// highlight that square red.
	// Same as before, use TileController.HighlightTile(color)
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

	
	
	
	
	
	void highlightByIndex( uint index_ , float offset_ )
	{
		
		uint t_arraySize;
		t_arraySize = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileSize;
	
		GameObject pObj_ , pPlayerTile_;
		pPlayerTile_ = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileArray[ onTileIndex ];
		
		float debug_distance;
		uint highlightIndex;
		
		highlightIndex = index_;
		if ( highlightIndex >= 0 && highlightIndex < t_arraySize )
		{
			pObj_ = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileArray[ highlightIndex ];
			debug_distance = distancePoints( pPlayerTile_.transform.position.x , pPlayerTile_.transform.position.z , pObj_.transform.position.x , pObj_.transform.position.z );
			
			if ( debug_distance < offset_ )
			{
				//pObj_.renderer.material.color = colourDestination;
				TileController t_tile = ( TileController )pObj_.GetComponent( typeof( TileController ) );
				t_tile.HighlightTile( "red" );
			}
		}
		
		
		
	}
	
	void setTileColour( uint index_ ){
	

		uint characterType = 2;
		
		switch( characterType ){
			
			case 0:
			
				highlightByIndex( index_ - 5 , 2.0f );
				highlightByIndex( index_ + 5 , 2.0f );
				highlightByIndex( index_ + 1 , 2.0f );
				highlightByIndex( index_ - 1 , 2.0f );
			
			break;
			
			case 1:
				
				highlightByIndex( index_ - 5 , 2.0f );
				highlightByIndex( index_ + 5 , 2.0f );
				highlightByIndex( index_ + 1 , 2.0f );
				highlightByIndex( index_ - 1 , 2.0f );
			
				highlightByIndex( index_ + 2 , 3.0f );
				highlightByIndex( index_ - 2 , 3.0f );
				highlightByIndex( index_ - 10 , 3.0f );
				highlightByIndex( index_ + 10 , 3.0f );
				highlightByIndex( index_ - 4 , 3.0f );
				highlightByIndex( index_ + 6 , 3.0f );
				highlightByIndex( index_ + 4 , 3.0f );
				highlightByIndex( index_ - 6 , 3.0f );

			
			break;
				
			case 2:
			

			
				highlightByIndex( index_ - 5 , 2.0f ); 
				highlightByIndex( index_ + 5 , 2.0f );
				highlightByIndex( index_ + 1 , 2.0f );
				highlightByIndex( index_ - 1 , 2.0f );
			
				highlightByIndex( index_ + 2 , 3.0f );
				highlightByIndex( index_ - 2 , 3.0f );
				highlightByIndex( index_ - 10 , 3.0f );
				highlightByIndex( index_ + 10 , 3.0f );
				highlightByIndex( index_ - 4 , 3.0f );
				highlightByIndex( index_ + 6 , 3.0f );
				highlightByIndex( index_ + 4 , 3.0f );
				highlightByIndex( index_ - 6 , 3.0f );
			
				highlightByIndex( index_ - 15 , 3.4f );
				highlightByIndex( index_ - 11 , 3.4f );
				highlightByIndex( index_ - 9 , 3.4f );
				highlightByIndex( index_ - 7 , 3.4f );
				highlightByIndex( index_ - 3 , 2.4f );
				highlightByIndex( index_ + 3 , 2.4f );
				highlightByIndex( index_ + 7 , 3.4f );
				highlightByIndex( index_ + 9 , 3.4f );
				highlightByIndex( index_ + 11 , 3.4f );
				highlightByIndex( index_ + 15 , 3.4f );
			break;			
			
		}			
		
	}	
	
}