﻿using UnityEngine;
using System.Collections;

public class HandleClick : MonoBehaviour {
	
	// just to store if mouse is held down or not..
	private bool onPress;
	
	public GameObject prefab;
	Transform mTrans;
	bool mIsDragging = false;
	Transform mParent;
	
	
	
	/// <summary>
	/// Cache the transform.
	/// </summary>

	void Awake () { mTrans = transform; }

	/// <summary>
	/// Start the drag event and perform the dragging.
	/// </summary>

	
	void Start () {
		onPress = false;
	}
	//.

	void Update () {
		
		//if the mouse/touch has not been released yet, we simulate a 'moved' event
		//if ( true == onPress ){
			
			
			
		//}
	}
	//.
	
	
	
	
	
	
	//LATER
	//void UpdateTable ()
	//{
		//UITable table = NGUITools.FindInParents<UITable>(gameObject);
		//if (table != null) table.repositionNow = true;
	//}
	
	

	
	//step 1
	void OnPress (bool isPressed)
	{
		if ( isPressed ){
			Debug.Log( "Began" );
			onPress = true;
			
			mIsDragging = false;
			//LATER FOR DROPPING
			//Collider col = collider;
			//if (col != null) col.enabled = !isPressed;
			//if (!isPressed) Drop();
			
		}
		
	}
	//.
	
	//step 2
	void OnDrag (Vector2 delta)
	{
		
		
		if (UICamera.currentTouchID > -2)
		{
			
			Debug.Log( "HHH" );
			if (!mIsDragging)
			{
				mIsDragging = true;
				mParent = mTrans.parent;
				//mTrans.parent = DragDropRoot.root; //what's this used for?
				
				Vector3 pos = mTrans.localPosition;
				pos.z = 0f;
				mTrans.localPosition = pos;
				
				//mTrans.BroadcastMessage("CheckParent", SendMessageOptions.DontRequireReceiver); //what's this used for?
			}
			else
			{
				mTrans.localPosition += ( Vector3 ) delta;
			}
		}
		
	}

	

	
	//step 3
	void OnClick(){
		Debug.Log( "Ended");
		onPress = false;
	}
	//.
	
	
	//step 0
	//windows only i assume? no such thing as hover for mobile
	//void OnHover (bool isOver){}
	
	
	

	
}