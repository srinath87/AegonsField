using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private static GameManager instance;
	
	private List<GameObject> matchList = new List<GameObject>();
		
	public GameObject match;
	
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
	
	[RPC]
	public void CreateMatch(int mId, string playerName, string opponentName, bool facingRight)
	{
		GameObject newMatch = (GameObject)GameObject.Instantiate((Object)match);
		newMatch.GetComponent<Match>().Init(mId, playerName, opponentName, facingRight);
		matchList.Add(newMatch);
	}
}
