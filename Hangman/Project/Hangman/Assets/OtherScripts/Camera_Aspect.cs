using UnityEngine;
using System.Collections;

public class Camera_Aspect : MonoBehaviour {

	// WIDTH / HEIGHT = 1.7
	void Start () {
		Camera.main.aspect = 1.7f;
	}
}
