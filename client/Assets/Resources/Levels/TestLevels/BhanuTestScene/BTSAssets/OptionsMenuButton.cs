using UnityEngine;
using System.Collections;

public class OptionsMenuButton : UIButtonSound {

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
			Destroy((Object)GameObject.Find("Options"));
			Destroy((Object)GameObject.Find("MenuButton"));
			
			Application.LoadLevel("MainMenuScreen");
		}
	}
}
