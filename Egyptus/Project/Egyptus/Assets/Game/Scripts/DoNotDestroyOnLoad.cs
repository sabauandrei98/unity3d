using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroyOnLoad : MonoBehaviour {

	public AudioClip inGameLoop;
	float musicVolume = 0.25f;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}

	void OnLevelWasLoaded(){
		
		string sceneName = SceneManager.GetActiveScene ().name;
		gameObject.GetComponent<AudioSource> ().volume = musicVolume;

		if (gameObject.GetComponent<AudioSource> ().clip != inGameLoop) {
			gameObject.GetComponent<AudioSource> ().clip = inGameLoop;
			gameObject.GetComponent<AudioSource> ().Play ();
		}
		
	}
}
