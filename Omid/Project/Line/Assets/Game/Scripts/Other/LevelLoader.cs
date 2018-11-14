using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

	public string sceneName;
	public Slider loadingBar;


	void Start()
	{
		StartCoroutine (WaitTime (0.1f));
	}

	IEnumerator WaitTime(float time)
	{
		yield return new WaitForSeconds (time);
		StartCoroutine (LoadAsync ());
	}

	IEnumerator LoadAsync()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);
		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / .9f);
			loadingBar.value = progress;
			yield return null;
		}

	}
}
