using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StarShop : MonoBehaviour {

	public void SendColor()
	{
		GameObject.Find ("PlayerColor").GetComponent<StarsShopPlayerColor> ().GetColor 
		(EventSystem.current.currentSelectedGameObject.name, EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color);
	}

	public void BuyColor()
	{
		int stars = GameObject.Find ("StarsStatsText").GetComponent<StarsSys> ().stars;
		string selectedColor = GameObject.Find ("PlayerColor").GetComponent<StarsShopPlayerColor> ().selectedColor;

		if (stars >= 15 && selectedColor != "") {
			stars -= 15;
			PlayerPrefs.SetInt ("Stars", stars);
			PlayerPrefs.SetString ("PlayerColor", selectedColor);
			PlayerPrefs.Save ();

			GameObject.Find ("StarsStatsText").GetComponent<StarsSys> ().UpdateText ();
		}
	}
}
