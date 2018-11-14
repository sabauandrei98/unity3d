using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour {

	int numberOfLevels = 40;
	int[] aLevels = new int[50];
	public Text unlockedInfo;

	void Awake()
	{
		PlayerPrefs.SetInt ("aLevel1", 1);
	}

	// Use this for initialization
	void Start () {

		int unlocked = 0;
		for (int i = 1; i <= numberOfLevels; i++) {
			aLevels [i] = PlayerPrefs.GetInt ("aLevel" + i.ToString (), 0);
			if (aLevels [i] == 1)
				unlocked++;
		}

		unlockedInfo.text = "CONGRATS!\nYou have unlocked\n" + unlocked.ToString() + "/" + numberOfLevels.ToString() + " Levels";
	}

}
