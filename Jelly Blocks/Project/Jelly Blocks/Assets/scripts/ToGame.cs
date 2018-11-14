using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find ("ButtonFunctions").GetComponent<ButtonFunctions> ().LevelLoader ("game");
	}
	

}
