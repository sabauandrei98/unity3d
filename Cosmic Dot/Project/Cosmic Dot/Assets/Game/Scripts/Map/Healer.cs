using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healer : MonoBehaviour {

	bool inProgress = false;
	float timeBetweenHeal = 0.05f;
	float healerVolume = 0.5f;
	GameObject healthBar;
	AudioSource gameAudioEffects;
	public AudioClip healEffect;


	void Start()
	{
		healthBar = GameObject.Find ("HealthBar");
		gameAudioEffects = GetComponent<AudioSource> ();
	}

	IEnumerator Heal(float time)
	{
		yield return new WaitForSeconds (time);
		inProgress = false;
		if(healthBar.GetComponent<HealthBar> ().health > 0)
			healthBar.GetComponent<HealthBar> ().HealthBarChangeValue (0.5f);
	}

	void OnTriggerStay (Collider col)
	{
		if(col.gameObject.tag == "Player" && !inProgress)
		{
			inProgress = true;
			if (!gameAudioEffects.isPlaying) {
				gameAudioEffects.PlayOneShot (healEffect, healerVolume * PlayerPrefs.GetInt ("effectVol", 1));
			}
			StartCoroutine("Heal", timeBetweenHeal);
		}
	}
}
