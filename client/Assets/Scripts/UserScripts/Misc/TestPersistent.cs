using UnityEngine;
using System.Collections;

public class TestPersistent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.Find("GameController");
		Debug.Log(obj);
		obj.SendMessage("TestFunction");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
