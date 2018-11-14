using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

	TextMesh txt;

	// Use this for initialization
	void Start () {
		txt = GetComponent<TextMesh>();

		int played = PlayerPrefs.GetInt("Played");
		float win = PlayerPrefs.GetInt("Won");
		float lost = PlayerPrefs.GetInt("Lost");

		float rate = win / lost;

		txt.text = "Played\n" + played + 
					"\n\n" + 
					"Win rate\n" + rate.ToString("F2"); 
				;
	}
	

}
