using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	//SCRIPTS
	private UI gameUI;
	private Generate gameGenerator;

	//VARS
	int score;
	int bestScore;
	int roundTime = 30;
	bool isWrong;
	bool gameOver = false;

	void Initialization()
	{
		//SCRIPTS
		gameUI = gameObject.GetComponent<UI> ();
		gameGenerator = gameObject.GetComponent<Generate> ();

		//VARS
		score = 0;
		bestScore = PlayerPrefs.GetInt ("bestScore", 0);

		//TEXT
		gameUI.setScore (score);
		gameUI.setBest (bestScore);

		//BUTTONS
		gameUI.ShowButtons (true);

	}
		
	void Start()
	{
		Initialization ();
		Run ();
	}

	IEnumerator roundTimer(float t)
	{
		yield return new WaitForSeconds (t);
		if (roundTime - t >= 0) {
			roundTime -= (int) t;
			gameUI.setTime (roundTime);

			StartCoroutine (roundTimer (1f));
		} else{
			if (!gameOver)
				FinishGame ();
		}
	}

	void Run()
	{
		roundTime = 30;
		StartCoroutine (roundTimer (1f));
		GeneratePattern ();
	}

	void FinishGame()
	{
		roundTime = 0;
		gameOver = true;
		gameUI.ShowButtons (false);
		gameUI.ShowResults ();
		gameUI.ResultsSetScore (score);
		gameUI.setRank (score);
	}

	void GoodAnswer()
	{
		score++;
		if (bestScore < score) {
			bestScore = score;
			PlayerPrefs.SetInt ("bestScore", bestScore);
			PlayerPrefs.Save ();
		}
		
		gameUI.setScore (score);
		gameUI.setBest (bestScore);
		gameUI.AnswerParticles ();
	}

	void GeneratePattern()
	{
		int randomVal = Random.Range (0, 2);
		if (randomVal == 1)
			isWrong = true;
		else
			isWrong = false;

		gameGenerator.GeneratePattern (isWrong);
	}

	public void ButtonYes()
	{
		if (isWrong == false) {
			GoodAnswer ();
			GeneratePattern ();
		} else {
			FinishGame ();
		}
	}

	public void ButtonNo()
	{
		if (isWrong == true) {
			GoodAnswer ();
			GeneratePattern ();
		} else {
			FinishGame ();
		}
	}


}
