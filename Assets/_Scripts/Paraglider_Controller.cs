using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
namespace Lighthouse{
	public class Paraglider_Controller : MonoBehaviour
	{
		public Wing_Controller leftWingHolder;
		public Wing_Controller rightWingHolder;
		public Vector3 leftWingPos;
		public Vector3 rightWingPos;

		private void Start()
		{
			StartCoroutine(MoveRoutine());
			
		}
		public IEnumerator MoveRoutine(){
			while(true){
				rightWingPos = rightWingHolder.holderPosition;
				leftWingPos = leftWingHolder.holderPosition;

				//If both left and right wings are 0,0,0, it should go front
				//If right wing are 0,y!=0,0 and left wing is 0,0,0 it should go right
				//If left wing are 0,y!=0,0 and right wing is 0,0,0 it should go left

				Debug.Log(leftWingPos - rightWingPos);

				yield return null;
			}
		}
	}
}

