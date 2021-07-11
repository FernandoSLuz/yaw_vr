using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
namespace Lighthouse{
	public class Paraglider_Controller : MonoBehaviour
	{
		public Wing_Controller left_wing_controller;
		public Wing_Controller right_wing_controller;

		private void Start()
		{
			StartCoroutine(MoveRoutine());
			
		}
		public IEnumerator MoveRoutine(){
			while(true){
				transform.position += Vector3.forward * Time.deltaTime;
				yield return null;
			}
		}
	}
}

