using UnityEngine;
using System.Collections;

public class TestScriptA : MonoBehaviour {
	
	public TestScriptB other;
	public int a;
	// Use this for initialization
	void Start () 
	{
		other.TestMethod(a, 50.0f, "TestString");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
}
