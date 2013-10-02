using UnityEngine;
using System.Collections;

public class AvatarsMenuButton : UIButtonSound {

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
			Application.LoadLevel("MainMenuScreen");
			
		}
	}
}
