using UnityEngine;
using System.Collections;

public class TestScriptB : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void TestMethod(int a, float b, string c)
	{
		Debug.Log(a);
		Debug.Log(b);
		Debug.Log(c);
	}
}
