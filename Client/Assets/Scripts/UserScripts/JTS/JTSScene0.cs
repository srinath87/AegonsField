using UnityEngine;
using System.Collections;

public class JTSScene0 : MonoBehaviour {
	
	
	public GameObject[] tileArray;
	public uint tileSize;
	public uint tileIndex;
	
	public GameObject[] playerArray;
	public uint playerSize;
	public uint playerIndex;
	
	public GameObject[] enemyArray_debug;
	public uint enemyIndex_debug;	
	
	// Use this for initialization
	void Start () {
		
		//startTiles();
		//Set max size.
		tileSize = 50;
		//Set index.
		tileIndex = 0;
		//Set array.
		tileArray = new GameObject[ tileSize ];
		
		
		startPlayer();
		startEnemy_debug(); // delete EVERYTHING with -debug
		
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	
	
	/*
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
		for (int i = 0; i < 5; i = i + 1){
			for ( int j = 0; j < 10; j = j + 1 ){
				GameObject ins = ( GameObject )Instantiate( Resources.Load( "Levels/TestLevels/JasTestScene/JTS_Tile" ) );
				ins.transform.position = new Vector3( 10.0f + 1.0f * j , 0.0f , 8.0f + 1.0f * i );
				ins.GetComponent<TileController>().column = i+1;
				ins.GetComponent<TileController>().row = j+1;				
				//Color colorStart = Color.white;
				//ins.renderer.material.color = colorStart;
				//ins.setTransparency( 0.0f );
				tileArray[ tileIndex ] = ins;	
				tileIndex++;
			}
		}
		
	}*/

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
				GameObject ins = ( GameObject )Instantiate( Resources.Load( "Common/Units/PF_Swordsman" ) );
				ins.transform.position = new Vector3( 0 + 1 * i, 0.08f , 0 );
				//go.transform.position = new Vector3( -5.4f + 1.35f * j , 0.08f , 2.8f - 1.4f * i );
				ins.AddComponent("UnitController");
				ins.transform.localEulerAngles = new Vector3( 0.0f , 180.0f , 0.0f );
				ins.GetComponent<UnitController>().unitOwner = "player1";
				ins.GetComponent<UnitController>().unitID = 1; //debug
				playerArray[ playerIndex ] = ins;	
				playerIndex++;
			}
		}
		
		
	}
	
	public void addPlayer( Vector3 position_ ){
		GameObject ins = ( GameObject )Instantiate( Resources.Load( "Common/Units/PF_Swordsman" ) );
		ins.transform.position = position_;
		ins.AddComponent("UnitController");
		ins.transform.localEulerAngles = new Vector3( 0.0f , 180.0f , 0.0f );
		ins.GetComponent<UnitController>().unitOwner = "player1";
		ins.GetComponent<UnitController>().unitID = 1; //debug
		playerArray[ playerIndex ] = ins;	
		playerIndex++;
	}

	void startEnemy_debug() {
		
		//Set max size.
		uint enemySize = 50;
		//Set index.
		enemyIndex_debug = 0;
		//Set array.
		enemyArray_debug = new GameObject[ enemySize ];
		
		//Make enemies.
		for (int i = 0; i < 1; i = i + 1){
			for ( int j = 0; j < 1; j = j + 1 ){
				GameObject ins = ( GameObject )Instantiate( Resources.Load( "Common/Units/PF_Swordsman" ) );
				ins.transform.position = new Vector3( 2.8f, 0.08f , 0  );
				ins.AddComponent("UnitController");
				ins.GetComponent<UnitController>().unitOwner = "player2"; //set to be controlled by 'other'
				ins.GetComponent<UnitController>().unitID = 2; //debug
				ins.transform.localEulerAngles = new Vector3( 0.0f , 0.0f , 0.0f );
				enemyArray_debug[ enemyIndex_debug ] = ins;	
				enemyIndex_debug++;
			}
		}
		
		
	}
}
