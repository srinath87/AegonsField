using UnityEngine;
using System.Collections;

public class ChallengesMenuButton : UIButtonSound {

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
			Destroy((Object)GameObject.Find("Challenges"));
			Destroy((Object)GameObject.Find("MenuButton"));
			
			Application.LoadLevel("MainMenuScreen");
			
		}
	}
}
