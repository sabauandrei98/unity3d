using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IconsMainMenu : MonoBehaviour {

	public Sprite[] music, effects;
	public GameObject musicButton, effectsButton;
	public Text best;
	public Text points;
	public GameObject[] playButtons;
	public GameObject noLeavesLeft;

	// Use this for initialization

	public void MusicIcon()
	{
		if (PlayerPrefs.GetInt ("Music", 1) == 1)
			musicButton.GetComponent<Image> ().sprite = music [1];
		else
			musicButton.GetComponent<Image> ().sprite = music [0];
	}

	public void EffectsIcon()
	{
		if (PlayerPrefs.GetInt ("Effects", 1) == 1)
			effectsButton.GetComponent<Image> ().sprite = effects [1];
		else
			effectsButton.GetComponent<Image> ().sprite = effects [0];
		
	}

	void SetBestScore()
	{
		best.text = "EASY: <color=#C1AC00FF>" + PlayerPrefs.GetInt ("easyBest", 0).ToString() + "</color>"
			+ '\n' + "MEDIUM: <color=#B06400FF>" + PlayerPrefs.GetInt ("mediumBest", 0).ToString() + "</color>"
			+ '\n' + "HARD: <color=#AC2A00FF>" + PlayerPrefs.GetInt ("hardBest", 0).ToString() + "</color>";
	}
		
	public void SetLeafPoints()
	{
		points.text = PlayerPrefs.GetInt ("Points", 0).ToString ();
	}

	void Update()
	{
		if (PlayerPrefs.GetInt ("Points", 0) == 0) {
			foreach (GameObject go in playButtons)
				if (go.activeInHierarchy)
					go.SetActive (false);
			
			if (!noLeavesLeft.activeInHierarchy)
				noLeavesLeft.SetActive (true);
		} else {
			foreach (GameObject go in playButtons)
				if (!go.activeInHierarchy)
					go.SetActive (true);
			
			if (noLeavesLeft.activeInHierarchy)
				noLeavesLeft.SetActive (false);
		}
	}
		
	void Start () {
		MusicIcon ();
		EffectsIcon ();
		SetBestScore ();
		SetLeafPoints ();
	}

}
