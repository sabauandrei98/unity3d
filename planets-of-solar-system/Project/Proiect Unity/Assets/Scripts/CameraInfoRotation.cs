using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInfoRotation : MonoBehaviour {

	public float radius = 6f;
	public float angle = 0f;
	public float coef = 0f;

	void Update () {

		angle += coef;
		angle %= 360;

		float x = radius * Mathf.Cos(angle * Mathf.PI/180);
		float z = radius * Mathf.Sin(angle * Mathf.PI/180);

		transform.position = new Vector3(x, 0, z);
	}
}
