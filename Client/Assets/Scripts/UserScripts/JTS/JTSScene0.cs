using UnityEngine;
using System.Collections;

public class JTSScene0 : MonoBehaviour {
	
	
	public GameObject[] tileArray;
	public uint tileSize;
	public uint tileIndex;
	
	public GameObject[] playerArray;
	public uint playerSize;
	public uint playerIndex;
	
	
	
	// Use this for initialization
	void Start () {
		
		startTiles();
		startPlayer();

		
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	
	
	
	void startTiles() {
		
		//Set max size.
		tileSize = 50;
		//Set index.
		tileIndex = 0;
		//Set array.
		tileArray = new GameObject[ tileSize ];
		
		//Temporary get height of terrain outside loop.
		//float terrain_height = 0.0f + Terrain.activeTerrain.SampleHeight( food.transform.position ); //ERROR, requires x and z to be set first.
		
		//Make tiles.
		for (int i = 0; i < 10; i = i + 1){
			for ( int j = 0; j < 5; j = j + 1 ){
				GameObject ins = ( GameObject )Instantiate( Resources.Load( "Levels/TestLevels/JasTestScene/JTS_Tile" ) );
				ins.transform.position = new Vector3( 10.0f + 1.0f * i , 0.0f , 8.0f + 1.0f * j );
				//Color colorStart = Color.white;
				//ins.renderer.material.color = colorStart;
				//ins.setTransparency( 0.0f );
				tileArray[ tileIndex ] = ins;	
				tileIndex++;
			}
		}
		
	}

	void startPlayer() {
		
		//Set max size.
		playerSize = 50;
		//Set index.
		playerIndex = 0;
		//Set array.
		playerArray = new GameObject[ playerSize ];
		
		//Make player.
		for (int i = 0; i < 1; i = i + 1){
			for ( int j = 0; j < 1; j = j + 1 ){
				GameObject ins = ( GameObject )Instantiate( Resources.Load( "Levels/TestLevels/JasTestScene/JTS_Player" ) );
				ins.transform.position = new Vector3( 11.0f + 1.0f * i , 0.5f , 11.0f + 1.0f * j );
				playerArray[ playerIndex ] = ins;	
				playerIndex++;
			}
		}
		
		
	}
		
}
