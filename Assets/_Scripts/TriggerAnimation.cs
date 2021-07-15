using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public Transform trigger;
    public Quaternion startRot;
    public Quaternion endRotation;
    public Vector3 endPos;

    [Range(0, 1)]
    public float input;

    private void Start()
    {
        startRot = Quaternion.Euler(Vector3.zero);
        StartCoroutine(AnimateTrigger());
    }

    IEnumerator AnimateTrigger()
    {
        Quaternion endRotation = Quaternion.Euler(endPos);
        while (true)
        {
            trigger.localRotation = Quaternion.Lerp(startRot, endRotation, input);
            yield return null;
        }
    }
}