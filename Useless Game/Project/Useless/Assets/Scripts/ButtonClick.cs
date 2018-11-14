using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour {

	public Text scoreTxt;
	public int score = 0;

	void Start () {
		score = PlayerPrefs.GetInt ("Score", 0);
		scoreTxt.text = "Useless points:\n" + score.ToString ();
	}
	
	public void SimpleClick()
	{
		score++;
		scoreTxt.text = "Useless points:\n" + score;
		PlayerPrefs.SetInt("Score", score);
		PlayerPrefs.Save();
	}

	public void UpdateScore()
	{
		scoreTxt.text = "Useless points:\n" + score;
		PlayerPrefs.SetInt("Score", score);
		PlayerPrefs.Save();
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
