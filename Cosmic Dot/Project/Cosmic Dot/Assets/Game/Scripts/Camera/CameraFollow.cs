using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothTime = 200F;
	private Vector3 velocity = Vector3.zero;
	private Vector3 offset = new Vector3 (5, 16, 0);

	void Update() {
		Vector3 desiredPosition = target.transform.position + offset;
		transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
	}
}
