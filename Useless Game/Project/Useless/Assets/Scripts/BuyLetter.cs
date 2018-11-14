using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyLetter : MonoBehaviour {

	public void Buy()
	{
		if (GameObject.Find ("Events").GetComponent<ButtonClick> ().score >= 2500) {
			EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().color = new Color (1, 0, 0, 0.9f);
			EventSystem.current.currentSelectedGameObject.gameObject.name = "1";
			EventSystem.current.currentSelectedGameObject.GetComponent<Button> ().interactable = false;
			GameObject.Find ("Events").GetComponent<ButtonClick> ().score -= 2500;
			GameObject.Find ("Events").GetComponent<ButtonClick> ().UpdateScore ();
		}
	}
}
