using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager: MonoBehaviour {

	GameObject shop;

	PointsManager pManager;

	/// <summary>
	/// item:
	/// 0 -> buy item
	/// 1 -> equip item
	/// 2 -> equiped
	/// </summary>

	public int getItemPrice(string itemName)
	{
		if (itemName == "Default")
			return 0;
		if (itemName == "Frostmid")
			return 45;
		if (itemName == "Lavamid")
			return 45;
		if (itemName == "Earthmid")
			return 45;
		
		if (itemName == "Aquamid")
			return 95;
		if (itemName == "Airmid")
			return 95;
		if (itemName == "Rastamid")
			return 95;
		if (itemName == "Chillimid")
			return 95;
		
		if (itemName == "Bubblegummid")
			return 195;
		if (itemName == "Toxicmid")
			return 195;
		if (itemName == "Sunnymid")
			return 195;
		if (itemName == "Rainbowmid")
			return 195;

		return -1;
	}


	public void UpdateShop()
	{
		for (int i = 0; i < shop.transform.childCount; i++) {
			string itemName = shop.transform.GetChild (i).name;

			int type = PlayerPrefs.GetInt (itemName, 0);

			if (type == 0) {
				shop.transform.GetChild (i).transform.GetChild (2).transform.GetChild (0).GetComponent<Text> ().text = "Buy" + " " + getItemPrice (itemName).ToString ();
			} else if (type == 1) {
				shop.transform.GetChild (i).transform.GetChild (2).transform.GetChild (0).GetComponent<Text> ().text = "Equip";
				shop.transform.GetChild (i).transform.GetChild (2).transform.GetChild (1).gameObject.SetActive (false);
				shop.transform.GetChild (i).transform.GetChild (2).GetComponent<Image> ().color = new Color (0, 0.5f, 0.4f);

			} else if (type == 2) {
				shop.transform.GetChild (i).transform.GetChild (2).transform.GetChild (0).GetComponent<Text> ().text = "Equipped";
				shop.transform.GetChild (i).transform.GetChild (2).transform.GetChild (1).gameObject.SetActive (false);
				shop.transform.GetChild (i).transform.GetChild (2).GetComponent<Image> ().color = new Color (0.55f, 0.72f, 0);
			}
		}
	}


	void Start () {
		shop = GameObject.Find ("Shop");
		pManager = GameObject.Find ("Points").GetComponent<PointsManager> ();
		UpdateShop ();
	}

	public void OnShopButtonPress()
	{
		string itemName = EventSystem.current.currentSelectedGameObject.name;

		int type = PlayerPrefs.GetInt (itemName, 0);
		if (type == 0) {
			if (pManager.points > getItemPrice (itemName)) {
				pManager.points -= getItemPrice (itemName);
				pManager.UpdatePointsText ();
				PlayerPrefs.SetInt (itemName, 1);
				UpdateShop ();
			}
		} else if (type == 1) {
			string currentEquiped = PlayerPrefs.GetString ("currentItem", "Default");
			PlayerPrefs.SetInt (currentEquiped, 1);
			PlayerPrefs.SetInt (itemName, 2);
			PlayerPrefs.SetString ("currentItem", itemName);
			UpdateShop ();
		} else if (type == 2) {
			//nothing
		}
	}
	

}
