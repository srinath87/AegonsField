using UnityEngine;
using System.Collections;

public class HandleClick : MonoBehaviour {
	
	// just to store if mouse is held down or not..
	private bool onPress;
	
	public GameObject prefab;
	Transform mTrans;
	bool mIsDragging = false;
	Transform mParent;
	
	bool selectedTile;
	float xOld;
	float yOld;
	
	/// <summary>
	/// Cache the transform.
	/// </summary>

	void Awake () { mTrans = transform; }

	/// <summary>
	/// Start the drag event and perform the dragging.
	/// </summary>

	
	void Start () {
		onPress = false;
		selectedTile = false;
		xOld = transform.position.x;
		yOld = transform.position.y;
	}
	//.

	void Update () {
		
		//if the mouse/touch has not been released yet, we simulate a 'moved' event
		//if ( true == onPress ){
			
			
			
		//}
		
		
		if ( false == onPress ){
			
			if ( xOld != transform.position.x && yOld != transform.position.y ){
			
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(xOld, yOld, transform.position.z ), Time.deltaTime * 16.0f );
				
			}
			/*
			if ( true == moved ){
				
			}
			*/
		}

		
		
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
				pos.z = 0.0f;
				mTrans.localPosition = pos;
				
				//mTrans.BroadcastMessage("CheckParent", SendMessageOptions.DontRequireReceiver); //what's this used for?
			}
			else
			{
				mTrans.localPosition += ( Vector3 ) delta;// * 2.15f;
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
