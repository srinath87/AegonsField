using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AssemblyCSharp
{
	public class Action
	{
		enum Actions {MOVE=1, ATTACK=2, POWER=3};
		
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
			this.unitId = attackerId;
			this.targetId = targetId;
		}
	}
}
