using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBoxBehave : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int enabled = PlayerPrefs.GetInt ("aLevel" + gameObject.name, 0);

		if (enabled == 0)
			gameObject.GetComponent<Button> ().interactable = false;
	}

}
