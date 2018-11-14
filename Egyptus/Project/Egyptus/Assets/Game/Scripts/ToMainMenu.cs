using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SceneManager.LoadScene ("mainMenu");	
	}
}
