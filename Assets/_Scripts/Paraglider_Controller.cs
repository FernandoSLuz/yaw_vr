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

		public float yRotationVelocity = 8;
		public float ZRotationLimit = 40;
		public float forwardVelocity = 3.5f;

		Transform t;
		float YRotation = 0;

		private void Start()
		{
			t = transform;
			StartCoroutine(MoveRoutine());
			
		}
		public IEnumerator MoveRoutine(){
			while(true){
				rightWingPos = rightWingHolder.holderPosition;
				leftWingPos = leftWingHolder.holderPosition;

				//rightWingPos = Vector3.ClampMagnitude(rightWingPos, 8);
				//leftWingPos = Vector3.ClampMagnitude(leftWingPos, 8);


				float wingMovement = -(leftWingPos.magnitude / 10) + rightWingPos.magnitude / 10;

				if (wingMovement >= 0.8) wingMovement = 0.8f;
				else if (wingMovement <= -0.8) wingMovement = -0.8f;
				Debug.Log(wingMovement);

				//If both left and right wings are 0,0,0, it should go front
				//If right wing are 0,y!=0,0 and left wing is 0,0,0 it should go right
				//If left wing are 0,y!=0,0 and right wing is 0,0,0 it should go left

				//wing movement must be smoothDamped

				YRotation += wingMovement * Time.deltaTime * yRotationVelocity;
				t.rotation = Quaternion.Euler(0, YRotation, -wingMovement * ZRotationLimit);
				t.position += (t.forward * Time.deltaTime * forwardVelocity);
				yield return null;
			}
		}
	}
}

