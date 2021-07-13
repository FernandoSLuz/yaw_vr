using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lighthouse{
    public class Ring_Manager : MonoBehaviour
    {
        public List<Ring_Instance> rings = new List<Ring_Instance>();
        public Material quedMaterial;
        public Material usedMaterial;
        public Material activeMaterial;


        public static Ring_Manager instance;

		void Awake()
		{
            instance = this;
            foreach (var item in rings){
                item.meshRenderer.material = quedMaterial;
                item.transform.parent.GetComponent<Animator>().enabled = false;
                item.ringState = Ring_Instance.RingState.qeued;
            }
            NextRing();
		}
		public void NextRing(){
            if(rings.Count > 0){
                rings[0].meshRenderer.material = activeMaterial;
                rings[0].transform.parent.GetComponent<Animator>().enabled = true;
                rings[0].ringState = Ring_Instance.RingState.active;
                rings.RemoveAt(0);
            }
        }
        public void RingCaught(Ring_Instance ringInstance){ 
            if(ringInstance.ringState == Ring_Instance.RingState.active){
                ringInstance.meshRenderer.material = usedMaterial;
                ringInstance.ringState = Ring_Instance.RingState.used;
                ringInstance.transform.parent.GetComponent<Animator>().enabled = false;
                ringInstance.StopAllCoroutines();
                GetComponent<AudioSource>().Play();
                NextRing();
            }
        }
	}
}

