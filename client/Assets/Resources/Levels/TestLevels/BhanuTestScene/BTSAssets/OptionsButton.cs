using UnityEngine;
using System.Collections;

public class OptionsButton : UIButtonSound {

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
			//NGUITools.SetActive(OptionsPanel , true);	
			
			Destroy((Object)GameObject.Find("MainMenuScreen"));
			Application.LoadLevel("OptionsScreen");
		}
	}
}
