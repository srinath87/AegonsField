using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {
	
	
	public bool isHighlighted;
	public bool isOccupied;
	public int row = 0;
	public int column = 0;
	public MatchController matchController;
	
	// Use this for initialization
	void Start () 
	{
		gameObject.AddComponent("InputInterface");
		UnHighlightTile();
		matchController = GameObject.Find("MatchControllerObj").GetComponent<MatchController>();
		isOccupied = false;
	}
	
	public void HighlightTile( string colour_ )
	{
		if ( !isOccupied )	
		{
			float t_alpha = 1.0f;
			switch( colour_ )
			{
				
				case "red":	renderer.material.color = new Color( 1.0f, 0.0f , 0.0f , t_alpha ); break;
				case "blue": renderer.material.color = new Color( 0.0f, 0.0f , 1.0f , t_alpha ); break;
				case "green": renderer.material.color = new Color( 0.0f, 1.0f , 0.0f , t_alpha ); break;
				
			}
			isHighlighted = true;
		}	
	}
	
	public void UnHighlightTile()
	{
		renderer.material.color = new Color( 1.0f , 1.0f , 1.0f , 0.0f );
		isHighlighted = false;	
	}		
	
	
	public void OnMouseDown()
	{
		//Debug.Log("Tile Tapped!");
		if ( isHighlighted )	
		{
			if(matchController.GetSelectedUnit() != null)
			{
				matchController.PerformMoveAction(gameObject.transform.position);
			}
		}
		matchController.UnHighlightTiles();
		matchController.SetSelectedUnit(null);
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		
		if(matchController == null)
		{
			matchController = GameObject.Find("MatchControllerObj").GetComponent<MatchController>();
		}
		/*
		if(isHighlighted)
		{
			renderer.material.color = new Color(1.0f, 0f, 0f, 1.0f);
		}
		else
		{
			renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
		}
		*/
		
	}
	
}
