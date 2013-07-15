using UnityEngine;
using System.Collections;

public class InputInterface : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Tapped()
	{
		SendMessage("OnTap");
	}
}
