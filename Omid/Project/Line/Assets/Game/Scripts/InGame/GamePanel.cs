using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {

	public bool touchToPlay = false;
	public bool gameOver = false;
	public bool drunkMode;

	public GameObject revivePanel;
	bool revivedAlready = false;

	int distanceBetweenDrunkMode = 3;
	int drunkModeDuration = 2;

	//Button Functions script
	ButtonFunctions buttonFunctions;

	//Sound manager script
	SoundManager soundManager;

	//UI Manager script
	UIManager uiManager;

	//Animation manager script
	AnimationManager animationManager;

	DrunkModeManager drunkModeManager;

	GameObject TapToPlayButton;

	void Start()
	{
		soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		uiManager = GameObject.Find ("UIManager").GetComponent<UIManager> ();
		buttonFunctions = GameObject.Find ("ButtonFunctions").GetComponent<ButtonFunctions> ();
		animationManager = GameObject.Find ("AnimationManager").GetComponent<AnimationManager> ();
		drunkModeManager = GameObject.Find ("DrunkModeManager").GetComponent<DrunkModeManager> ();
		TapToPlayButton = GameObject.Find ("TapToPlayButton");

		manageDrunkMode ();
	}

	public void TapToPlay()
	{
		touchToPlay = true;
	}

	public void setGameOver()
	{
		//Stop the game
		gameOver = true;

		uiManager.SetBackground ("dead");

		//Play a specific sound
		soundManager.PlayGameOverSound ();

		//Load the main menu
		float positionY = GameObject.Find("Line").transform.position.y;
		if (PlayerPrefs.GetInt ("Points", 0) >= 10 && positionY > -5.35f && positionY < 5.35f && !revivedAlready) {
			revivePanel.SetActive (true);
			GameObject.Find ("ReviveScoreText").GetComponent<Text> ().text = "Score: " + "<color=#DFFB5CFF>" + uiManager.score.text + "</color>";
			revivedAlready = true;
		}
		else
			buttonFunctions.StartCoroutine(buttonFunctions.loadMainMenu(1.25f));

		int gamesPlayed = PlayerPrefs.GetInt ("gamesPlayed", 0) + 1;
		int leafPoints = PlayerPrefs.GetInt ("Points", 0) - 1;
		PlayerPrefs.SetInt ("gamesPlayed", gamesPlayed);
		PlayerPrefs.SetInt ("Points", leafPoints);

		PlayerPrefs.Save ();
	}
		
	public void setScore()
	{
		//Add score
		uiManager.AddScore ();

		//Play a specific sound
		soundManager.PlayScoreSound ();

		//Specific score text animation
		animationManager.playScoreAnimation ();

		//Set last score for main menu
		int newScore = int.Parse (uiManager.score.text);

		if (SceneManager.GetActiveScene ().name == "easy") {
			int best = PlayerPrefs.GetInt ("easyBest", 0);
			if (best < newScore)
				PlayerPrefs.SetInt ("easyBest", newScore);
		}
		if (SceneManager.GetActiveScene ().name == "medium") {
			int best = PlayerPrefs.GetInt ("mediumBest", 0);
			if (best < newScore)
				PlayerPrefs.SetInt ("mediumBest", newScore);
		}
		if (SceneManager.GetActiveScene ().name == "hard") {
			int best = PlayerPrefs.GetInt ("hardBest", 0);
			if (best < newScore)
				PlayerPrefs.SetInt ("hardBest", newScore);
		}


		//Set points for main menu
		int points = PlayerPrefs.GetInt ("Points", 0);
		PlayerPrefs.SetInt ("Points", points + 1);

		//Save all the prefs
		PlayerPrefs.Save ();
	}

	public void manageDrunkMode()
	{
		int score = int.Parse (uiManager.score.text);

		bool currentState = false;

		score %= (distanceBetweenDrunkMode + drunkModeDuration);
		if (score < distanceBetweenDrunkMode) {
			uiManager.SetDrunkModeText (distanceBetweenDrunkMode - score);
		} else {
			currentState = true;
		}
		if (currentState != drunkMode) {
			uiManager.SetDrunkModeText (distanceBetweenDrunkMode - score);
			drunkMode = currentState;
			drunkModeManager.SwitchDrunkMode (currentState);
		}
	}

	public void Revive()
	{
		gameOver = false;
		touchToPlay = false;
		TapToPlayButton.SetActive (true);

		int points = PlayerPrefs.GetInt ("Points", 0) - 10;
		PlayerPrefs.SetInt ("Points", points);

		if(drunkMode)
			uiManager.SetBackground ("drunk");
		else
			uiManager.SetBackground ("normal");
		
		revivePanel.SetActive (false);

		GameObject[] walls = GameObject.FindGameObjectsWithTag ("wall");
		for (int i = 0; i < walls.Length; i++)
			Destroy (walls [i]);
	}
		
}
