using UnityEngine;
using System.Collections;

public class CardController : MonoBehaviour {
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnMouseDown()
	{
		//Debug.Log(StackTraceUtility.ExtractStackTrace().ToString());
		
	}
	
	public void OnMouseDrag()
	{
		Debug.Log("Card Dragged");
	}
	
	public void OnTap( uint touchposition_ )
	{
		Debug.Log( touchposition_ );//+ " " touchposition_.y + " " + touchposition_.z );
	}
	
	
}
