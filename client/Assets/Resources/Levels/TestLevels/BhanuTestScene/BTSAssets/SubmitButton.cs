using UnityEngine;
using System.Collections;

public class SubmitButton : UIButtonSound {

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
			Destroy((Object)GameObject.Find("LogIn"));
			Application.LoadLevel("LoadingScreen");
		}
	}
}
