using UnityEngine;
using System.Collections;

public class ChallengesBackButton : UIButtonSound {

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
			Destroy((Object)GameObject.Find("ChallengesScreen"));
			Application.LoadLevel("MainMenuScreen");
			
		}
	}
}
