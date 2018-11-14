using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroyOnLoad : MonoBehaviour {

	public AudioClip mainMenuLoop, inGameLoop;
	float musicVolume = 0.25f;

	void Awake()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		DontDestroyOnLoad (gameObject);
	}

	public void ApplySettings(int type)
	{
		gameObject.GetComponent<AudioSource> ().volume = type * musicVolume;
	}

	void OnLevelWasLoaded(){
		
		string sceneName = SceneManager.GetActiveScene ().name;

		gameObject.GetComponent<AudioSource> ().volume = PlayerPrefs.GetInt ("musicVol", 1) * musicVolume;

		if (sceneName [0] == 'i' && sceneName [1] == 'n') {
			gameObject.GetComponent<AudioSource> ().Stop ();
		}
		else
		if (sceneName.Length <= 3) {
			if (gameObject.GetComponent<AudioSource> ().clip != inGameLoop) {
				gameObject.GetComponent<AudioSource> ().clip = inGameLoop;
				gameObject.GetComponent<AudioSource> ().Play ();
			}
		} else {
			if (gameObject.GetComponent<AudioSource> ().clip != mainMenuLoop) {
				gameObject.GetComponent<AudioSource> ().clip = mainMenuLoop;
				gameObject.GetComponent<AudioSource> ().Play ();
			}
		}
	}
}
