using UnityEngine;
using System.Collections;

public class TrainingMenuButton : UIButtonSound {

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
			Destroy((Object)GameObject.Find("Training"));
			Destroy((Object)GameObject.Find("MenuButton"));
			
			Application.LoadLevel("MainMenuScreen");
		}
	}
}
