using UnityEngine;
using System.Collections;

public class AvatarsButton : UIButtonSound {

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
	
	void OnClick ()
	{
		if (enabled && trigger == Trigger.OnClick)
		{
			Destroy((Object)GameObject.Find("CustomizeScreen"));
			Application.LoadLevel("AvatarsScreen");
			
		}
	}
}
