using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSession : MonoBehaviour {

	void Start () {
		if (PlayerPrefs.GetInt ("Sessions", 0) == 0) {
			PlayerPrefs.SetInt ("Sessions", 1);
			PlayerPrefs.SetInt ("Default", 2);
			PlayerPrefs.SetInt ("Points", 200); 
			PlayerPrefs.Save ();
		}
	}
}
