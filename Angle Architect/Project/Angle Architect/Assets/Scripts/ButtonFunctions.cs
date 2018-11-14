﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour {


	public void RestartGame()
	{
		SceneManager.LoadScene (1);
	}

	public void StartGame()
	{
		SceneManager.LoadScene (1);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene (0);
	}

	public void Exit()
	{
		Application.Quit ();
	}
	

}
