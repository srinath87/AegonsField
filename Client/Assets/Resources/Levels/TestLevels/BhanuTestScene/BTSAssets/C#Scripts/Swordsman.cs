using UnityEngine;
using System.Collections;

public class Swordsman : MonoBehaviour {
	
	private bool isHeld;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
			
			if(Input.anyKeyDown){
				//Debug.Log("You have Pressed Something");
				animation.Play("Attack");
			}
		
		}
}
