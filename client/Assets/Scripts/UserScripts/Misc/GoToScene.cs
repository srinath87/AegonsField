using UnityEngine;
using System.Collections;

public class GoToScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void LoadScene(string sceneToLoad)
	{
		Application.LoadLevel(sceneToLoad);
	}
}
