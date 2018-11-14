using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour {

	void Start () {
		GameObject.Find ("Scripts").GetComponent<ButtonFunctions> ().LoadAnyScene ("menu");
	}
}
