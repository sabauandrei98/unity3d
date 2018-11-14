using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;

public class AdManager : MonoBehaviour {

	public static AdManager Instance{ set; get; }

	public string bannerId, interstitialId;
	int startCount;

	// Use this for initialization
	void Start () {
		//Instance = this;
		//DontDestroyOnLoad (gameObject);
		//startCount = 0;
		//Admob.Instance ().setTesting (true);

		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		Admob.Instance ().initAdmob (bannerId, interstitialId);
		Admob.Instance ().loadInterstitial ();
		#endif

		ShowBanner ();
	}

	public void ShowBanner()
	{

		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		Admob.Instance ().showBannerRelative (AdSize.Banner, AdPosition.BOTTOM_CENTER, 5);
		#endif
	}

	public void ShowVideo()
	{

		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		if (Admob.Instance ().isInterstitialReady ()) {
			Admob.Instance ().showInterstitial ();
		}
		#endif
	}
}
