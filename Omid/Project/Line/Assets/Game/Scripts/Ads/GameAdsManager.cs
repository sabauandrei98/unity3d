using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class GameAdsManager : MonoBehaviour {

    
	private string gameId = "1766705";
	private string rewardedId = "rewardedVideo";
	private string interstitialId = "video";

	private string levelName = "";
	private bool instanceCreated = false;


	void Awake()
	{
		if (!instanceCreated) {
			DontDestroyOnLoad (this.gameObject);
			instanceCreated = true;
			Debug.Log ("Instance created");
		}
	}
		
	void Start () {
		Debug.Log ("Instance start");
		Advertisement.Initialize (gameId);
	}
		

	void Update () {
		levelName = SceneManager.GetActiveScene ().name;

		if (levelName == "menu") {
	
			if (Advertisement.IsReady (rewardedId)) {
				GameObject go = GameObject.Find ("Canvas").transform.Find ("BackPannelAds").gameObject;
				if (!go.activeInHierarchy)
					go.SetActive (true);
			}

			ShowInterstitial ();
		} else if (levelName == "easy" || levelName == "medium" || levelName == "hard") {
			if (Advertisement.IsReady (rewardedId)) {
				GameObject go = GameObject.Find ("Canvas").transform.Find ("RevivePanel").transform.Find ("ReviveSubPanel").transform.Find ("ReviveAd").gameObject;
				if (!go.activeInHierarchy)
					go.SetActive (true);
			}
		}

	}

	public void ShowInterstitial()
	{
		if (!Advertisement.IsReady(interstitialId))
			return;

		int gamesPlayed = PlayerPrefs.GetInt ("gamesPlayed", 0);
		if (gamesPlayed % 4 == 0 && gamesPlayed != 0) {

			PlayerPrefs.SetInt ("gamesPlayed", gamesPlayed + 1);
			PlayerPrefs.Save ();
			Advertisement.Show (interstitialId);
		}
	}

	public void ShowRewarded()
	{
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;
		Advertisement.Show(rewardedId, options);
	}
		
	private void HandleShowResult (ShowResult result)
	{
		if (result == ShowResult.Finished) {

			if (SceneManager.GetActiveScene ().name != "menu")
				return;
			
			GameObject.Find ("Canvas").transform.Find ("CongratsPanel").gameObject.SetActive (true);
			int points = PlayerPrefs.GetInt ("Points", 0) + 10;
			PlayerPrefs.SetInt ("Points", points);
			PlayerPrefs.Save ();
			GameObject.Find ("IconsMainMenu").GetComponent<IconsMainMenu> ().SetLeafPoints ();

		} else if (result == ShowResult.Skipped) {

		} else if (result == ShowResult.Failed) {
		}
	}
}
