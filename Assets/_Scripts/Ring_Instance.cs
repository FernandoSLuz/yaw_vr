using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lighthouse{
    public class Ring_Instance : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        public RingState ringState;
		private void Start()
		{
            //StartCoroutine(LoopLookAt());
		}
		private void OnTriggerEnter(Collider other)
		{
			if(other.tag == "Player"){
                Ring_Manager.instance.RingCaught(this);
            }
		}
        /*
        public IEnumerator LoopLookAt(){
            /*var target = GameObject.FindGameObjectWithTag("Player").transform;
            while(true){

                Vector3 targetPostition = new Vector3(target.position.x,
                                        this.transform.parent.position.y,
                                        target.position.z);
                this.transform.parent.LookAt(targetPostition);

                yield return null;
            }
            yield retun null;
        }
        */
		[System.Serializable]
        public enum RingState{ 
            qeued,
            active,
            used
        }
    }
}

