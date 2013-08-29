using UnityEngine;
using System.Collections;

public class SkipButton : UIButtonSound {

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
			Destroy((Object)GameObject.Find("MainMenuScreen"));
			Application.LoadLevel("GameScreen");
		}
	}
}
