using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSchimbaPlaneta : MonoBehaviour {

	public GameObject[] planete;
	public Text[] descrierePlanete;
	public Text titluPlaneta;
	string[] titluPlanete = new string[]{"Mercur", "Venus", "Pamant", "Luna", "Marte", "Jupiter", "Saturn", "Uranus", "Neptun", "Pluto"};
	

	public void SchimbaPlaneta(int index)
	{
		planete[index].SetActive(true);
		descrierePlanete[index].gameObject.SetActive(true);
		titluPlaneta.text = titluPlanete[index];
	}

	public void TurnVisibleOff(int index)
	{
		planete[index].SetActive(false);
		descrierePlanete[index].gameObject.SetActive(false);
	}
}
