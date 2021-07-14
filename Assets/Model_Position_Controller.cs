using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Position_Controller : MonoBehaviour
{
	public Transform model;
	public Transform positioner;
	public Transform pivot;

	public Transform IMU;

	private void Start()
	{
		StartCoroutine(MovementRoutine());
	}

	public IEnumerator MovementRoutine(){
		WaitForFixedUpdate update = new WaitForFixedUpdate();
		while(true){
			pivot.rotation = IMU.rotation;
			model.position = positioner.position;
			yield return update;
		}
	}
}
