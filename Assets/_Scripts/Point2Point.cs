using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point2Point : MonoBehaviour
{
	public Transform pointA;
	public Transform pointB;
	public LineRenderer lineRenderer;
	// Start is called before the first frame update
	private void Start()
	{
		StartCoroutine(changeLineRendererPos());
	}
	public IEnumerator changeLineRendererPos(){
        while(true){
			var positions = new Vector3[2];
			positions[0] = pointA.position;
			positions[1] = pointB.position;

			lineRenderer.SetPositions(positions);

			yield return null;
        }
    }
}
