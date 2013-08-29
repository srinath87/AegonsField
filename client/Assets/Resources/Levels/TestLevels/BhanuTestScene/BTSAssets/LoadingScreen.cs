using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
	
	public string SceneToLoad = "MainMenuScreen";
	
	void Start () 
	{
		StartCoroutine("LoadScene" , SceneToLoad);
	}
	
	
	void Update () 
	{
		
	}
	
	IEnumerator LoadScene(string SceneToLoad)
	{
	  
		yield return new WaitForSeconds(1);
		
		if(!SceneToLoad.Equals("None"))
		{
			Application.LoadLevel(SceneToLoad);
			Destroy((Object)GameObject.Find("LoadingScreen"));
		}
		
	}
	
}
