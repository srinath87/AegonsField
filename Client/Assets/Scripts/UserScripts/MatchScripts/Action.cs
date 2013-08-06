using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Actions {MOVE=1, ATTACK=2, POWER=3};

namespace AssemblyCSharp
{
	public class Action
	{		
		Actions actionType;
		int unitId;
		int targetId;
		Vector3 targetLocation;
					
		public Action ()
		{
		
		}
		
		/**
		 * Overload for Move Action
		 */
		public void Init(int type, int unitId, Vector3 targetLocation)
		{
			actionType = (Actions)type;
			this.unitId = unitId;
			this.targetLocation = targetLocation;
		}
		
		/**
		 * Overload for Move Action
		 */
		public void Init(int type, int attackerId, int targetId)
		{
			actionType = (Actions)type;
			Debug.Log(actionType.ToString());
			this.unitId = attackerId;
			this.targetId = targetId;
		}
		
		public Actions GetActionType()
		{
			return actionType;
		}
		
		public void SetActionType(int actionType)
		{
			this.actionType = (Actions)actionType;
		}
		
		public int GetUnitID()
		{
			return unitId;
		}
		
		public void SetUnitId(int unitId)
		{
			this.unitId = unitId;
		}
		
		public int GetTargetId()
		{
			return targetId;
		}
		
		public void SetTargetId(int targetId)
		{
			this.targetId = targetId;
		}
		
		public Vector3 GetTargetLocation()
		{
			return targetLocation;
		}
		
		public void SetTargetLocation(float x, float y, float z)
		{
			targetLocation = new Vector3(x, y, z);
		}
	}
}
