#pragma strict


public var rayHitPoint : Vector3;


function Start () {

}

function Update () {
	RaycastToTerrain();
}

function RaycastToTerrain()
{
	var hit : RaycastHit;
	var rayPos : Ray = Camera.main.ScreenPointToRay( Input.mousePosition );
	 
	if ( Physics.Raycast( rayPos, hit, Mathf.Infinity ) ) // also consider a layermask to just the terrain layer
	{
		Debug.DrawLine( Camera.main.transform.position, hit.point, Color.red );
		rayHitPoint = hit.point;
		//Debug.Log( hit.point );
	}
}