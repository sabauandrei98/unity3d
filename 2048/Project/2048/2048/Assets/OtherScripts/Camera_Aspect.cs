using UnityEngine;
using System.Collections;

public class Camera_Aspect : MonoBehaviour {
	
	void Start () {
		Camera.main.aspect = 800f / 1400f;
	}
}
