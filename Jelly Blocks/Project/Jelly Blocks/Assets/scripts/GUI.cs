using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class GUI : MonoBehaviour {

	Board gameBoard;
	Text gameOverText;
	Text emptySquaresText;
	Image turnImage;
	private InterstitialAd interstitial;

	void Awake()
	{

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		gameBoard = GameObject.Find ("Scripts").GetComponent<Board> ();
		gameOverText = GameObject.Find ("gameOverText").GetComponent<Text> ();
		emptySquaresText = GameObject.Find ("emptySquaresText").GetComponent<Text> ();
		turnImage = GameObject.Find ("turnImage").GetComponent<Image> ();

		increasePlayedGames ();
		RequestInterstitial ();
	}
		
	public void increasePlayedRounds()
	{
		if (gameBoard.gamePlayers == 1) {
			int rounds = PlayerPrefs.GetInt ("RoundsPlayed", 0) + 1;
			PlayerPrefs.SetInt ("RoundsPlayed", rounds);
			PlayerPrefs.Save ();
		}
	}

	void increasePlayedGames()
	{
		int games = PlayerPrefs.GetInt ("GamesPlayed", 0) + 1;
		PlayerPrefs.SetInt ("GamesPlayed", games);
		PlayerPrefs.Save ();
	}

	public void gameOverUpdateText(string winner)
	{
		gameOverText.text = "Winner:\n" + winner;
	}
		

	public void updateEmptySquares()
	{
		emptySquaresText.text = gameBoard.boardEmptySquares ().ToString ();
	}

	public void updateButtonColor(int x, int y, string symbol)
	{
		if (symbol == "X") {
			Color colorX = new Color (0.5f, 0.68f, 0.8f, 0.7f);
			gameBoard.board [x, y].GetComponent<Button> ().image.color = colorX;
		} else if (symbol == "0") {
			Color colorO = new Color (1, 0.8f, 0.42f, 0.7f);
			gameBoard.board [x, y].GetComponent<Button> ().image.color = colorO;
		}
	}

	public void updateSquareImage(string symbol)
	{
		//red Color colorX = new Color (0.96f, 0.45f, 0.5f, 1);
		if (symbol == "X") {
			Color colorX = new Color (0.5f, 0.68f, 0.8f, 0.6f);
			turnImage.color = colorX;
		} else if (symbol == "0") {
			Color colorO = new Color (1, 0.8f, 0.42f, 0.6f);
			turnImage.color = colorO;
		}
	}

	public void buttonAnimation(int x, int y)
	{
		gameBoard.board [x, y].GetComponent<Animator> ().SetBool ("PlayAnim", true);
	}

	public void showRestartButton()
	{
		GameObject.Find ("RestartButton").GetComponent<Animator> ().SetBool ("Show", true);
		gameOverShowAd ();
	}

	public void updateWinRate(string winner)
	{
		increasePlayedRounds ();
		int playerWins = PlayerPrefs.GetInt ("PlayerWins", 0);

		if (winner == "Player")
			playerWins++;

		PlayerPrefs.SetInt ("PlayerWins", playerWins);
		PlayerPrefs.Save ();
		GameObject.Find ("WinRateUI").GetComponent<WinRateUI> ().updateWinRate ();
	}
		

	public void updateInfo(string symbol)
	{
		updateSquareImage (symbol);
		updateEmptySquares ();
	}


/////////////////////////////////////////////////////////////////

	public void gameOverShowAd()
	{
		if (PlayerPrefs.GetInt ("GamesPlayed", 0) % 3 == 0)
		if (interstitial.IsLoaded ()) {
			interstitial.Show ();
		}
	}
	
	private void RequestInterstitial()
	{
		if (PlayerPrefs.GetInt ("GamesPlayed", 0) % 3 != 0)
			return;

		#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-8267006366479170/6518510817";
		#else
			string adUnitId = "unexpected_platform";
		#endif

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}


}
