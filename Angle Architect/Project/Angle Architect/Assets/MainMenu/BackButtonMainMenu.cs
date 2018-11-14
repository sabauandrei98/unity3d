using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonMainMenu : MonoBehaviour {

	public GameObject helpTab;

	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) 
		{ 
			if (helpTab.activeSelf == false)
				Application.Quit ();
			else
				helpTab.SetActive (false);
		}
	}
}
