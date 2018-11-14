using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartShop : MonoBehaviour {

	void AddHearts(int number)
	{
		int hearts = PlayerPrefs.GetInt ("Hearts", 0);
		hearts += number;

		PlayerPrefs.SetInt ("Hearts", hearts);
		PlayerPrefs.Save ();
	}

	public void WatchAd()
	{
		AddHearts (10);
		GameObject.Find ("HeartsStatsText").GetComponent<HeartsSys> ().UpdateText ();
	}

	public void ConvertStars()
	{
		int stars = PlayerPrefs.GetInt ("Stars");

		if (stars > 0) {
			stars--;
			PlayerPrefs.SetInt ("Stars", stars);
			GameObject.Find ("StarsStatsText").GetComponent<StarsSys> ().UpdateText ();

			AddHearts (3);
			GameObject.Find ("HeartsStatsText").GetComponent<HeartsSys> ().UpdateText ();
		}
	}
}
