using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinRateUI : MonoBehaviour {

	public void updateWinRate()
	{
		int rounds = PlayerPrefs.GetInt ("RoundsPlayed", 1);
		int playerWins = PlayerPrefs.GetInt ("PlayerWins", 0);
		gameObject.GetComponent<Text> ().text = "Win rate: " + ((double)playerWins / rounds * 100).ToString ("F1") + "%" + '\n' + playerWins.ToString() + "/" + rounds.ToString();
	}
		
	void Start () {
		updateWinRate ();
	}
	

}