using UnityEngine;
using System.Collections;

public class CustomizeMenuButton : UIButtonSound {

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
			Application.LoadLevel("MainMenuScreen");
			
		}
	}
}
