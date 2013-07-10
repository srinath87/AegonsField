using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {
	
	
	public bool isHighlighted;
	public bool isOccupied;
	
	// Use this for initialization
	void Start () {
		isHighlighted = false;
		isOccupied = false;
	}
	
	public void HighlightTile( string colour_ )
	{
		if ( !isOccupied )	
		{
			float r , g , b;
			r = renderer.material.color.r;
			g = renderer.material.color.g;
			b = renderer.material.color.b;
			
			switch ( colour_ )
			{
				case "red":
					renderer.material.color = new Color( 1.0f , 0.0f , 0.0f , 1.0f );
				break;		
			}
			isHighlighted = true;
		}	
	}
	
	public void UnHighlightTile()
	{
		renderer.material.color = new Color( 1.0f , 1.0f , 1.0f , 1.0f );
		isHighlighted = false;	
	}
		
	
	
	public void OnTap()
	{
		
		if ( isHighlighted )	
		{
						
			//MatchController.PerformMoveAction(MatchController.playerName, 	
			//MatchController.selectedUnit.UnitID, 	myLocation,)
			// Loop through all the highlighted tiles and call UnHighlightTile() on them.

			
		}
		
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
}
