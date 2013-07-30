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
			Destroy((Object)GameObject.Find("OptionsScreen"));
			Application.LoadLevel("MainMenuScreen");
		}
	}
}
