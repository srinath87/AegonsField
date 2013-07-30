using UnityEngine;
using System.Collections;

public class MainMenuScreen : MonoBehaviour {

	
	void Start () 
	{
		GameObject OptionsPanel = GameObject.Find ("OptionsPanel");
		NGUITools.SetActive(OptionsPanel , false);
	}
	
	
	void Update () 
	{
	
	}
	
	
}
