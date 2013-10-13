using UnityEngine;
using System.Collections;

[AddComponentMenu("NGUI/Examples/Drag & Drop Item")]
public class CardController : MonoBehaviour {
	
	
	/// <summary>
	/// Prefab object that will be instantiated on the DragDropSurface if it receives the OnDrop event.
	/// </summary>

	public GameObject prefab;

	Transform mTrans;
	bool mIsDragging = false;
	Transform mParent;

	/// <summary>
	/// Update the table, if there is one.
	/// </summary>

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	


	void Drop ()
	{
		// Is there a droppable container?
		Collider col = UICamera.lastHit.collider;
		DragDropContainer container = (col != null) ? col.gameObject.GetComponent<DragDropContainer>() : null;
		Debug.Log( container );
		if (container != null)
		{
			// Container found -- parent this object to the container
			//mTrans.parent = container.transform;

			//Vector3 pos = mTrans.localPosition;
			//pos.z = 0f;
			//mTrans.localPosition = pos;
		}
		else
		{
			// No valid container under the mouse -- revert the item's parent
			//mTrans.parent = mParent;
		}

		// Notify the table of this change
		//UpdateTable();

		// Make all widgets update their parents
		//BroadcastMessage("CheckParent", SendMessageOptions.DontRequireReceiver);
	}

	/// <summary>
	/// Cache the transform.
	/// </summary>

	void Awake () { mTrans = transform; }

	/// <summary>
	/// Start the drag event and perform the dragging.
	/// </summary>

	void OnDrag (Vector2 delta)
	{
	}

	/// <summary>
	/// Start or stop the drag operation.
	/// </summary>

	void OnPress (bool isPressed)
	{
		mIsDragging = false;
		Collider col = collider;
		if (col != null) col.enabled = !isPressed;
		if (!isPressed) Drop();
	}

	
	
	
	
	
	
}
