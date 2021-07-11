using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
public class Oculus_Physical_Effects : MonoBehaviour
{

	public IEnumerator leftHandVibrationRoutine;
	public IEnumerator rightHandVibrationRoutine;

	public void onHandOn(string hand)
	{
		switch(hand){
			case "left":
				leftHandVibrationRoutine = VibrationRoutine(hand);
				StartCoroutine(leftHandVibrationRoutine);
				break;
			case "right":
				rightHandVibrationRoutine = VibrationRoutine(hand);
				StartCoroutine(rightHandVibrationRoutine);
				break;
		}
	}
	public void onHandOff(string hand)
	{
		switch (hand)
		{
			case "left":
				StopCoroutine(leftHandVibrationRoutine);
				break;
			case "right":
				StopCoroutine(rightHandVibrationRoutine);
				break;
		}
	}
	public IEnumerator VibrationRoutine(string hand){
		while (true){
			float timer = 0.0f;
			if (hand == "left") InputBridge.Instance.VibrateController(0.3f, 0.5f, 0.5f, ControllerHand.Left);
			else InputBridge.Instance.VibrateController(0.3f, 0.5f, 0.5f, ControllerHand.Right);
			while (timer <= 0.4f){
				timer += Time.deltaTime * 1.0f;
				yield return null;
			}
			yield return null;
		}
	}

}
