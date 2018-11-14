using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	int soundEffects = 1;
	AudioSource audioSource;

	public AudioClip scoreSound, gameOverSound, bubbleSound;

	void Start()
	{
		soundEffects = PlayerPrefs.GetInt ("Effects", 1);
		audioSource = GameObject.Find ("Audio").GetComponent<AudioSource> ();
	}

	public void PlayScoreSound()
	{
		if (soundEffects == 0)
			return;

		audioSource.PlayOneShot (scoreSound, 0.40f);
	}

	public void PlayGameOverSound()
	{
		if (soundEffects == 0)
			return;

		audioSource.PlayOneShot (gameOverSound, 0.35f);
	}

	public void PlayBubbleSound()
	{
		if (soundEffects == 0)
			return;

		audioSource.PlayOneShot (bubbleSound, 0.35f);
	}
}
