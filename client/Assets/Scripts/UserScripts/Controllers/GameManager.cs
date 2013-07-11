using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private static GameManager instance;
	
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject ("GameManager").AddComponent<GameManager> ();
			}
 
			return instance;
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(gameObject);

		StartCoroutine("LeaveScene");
	}
	
	IEnumerator LeaveScene()
	{
		yield return new WaitForSeconds(5);

		SendMessage("LoadScene", "ServerConnect");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
 
	public void OnApplicationQuit ()
	{
		instance = null;
	}
	
	public void TestFunction()
	{
		Debug.Log("TestPassed");
	}
}
