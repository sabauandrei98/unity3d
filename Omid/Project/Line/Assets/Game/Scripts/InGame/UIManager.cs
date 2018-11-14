using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text score;
	public Text drunkMode;
	public GameObject myBackground;
	public Sprite normalBackground, drunkBackground, deadBackground;

	public void AddScore()
	{
		int newScore = int.Parse (score.text) + 1;
		score.text = newScore.ToString ();
	}

	public void SetDrunkModeText(int n)
	{
		drunkMode.text = n.ToString ();
	}

	public void SetBackground(string mode)
	{
		if (mode == "normal") {
			myBackground.GetComponent<Image> ().sprite = normalBackground;
		} else if (mode == "drunk") {
			myBackground.GetComponent<Image> ().sprite = drunkBackground;
		} else if (mode == "dead") {
			myBackground.GetComponent<Image> ().sprite = deadBackground;
		}
	}


}
