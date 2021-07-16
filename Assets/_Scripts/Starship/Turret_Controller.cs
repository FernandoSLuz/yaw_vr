using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lighthouse{
    public class Turret_Controller : MonoBehaviour
    {
		public static Turret_Controller instance;
		Vector2 targetRotation;
		Transform t;

		public void Awake()
		{
			instance = this;
			t = transform;
		}

		public void GetJoystickValues(Vector2 joystickInput){
			Debug.Log(joystickInput);
			//transform.rotation = Quaternion.Euler();
		}

		IEnumerator RotateTurret()
		{
			WaitForFixedUpdate update = new WaitForFixedUpdate();
			float rotationY = 0;
			float rotationZ = 0;
			float velocityY = 0;
			float velocityZ = 0;
			while (true){
				rotationY = Mathf.SmoothDamp(rotationY, targetRotation.x, ref velocityY, Time.deltaTime);
				//rotationX = Mathf.SmoothDamp(rotationX, targetRotation.x, ref velocityY, Time.deltaTime);
				t.rotation = Quaternion.Euler(t.rotation.x, rotationY, rotationZ);
				yield return update;
			}
		}
	}
}

