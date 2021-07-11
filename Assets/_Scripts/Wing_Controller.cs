using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
namespace Lighthouse
{
    public class Wing_Controller : MonoBehaviour
    {
        public Transform holder;
        Vector3 startPosition;
        Quaternion startRotation;
        IEnumerator coroutine;
        [System.NonSerialized]
        public Vector3 holderPosition;

        private void Start()
		{
            startPosition = holder.transform.localPosition;
            startRotation = holder.transform.localRotation;
            holderPosition = (holder.transform.localPosition - startPosition);
        }
		public void OnCatch(){
            if(coroutine != null) StopCoroutine(coroutine);
            coroutine = HoldRoutine();
            StartCoroutine(coroutine);
        }
        public void OnRelease(){
            StopCoroutine(coroutine);
            coroutine = ReleaseRoutine();
            StartCoroutine(coroutine);
        }
        public IEnumerator HoldRoutine(){
            while(true){
                //Debug.Log("Position = " + (holder.transform.localPosition - startPosition) + " --- Distance = " + Vector3.Distance(holder.transform.localPosition, startPosition));
                holderPosition = (holder.transform.localPosition - startPosition);
                yield return null;
            }
        }
        public IEnumerator ReleaseRoutine(){
            float timer = 0.0f;
            Vector3 originPosition = holder.transform.localPosition;
            Quaternion originRotation = holder.transform.localRotation;
            while (timer <= 1.0f)
            {
                timer += Time.deltaTime * 2.0f;
                holder.transform.localPosition = Vector3.Lerp(originPosition, startPosition, timer);
                holder.transform.localRotation = Quaternion.Lerp(originRotation, startRotation, timer);
                holderPosition = (holder.transform.localPosition - startPosition);
                yield return null;
            }
            holder.transform.localPosition = startPosition;
            holder.transform.localRotation = startRotation;
            holderPosition = (holder.transform.localPosition - startPosition);
        }
    }
}