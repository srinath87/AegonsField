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
			//GameObject OptionsPanel = GameObject.Find ("OptionsPanel");
			//NGUITools.SetActive(OptionsPanel , false);
			
			Destroy((Object)GameObject.Find("OptionsScreen"));
			Application.LoadLevel("MainMenuScreen");
		}
	}
}
