using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StarsShopPlayerColor : MonoBehaviour {

	public string selectedColor = "";

	void Start () {
		GetComponent<Image> ().color = Color.white;
	}
	
	public void GetColor(string col, Color color)
	{
		GetComponent<Image> ().color = color;
		selectedColor = col;
	}
		
}
