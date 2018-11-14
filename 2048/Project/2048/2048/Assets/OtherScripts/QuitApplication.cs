using UnityEngine;
using System.Collections;

public class QuitApplication : MonoBehaviour {
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
	}
}
