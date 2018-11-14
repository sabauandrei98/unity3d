using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

	//Line script
	Line lineScript;

	//Difference between where the camera points and where the line is
	float cameraOffsetRotation = 7.5f;


	void Start () {
		lineScript = GameObject.FindWithTag ("line").GetComponent<Line> ();
	}

	void UpdateCamera()
	{
		float cameraAngle = cameraOffsetRotation + lineScript.lineAngle;
		cameraAngle %= 360;

		float x = lineScript.circleRadius * Mathf.Cos(cameraAngle * Mathf.PI/180);
		float z = lineScript.circleRadius * Mathf.Sin(cameraAngle * Mathf.PI/180);

		gameObject.transform.LookAt (new Vector3(z, 0, x));
	}
	

	void Update () {
		UpdateCamera ();
	}
}
