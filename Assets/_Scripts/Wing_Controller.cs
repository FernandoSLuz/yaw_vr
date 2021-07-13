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

        float timer = 0.0f;
        Vector3 originPosition;
        Quaternion originRotation;

        private void Start()
		{
            startPosition = holder.localPosition;
            startRotation = holder.localRotation;
            holderPosition = (holder.localPosition - startPosition);
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
                holderPosition = (holder.localPosition - startPosition);
                yield return null;
            }
        }
        public IEnumerator ReleaseRoutine(){
            timer = 0.0f;
            originPosition = holder.localPosition;
            originRotation = holder.localRotation;
            while (timer <= 1.0f)
            {
                timer += Time.deltaTime * 2.0f;
                holder.localPosition = Vector3.Lerp(originPosition, startPosition, timer);
                holder.localRotation = Quaternion.Lerp(originRotation, startRotation, timer);
                holderPosition = (holder.transform.localPosition - startPosition);
                yield return null;
            }
            holder.localPosition = startPosition;
            holder.localRotation = startRotation;
            holderPosition = (holder.transform.localPosition - startPosition);
        }
    }
}