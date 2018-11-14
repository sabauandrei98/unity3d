using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameCore : MonoBehaviour {

	public Text gameScore, gameBest, gameMP;
	public Button[] colorButtons; 
	public Button playButton, pauseButton;
	public AudioClip[] buttonSounds;
	public Sprite wrongTurn, playerTurn, computerTurn;
	public Image gameState;
	public GameObject MPWarning;

	List<int> tableColors = new List<int>();

	//gap time between colors
	float timeBetweenColors = 0.35f;

	//time spent on a color
	float nextColorTime = 0.35f;

	//player <time> computer
	float timeBetweenTurns = 1f;

	int colorIndex, score, best, mPoints;
	bool buttonsState;


	public void Pause()
	{
		if (mPoints >= 10) {
			mPoints -= 10;
			Time.timeScale = 0;
			UpdateMPText ();
			playButton.interactable = true;
			pauseButton.interactable = false;
			buttonsState = colorButtons[0].interactable;
			ButtonInteraction (false);
		} else {
			MPWarning.SetActive (true);
		}
	}

	public void StartGame()
	{
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
			ButtonInteraction (buttonsState);
		}
		else
		AddColorToGame();
	}
		

	void Start () {
		score = 0;
		mPoints = PlayerPrefs.GetInt ("MP", 0);
		UpdateScoreText();
		UpdateBestText();
		UpdateMPText();
	}

	void UpdateScoreText()
	{
		gameScore.text = "Score: " + score.ToString();
	}

	void UpdateBestText()
	{
		best = PlayerPrefs.GetInt("Best", 0);
		if (score > best)
			best = score;
		PlayerPrefs.SetInt("Best", best);

		gameBest.text = "Best: " + best.ToString();
	}

	void UpdateMPText()
	{
		PlayerPrefs.SetInt ("MP", mPoints);
		gameMP.text = "MP: " + mPoints.ToString();
	}

	//make buttons interactable or not
	void ButtonInteraction(bool value)
	{
		for(int i = 0; i < colorButtons.Length; i++)
		{
			colorButtons[i].interactable = value;
		}
	}

	//reset buttons to avoid accidental highlighting
	void ButtonsReset()
	{
		for(int i = 0; i < colorButtons.Length; i++)
		{
			colorButtons[i].gameObject.SetActive(false);
			colorButtons[i].gameObject.SetActive(true);
		}
	}

	//choose a random color and add it to the queue
	void AddColorToGame()
	{
		colorIndex = 0;
		gameState.GetComponent<Image>().sprite = computerTurn;
		ButtonInteraction(false);

		tableColors.Add(Random.Range(0,4));
		//Debug.Log (tableColors [tableColors.Count - 1].ToString ());
		StartCoroutine(ShowColors(nextColorTime));
	}

	IEnumerator TimeBetweenColors(float time)
	{
		yield return new WaitForSeconds(timeBetweenColors);
		StartCoroutine(ShowColors(nextColorTime));
	}

	IEnumerator TimeBetweenTurns(float time)
	{
		gameState.GetComponent<Image>().sprite = computerTurn;
		ButtonInteraction(false);

		yield return new WaitForSeconds(timeBetweenTurns);
		ButtonsReset();
		AddColorToGame();
	}
		
	IEnumerator ShowColors(float time)
	{
		if(colorIndex >= 0)
		{
			GetComponent<AudioSource>().PlayOneShot(buttonSounds[tableColors[colorIndex]]);
			ColorBlock cb = colorButtons[tableColors[colorIndex]].colors;
			cb.disabledColor = Color.white;
			colorButtons[tableColors[colorIndex]].colors = cb;
		}
			
		yield return new WaitForSeconds(time);

		if(colorIndex >= 0)
		{
			ColorBlock cb = colorButtons[tableColors[colorIndex]].colors;
			cb.disabledColor = new Color(1, 1, 1, 0.4f);
			colorButtons[tableColors[colorIndex]].colors = cb;
		}

		if(colorIndex + 1 < tableColors.Count)
		{
			colorIndex++;
			StartCoroutine(TimeBetweenColors(0));
		}
		else
			SwitchToMoveState();
	}


	void SwitchToMoveState()
	{
		colorIndex = 0;
		ButtonInteraction(true);
		gameState.GetComponent<Image>().sprite = playerTurn;
	}

	public void SelectColor(int buttonIndex)
	{
		if (buttonIndex == tableColors[colorIndex])
		{
			colorIndex++;

			if(colorIndex + 1 > tableColors.Count)
			{	
				score++;
				mPoints++;
				UpdateScoreText();
				UpdateBestText();
				UpdateMPText();
				StartCoroutine(TimeBetweenTurns(0));
			}
		}
		else
			GameOver();
	}

	void GameOver()
	{
		gameState.GetComponent<Image>().sprite = wrongTurn;
		ButtonInteraction(false);
	}
}
