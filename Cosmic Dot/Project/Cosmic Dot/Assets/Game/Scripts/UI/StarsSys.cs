using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsSys : MonoBehaviour {

	public int stars;

	void Start () {
		stars = PlayerPrefs.GetInt ("Stars");
		GetComponent<Text> ().text = stars.ToString ();
	}
	
	public void AddStar()
	{
		stars++;
		GetComponent<Text> ().text = stars.ToString ();
		PlayerPrefs.SetInt ("Stars", stars);
		PlayerPrefs.Save ();
	}

	public void UpdateText()
	{
		stars = PlayerPrefs.GetInt ("Stars");
		GetComponent<Text> ().text = stars.ToString();
	}
}
