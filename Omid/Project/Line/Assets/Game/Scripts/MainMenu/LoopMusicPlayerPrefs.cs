using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMusicPlayerPrefs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("Music", 1) == 1) {
			PlayMusic ();
		}
	}
	
	public void PlayMusic()
	{
		gameObject.GetComponent<AudioSource> ().Play ();
	}

	public void StopMusic()
	{
		gameObject.GetComponent<AudioSource> ().Stop ();
	}
}
