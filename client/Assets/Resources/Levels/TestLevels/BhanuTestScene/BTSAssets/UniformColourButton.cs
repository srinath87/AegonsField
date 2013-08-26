using UnityEngine;
using System.Collections;

public class UniformColourButton : UIButtonSound {

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
			Destroy((Object)GameObject.Find("TrainingScreen"));
			Application.LoadLevel("UniformColourScreen");
		}
	}
}
