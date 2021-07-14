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
		public float forwardAcceleration = 2f;
		public float forwardDeceleration = 2f;
		public Vector2 forwardVelocityConstraints = Vector2.one;

		Transform t;
		float YRotation = 0;
		public float velocity = 2;
		public float rotVelocity = 2;
		public float smooth = 2;
		public float rotationSmooth = 1;
		public YawController yawController; // reference to YawController
		public AnimationCurve curve;
		public Transform positioner;
		public Transform model;

		private void Start()
		{
			
			t = transform;
			// Assign the instance to the yaw controller, and try to connect
			yawController = YawController.Instance();
			yawController.ConnectToDevice(new YawDevice(IPAddress.Parse("192.168.0.12"), 50020, 50010, "001", "DEBUG", DeviceStatus.Available),
					() => { Debug.Log("Sucesso"); }, (error) => { Debug.Log(error); }
					);
			StartCoroutine(MoveRoutine());
		}

		public IEnumerator MoveRoutine()
		{
			// wait until the connection is done and the instance not null
			yield return new WaitUntil(() => yawController != null);
			yield return new WaitUntil(() => yawController.State == ControllerState.Connected);
			float currentWingPosition = 0;
			float currentRotation = 0;
			WaitForFixedUpdate update = new WaitForFixedUpdate();
			while (true)
			{
				rightWingPos = wingsHolders[1].holderPosition;
				leftWingPos = wingsHolders[0].holderPosition;

				float targetWingMovement = -(leftWingPos.magnitude / 10) + rightWingPos.magnitude / 10;
				
				targetWingMovement = Mathf.Clamp(targetWingMovement, -8, 8);
				currentWingPosition = Mathf.SmoothDamp(currentWingPosition, targetWingMovement, ref velocity, Time.deltaTime, smooth);

				forwardVelocity -= Time.deltaTime * forwardDeceleration;
				forwardVelocity = Mathf.Clamp(forwardVelocity, forwardVelocityConstraints.x, forwardVelocityConstraints.y);
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

		public void ControlSpeed(float change)
        {
			forwardVelocity += change * Time.deltaTime * forwardAcceleration;
		}
	}
}

