using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		setTransparency( 0.0f );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// i want to use this function, to keep things nice and separate,
	//but i have no idea how to call the function externally, as it is not a function in the original inherited class..
	public void setTransparency( float alpha_ ) {
		

		
		float r , g , b;
		r = renderer.material.color.r;
		g = renderer.material.color.g;
		b = renderer.material.color.b;

		renderer.material.color = new Color( r , g , b , alpha_ );

	}
	
}
