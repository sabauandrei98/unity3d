using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine.UI;
using System;

public class Rewarded : MonoBehaviour {

	private RewardBasedVideoAd rewarded;
	bool ready;
	float time = 50;
	public Text remainingTime;

	public void Start () {
		StartCoroutine(CreateAd(2f));
	}

	IEnumerator CreateAd(float t)
	{
		yield return new WaitForSeconds (t);

		rewarded = RewardBasedVideoAd.Instance;
		rewarded.OnAdRewarded += HandleOnAdRewarded;
		rewarded.OnAdClosed += HandleOnAdClosed;
		ready = false;

		LoadRewarded ();
		StartCoroutine (CheckIfLoaded (0.5f));
	}

	IEnumerator CheckIfLoaded(float t)
	{
		yield return new WaitForSeconds (t);
		if (!ready && rewarded.IsLoaded ()) {
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
		
	public void PlayRewarded()
	{
		rewarded.Show ();
	}
	
	private void LoadRewarded()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-8267006366479170/2997128548";
		#else
		string adUnitId = "unexpedted_platform";
		#endif

		rewarded.LoadAd (new AdRequest.Builder ().Build (), adUnitId);
	}

	public void HandleOnAdRewarded(object sender, Reward args)
	{
		GameObject.Find ("Events").GetComponent<ButtonClick> ().score += 300;
		GameObject.Find ("Events").GetComponent<ButtonClick> ().UpdateScore ();
	}
	
	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		ready = false;
		gameObject.GetComponent<Button> ().interactable = false;
		LoadRewarded ();
		time = 50;
		StartCoroutine (RemainingTime (0));
	}

}