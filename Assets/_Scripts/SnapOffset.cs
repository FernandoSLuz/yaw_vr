using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapOffset : MonoBehaviour
{
    Transform t;
    public Transform reference;
    public Vector3 offset;

	private void Start()
	{
        t = transform;
	}

	void Update()
    {
        t.position = reference.position +
            reference.forward * offset.z +
            reference.up * offset.y +
            reference.right * offset.x;
    }
}
