using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectIcon : MonoBehaviour {

	public Sprite effectOn, effectOff;
	void Start () {
		if (PlayerPrefs.GetInt ("effectVol", 1) == 1)
			gameObject.GetComponent<Image> ().sprite = effectOn;
		else
			gameObject.GetComponent<Image> ().sprite = effectOff;
	}
}
