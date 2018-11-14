using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMusicIcon : MonoBehaviour {

	public Sprite musicOn, musicOff;
	void Start () {
		if (PlayerPrefs.GetInt ("musicVol", 1) == 1)
			gameObject.GetComponent<Image> ().sprite = musicOn;
		else
			gameObject.GetComponent<Image> ().sprite = musicOff;
	}

}
