using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
	
	public Vector3 rayHitPoint;
	public uint onTileIndex;
	
	// Use this for initialization
	void Start () {
		rayHitPoint = new Vector3( 0.0f , 0.0f , 0.0f );
		onTileIndex = 999;//maybe scan tile array at start ?
		renderer.material.color = new Color( 1 , 1 , 1 , 0.5f );
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastToTerrain();
		touch();
		onTileIndex = getTileIndexAtPlayer();

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
					float t_y;
					t_y = pObj_.transform.position.y;
					
					transform.position = new Vector3( pObj_.transform.position.x , transform.position.y , pObj_.transform.position.z );
				}
				t_return = i;
				Debug.Log ( pObj_.renderer.material.color.g + " " + t_return );
			}
			
			
			//Color colourDestination = Color.white;
			pObj_.renderer.material.color = new Color( 1.0f , 1.0f , 1.0f , 1.0f );//colourDestination;
			
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
			pObj_.renderer.material.color = new Color( 1.0f , 1.0f , 1.0f , 0.0f );//colourDestination;
			
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
	
	void setTileColour( uint index_ ){
		
		uint t_arraySize;
		//t_arraySize = GameObject.FindWithTag("Scripts/UserScripts/JTS/JTSScene0").GetComponent<JTSScene0>().tileSize;
		t_arraySize = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileSize;
	
		GameObject pObj_ , pPlayerTile_;
		//pPlayerTile_ = GameObject.FindWithTag("Scripts/UserScripts/JTS/JTSScene0").GetComponent<JTSScene0>().tileArray[ index_ ];
		pPlayerTile_ = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileArray[ index_ ];
		
		Color colourDestination = Color.red;
		
		
		//if player.type = blah, do this, or do that... { ?different loop? }
		uint characterType = 0; //Maybe Enumeration?

		uint highlightIndex = 999;
		float debug_distance;
		float offsetXZ = 2.0f;
		
		switch( characterType ){
			
			case 0:
				//left
				highlightIndex = index_ - 5;
				if ( highlightIndex >= 0 && highlightIndex < t_arraySize ){
					//pObj_ = GameObject.FindWithTag("Scripts/UserScripts/JTS/JTSScene0").GetComponent<JTSScene0>().tileArray[ highlightIndex ];
					pObj_ = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileArray[ highlightIndex ];
					debug_distance = distancePoints( pPlayerTile_.transform.position.x , pPlayerTile_.transform.position.z , pObj_.transform.position.x , pObj_.transform.position.z );
					if ( debug_distance < offsetXZ ){
						pObj_.renderer.material.color = colourDestination;
					}
				}
			
				//right
				highlightIndex = index_ + 5;
				if ( highlightIndex >= 0 && highlightIndex < t_arraySize ){
					//pObj_ = GameObject.FindWithTag("Scripts/UserScripts/JTS/JTSScene0").GetComponent<JTSScene0>().tileArray[ highlightIndex ];
					pObj_ = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileArray[ highlightIndex ];
					debug_distance = distancePoints( pPlayerTile_.transform.position.x , pPlayerTile_.transform.position.z , pObj_.transform.position.x , pObj_.transform.position.z );
					if ( debug_distance < offsetXZ ){
						pObj_.renderer.material.color = colourDestination;
					}
				}
			
				//up
				highlightIndex = index_ + 1;
				if ( highlightIndex >= 0 && highlightIndex < t_arraySize ){
					//pObj_ = GameObject.FindWithTag("Scripts/UserScripts/JTS/JTSScene0").GetComponent<JTSScene0>().tileArray[ highlightIndex ];
					pObj_ = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileArray[ highlightIndex ];
					debug_distance = distancePoints( pPlayerTile_.transform.position.x , pPlayerTile_.transform.position.z , pObj_.transform.position.x , pObj_.transform.position.z );
					if ( debug_distance < offsetXZ ){
						pObj_.renderer.material.color = colourDestination;
					}
				}
			
				//down
				highlightIndex = index_ - 1;
				if ( highlightIndex >= 0 && highlightIndex < t_arraySize ){
					//pObj_ = GameObject.FindWithTag("Scripts/UserScripts/JTS/JTSScene0").GetComponent<JTSScene0>().tileArray[ highlightIndex ];
					pObj_ = GameObject.FindWithTag("oScene0").GetComponent<JTSScene0>().tileArray[ highlightIndex ];
					debug_distance = distancePoints( pPlayerTile_.transform.position.x , pPlayerTile_.transform.position.z , pObj_.transform.position.x , pObj_.transform.position.z );
					if ( debug_distance < offsetXZ ){
						pObj_.renderer.material.color = colourDestination;
					}
				}
			
			break;
		}			
		
	}	
	
}