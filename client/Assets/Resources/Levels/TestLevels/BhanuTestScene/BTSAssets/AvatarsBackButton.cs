using UnityEngine;
using System.Collections;

public class AvatarsBackButton : UIButtonSound {

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
			Destroy((Object)GameObject.Find("AvatarsScreen"));
			Application.LoadLevel("CustomizeScreen");
			
		}
	}
}
