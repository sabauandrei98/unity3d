using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour {

	public int points;

	// Use this for initialization
	void Start () {
		points = PlayerPrefs.GetInt ("Points", 0);
		UpdatePointsText ();
	}

	public void UpdatePointsText()
	{
		gameObject.transform.GetChild (0).GetComponent<Text> ().text = points.ToString();
	}
	

}
