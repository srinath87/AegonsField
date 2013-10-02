using UnityEngine;
using System.Collections;

public class ChallengesButton : UIButtonSound {

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
			Application.LoadLevel("ChallengesScreen");
		}
	}
}
