using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsSys : MonoBehaviour {

	public int hearts;
	public Sprite heartOff;

	void Awake () {
		hearts = PlayerPrefs.GetInt ("Hearts", 30);
		hearts = 10;
		GetComponent<Text> ().text = hearts.ToString();
	}
	
	public void ReduceHeart()
	{
		hearts--;
		if (hearts == 0) {
			GameObject.Find ("NoHeartsLeft").GetComponent<Text> ().enabled = true;
			GameObject go = GameObject.Find ("NoHeartsLeft").transform.Find ("Shop").gameObject;
			go.SetActive (true);
			GameObject.Find ("HeartsSys").transform.Find ("Heart").GetComponent<Image> ().sprite = heartOff;
		}

		GetComponent<Text> ().text = hearts.ToString();
		PlayerPrefs.SetInt ("Hearts", hearts);
		PlayerPrefs.Save ();
	}

	public void UpdateText()
	{
		hearts = PlayerPrefs.GetInt ("Hearts");
		GetComponent<Text> ().text = hearts.ToString ();
	}
}
