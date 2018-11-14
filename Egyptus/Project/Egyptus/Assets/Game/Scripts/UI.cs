using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class UI : MonoBehaviour {

	public Text score, best, time;
	public GameObject resultsScroll;
	public Sprite[] ranks;

	private InterstitialAd interstitial;
	int timesPlayed = 0;

	void Start()
	{
		timesPlayed = PlayerPrefs.GetInt ("timesPlayed", 0);
		if ((timesPlayed + 1) % 4 == 0)
			RequestInterstitial ();
	}

	public void setScore(int val)
	{
		score.text = "Score: " + val.ToString ();
	}

	public void setBest(int val)
	{
		best.text = "Best: " + val.ToString ();
	}

	public void setTime(int val)
	{
		time.text = "Time left: " + val.ToString ();
	}

	public void ShowResults()
	{
		resultsScroll.SetActive (true);
	}

	public void AnswerParticles()
	{
		GameObject.Find ("ParticleSystem").GetComponent<ParticleSystem> ().Play ();
	}

	public void HideResults()
	{
		resultsScroll.SetActive (false);
	}

	public void ResultsSetScore(int val)
	{
		resultsScroll.transform.GetChild (0).GetComponent<Text> ().text = val.ToString ();
	}

	public void ShowButtons(bool val)
	{
		GameObject.Find ("ButtonYes").GetComponent<Button> ().interactable = val;
		GameObject.Find ("ButtonNo").GetComponent<Button> ().interactable = val;
	}

	public void setRank(int val)
	{
		if (val > 13)
			val = 13;

		timesPlayed++;
		PlayerPrefs.SetInt ("timesPlayed", timesPlayed);
		PlayerPrefs.Save ();

		resultsScroll.transform.GetChild (1).GetComponent<Image> ().sprite = ranks [val];

		Debug.Log (timesPlayed.ToString ());
		if (timesPlayed % 4 == 0)
		if (interstitial.IsLoaded ()) {
			interstitial.Show ();
		}
	}



	private void RequestInterstitial()
	{
		#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-8267006366479170/9498790150";
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
