using UnityEngine;
using System.Collections;

public class EditDeckButton : UIButtonSound {

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
			Application.LoadLevel("EditDeckScreen");
		}
	}
}
