using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour {

	public void MainMenuPlay()
	{
		SceneManager.LoadScene ("game");
	}

	public void MainMenuExit()
	{
		Application.Quit ();
	}

	public void InGameMainMenu()
	{
		SceneManager.LoadScene ("mainMenu");
	}

	public void InGameRestart()
	{
		SceneManager.LoadScene ("game");
	}

	void Update()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			if (SceneManager.GetActiveScene ().name == "game")
				InGameMainMenu ();

			if (SceneManager.GetActiveScene ().name == "mainMenu")
				MainMenuExit ();
		}
	}
}
