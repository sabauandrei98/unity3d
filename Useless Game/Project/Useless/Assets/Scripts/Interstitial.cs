using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine.UI;
using System;

public class Interstitial : MonoBehaviour {


	private InterstitialAd interstitial;
	bool ready = false;
	float time = 20;
	public Text remainingTime;

	void Start () {
		StartCoroutine(CreateAd (2f));
	}

	IEnumerator CreateAd(float t)
	{
		yield return new WaitForSeconds (t);

		LoadInterstitial ();
		StartCoroutine (CheckIfLoaded (0.5f));
	}

	IEnumerator CheckIfLoaded(float t)
	{
		yield return new WaitForSeconds (t);
		if (!ready && interstitial.IsLoaded ()) {
			ready = true;
			gameObject.GetComponent<Button> ().interactable = true;
		}
		else
			StartCoroutine (CheckIfLoaded (0.1f));
	}
		
	IEnumerator RemainingTime(float t)
	{
		yield return new WaitForSeconds (t);
		time -= Time.deltaTime;
		if (time < 0)
			time = 0;

		remainingTime.text = time.ToString ("F1");

		if (time > 0)
			StartCoroutine (RemainingTime (0));
		else
			StartCoroutine (CheckIfLoaded (0.1f));

	}

	public void PlayInterstitial()
	{
		interstitial.Show ();
	}

	private void LoadInterstitial()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-8267006366479170/1576413277";
		#else
		string adUnitId = "unexpedted_platform";
		#endif

		interstitial = new InterstitialAd (adUnitId);
		interstitial.OnAdClosed += HandleOnInterstitialClosed;

		AdRequest request = new AdRequest.Builder ().Build ();
		interstitial.LoadAd (request);
	}

	public void HandleOnInterstitialClosed(object sender, EventArgs args)
	{

		GameObject.Find ("Events").GetComponent<ButtonClick> ().score += 50;
		GameObject.Find ("Events").GetComponent<ButtonClick> ().UpdateScore ();

		ready = false;
		gameObject.GetComponent<Button> ().interactable = false;
		LoadInterstitial ();

		time = 20;
		StartCoroutine (RemainingTime (0));
	}

}
