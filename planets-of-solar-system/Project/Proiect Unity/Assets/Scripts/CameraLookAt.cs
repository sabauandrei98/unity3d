using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.LookAt(Vector3.zero);
	}
}
