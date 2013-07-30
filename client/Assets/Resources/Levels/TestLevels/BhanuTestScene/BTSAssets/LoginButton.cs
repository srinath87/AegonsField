﻿using UnityEngine;
using System.Collections;

public class LoginButton : UIButtonSound {

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
			Destroy((Object)GameObject.Find("LogInScreen"));
			Application.LoadLevel("LoadingScreen");
		}
	}
}