using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour {

	bool oneShot = false;
	int numberOfLevels = 40;
	public GameObject LevelFinish;

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player" && !oneShot) {
			oneShot = true;
			col.gameObject.GetComponent<MoveSphere> ().enabled = false;
			//Camera.main.GetComponent<BloomOptimized> ().threshold = 1.3f;
			GameObject.Find ("Platform(Clone)").SetActive (false);
			GameObject.Find ("Player").SetActive (false);

			LevelFinish.SetActive (true);

			int Level = int.Parse (SceneManager.GetActiveScene ().name) + 1;
			if (Level <= numberOfLevels) {
				PlayerPrefs.SetInt ("aLevel" + Level, 1);
			} 

			gameObject.SetActive (false);
		}
	}
}
