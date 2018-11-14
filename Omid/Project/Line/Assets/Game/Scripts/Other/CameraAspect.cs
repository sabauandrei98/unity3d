using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspect : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Camera.main.aspect = 0.62f;
	}
}
