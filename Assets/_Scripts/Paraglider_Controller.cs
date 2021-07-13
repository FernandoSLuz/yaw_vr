using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using YawVR;
using System.Net;
namespace Lighthouse
{
	public class Paraglider_Controller : MonoBehaviour
	{
		public Wing_Controller[] wingsHolders;
		public Vector3 leftWingPos;
		public Vector3 rightWingPos;

		public float yRotationVelocity = 8;
		public float ZRotationLimit = 40;
		public float forwardVelocity = 3.5f;

		Transform t;
		float YRotation = 0;
		public float velocity = 2;
		public float rotVelocity = 2;
		public float smooth = 2;
		public float rotationSmooth = 1;
		public YawController yawController; // reference to YawController
		public AnimationCurve curve;

		private IEnumerator Start()
		{
			t = transform;
			StartCoroutine(MoveRoutine());
			yield return new WaitForSeconds(1.0f);
			yawController = YawController.Instance();
			yawController.ConnectToDevice(new YawDevice(IPAddress.Parse("192.168.0.12"), 50020, 50010, "001", "DEBUG", DeviceStatus.Available),
					() => { Debug.Log("Sucesso"); }, (error) => { Debug.Log(error); }
					);
		}

		public IEnumerator MoveRoutine()
		{
			yield return new WaitForSeconds(5.0f);
			float currentWingPosition = 0;
			float currentRotation = 0;
			WaitForFixedUpdate update = new WaitForFixedUpdate();
			while (true)
			{
				rightWingPos = wingsHolders[1].holderPosition;
				leftWingPos = wingsHolders[0].holderPosition;

				//rightWingPos = Vector3.ClampMagnitude(rightWingPos, 8);
				//leftWingPos = Vector3.ClampMagnitude(leftWingPos, 8);

				float targetWingMovement = -(leftWingPos.magnitude / 10) + rightWingPos.magnitude / 10;

				targetWingMovement = Mathf.Clamp(targetWingMovement, -8, 8);
				currentWingPosition = Mathf.SmoothDamp(currentWingPosition, targetWingMovement, ref velocity, Time.deltaTime, smooth);
				//If both left and right wings are 0,0,0, it should go front
				//If right wing are 0,y!=0,0 and left wing is 0,0,0 it should go right
				//If left wing are 0,y!=0,0 and right wing is 0,0,0 it should go left

				//wing movement must be smoothDamped
				float targetRotation = -currentWingPosition * ZRotationLimit;
				currentRotation = Mathf.SmoothDamp(currentRotation, -currentWingPosition * ZRotationLimit, ref rotVelocity,
					curve.Evaluate(Mathf.Abs(currentWingPosition / 8) * rotationSmooth));

				YRotation += currentWingPosition * Time.deltaTime * yRotationVelocity;
				t.rotation = Quaternion.Euler(0, YRotation, currentRotation);
				t.position += (t.forward * Time.deltaTime * forwardVelocity);
				yawController.TrackerObject.SetRotation(t.localEulerAngles);
				yield return update;
			}
		}
	}
}

