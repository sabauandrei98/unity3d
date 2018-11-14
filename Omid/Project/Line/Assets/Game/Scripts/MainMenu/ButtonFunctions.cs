using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour {

	public void ButtonMusicPress()
	{
		if (PlayerPrefs.GetInt ("Music", 1) == 1) {
			PlayerPrefs.SetInt ("Music", 0);
			GameObject.Find ("LoopMusic").GetComponent<LoopMusicPlayerPrefs> ().StopMusic ();
		} else {
			PlayerPrefs.SetInt ("Music", 1);
			GameObject.Find ("LoopMusic").GetComponent<LoopMusicPlayerPrefs> ().PlayMusic ();
		}
		PlayerPrefs.Save ();
		GameObject.Find ("IconsMainMenu").GetComponent<IconsMainMenu> ().MusicIcon ();

	}

	public IEnumerator loadMainMenu(float time)
	{
		yield return new WaitForSeconds (time);

		SceneManager.LoadScene ("menu");
	}

	public void ButtonEffectsPress()
	{
		if (PlayerPrefs.GetInt ("Effects", 1) == 1) {
			PlayerPrefs.SetInt ("Effects", 0);
		}
		else
			PlayerPrefs.SetInt ("Effects", 1);

		PlayerPrefs.Save ();
		GameObject.Find ("IconsMainMenu").GetComponent<IconsMainMenu> ().EffectsIcon ();
	}

	public void ShowRewarded()
	{
		GameObject.Find ("UnityAdsManager").GetComponent<GameAdsManager> ().ShowRewarded ();
	}


	public void Quit()
	{
		Application.Quit ();
	}

	public void MainMenu()
	{
		SceneManager.LoadScene ("menu");
	}

	public void Play()
	{
		SceneManager.LoadScene ("game");
	}

	public void Customize()
	{
		SceneManager.LoadScene ("customize");
	}

	IEnumerator LoadLevel(string sceneName)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);
		while (!operation.isDone) {
			yield return null;
		}
	}

	public void LoadAnyScene(string sceneName)
	{
		StartCoroutine (LoadLevel (sceneName));
	}


	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (SceneManager.GetActiveScene ().name == "menu") {
				try {
					if (GameObject.Find ("HowToPlayPanel").activeInHierarchy)
						GameObject.Find ("HowToPlayPanel").SetActive (false);
				} catch {
					Quit ();
				}
			} else
				MainMenu ();
			
		}
	}

}
