using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour {

	public void Restart()
	{
		int gamesPlayed = PlayerPrefs.GetInt("GAMES", 0) + 1;
		PlayerPrefs.SetInt("GAMES", gamesPlayed);
		PlayerPrefs.Save();

		if (gamesPlayed % 4 == 0)
		{
			GameObject.FindWithTag ("Admanager").GetComponent<AdManager> ().ShowVideo ();
		}

		SceneManager.LoadScene(1);
	}

	public void HowToPlay()
	{
		SceneManager.LoadScene (0);
	}

	public void PlayGame()
	{
		SceneManager.LoadScene (1);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
