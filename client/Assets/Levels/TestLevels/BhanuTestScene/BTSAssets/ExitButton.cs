using UnityEngine;
using System.Collections;

public class ExitButton : UIButtonSound {

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
			Application.Quit();
		}
	}
}
