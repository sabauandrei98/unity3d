using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelIntro : MonoBehaviour {

	public AudioClip clip;
	float clipVol = 1f;
	int level;

	IEnumerator startLevel(float t)
	{
		yield return new WaitForSeconds (t);
		SceneManager.LoadScene (level.ToString());
	}

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<AudioSource> ().PlayOneShot (clip, clipVol * PlayerPrefs.GetInt ("musicVol", 1));

		string sName = SceneManager.GetActiveScene ().name;
		level = 0;

		for (int i = 0; i < sName.Length; i++) {
			if ('0' <= sName [i] && sName [i] <= '9')
				level = level * 10 + (sName [i] - '0');
		}
			
		StartCoroutine (startLevel (6.5f));
	}

}
