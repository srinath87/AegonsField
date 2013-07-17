using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {
	
	
	public bool isHighlighted;
	public bool isOccupied;
	public MatchController controller;
	
	// Use this for initialization
	void Start () 
	{
		gameObject.AddComponent("InputInterface");
		UnHighlightTile();
		controller = GameObject.Find("MatchControllerObj").GetComponent<MatchController>();
		isOccupied = false;
	}
	
	public void HighlightTile( string colour_ )
	{
		if ( !isOccupied )	
		{
			isHighlighted = true;
		}	
	}
	
	public void UnHighlightTile()
	{
		renderer.material.color = new Color( 1.0f , 1.0f , 1.0f , 1.0f );
		isHighlighted = false;	
	}		
	
	
	public void OnMouseDown()
	{
		Debug.Log("Tile Tapped!");
		if ( isHighlighted )	
		{
			controller.PerformMoveAction(gameObject.transform.position);
		}
		controller.UnHighlightTiles();
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		if(controller == null)
		{
			controller = GameObject.Find("MatchControllerObj").GetComponent<MatchController>();
		}
		
		if(isHighlighted)
		{
			renderer.material.color = new Color(1.0f, 0f, 0f, 1.0f);
		}
		else
		{
			renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
		}
	}
	
}
