using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDespreRotation : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(0, (transform.eulerAngles.y + 0.05f % 360), 0);
	}
}
