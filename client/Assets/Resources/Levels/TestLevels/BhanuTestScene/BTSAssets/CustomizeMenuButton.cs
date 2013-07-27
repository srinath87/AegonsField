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
			Destroy((Object)GameObject.Find("Customize"));
			Destroy((Object)GameObject.Find("MenuButton"));
			
			Application.LoadLevel("MainMenuScreen");
			
		}
	}
}
