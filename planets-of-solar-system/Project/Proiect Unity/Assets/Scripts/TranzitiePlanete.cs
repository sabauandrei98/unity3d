using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranzitiePlanete : MonoBehaviour {


	public GameObject[] planete;
	Vector3[] offSet = new Vector3[10];
	int index = -1;
	int maxIndex = 9, minIndex = 0;

	string[] numePl = new string[10]{"Mercur", "Venus", "Pamant", "Luna", "Marte", "Jupiter", "Saturn", "Uranus", "Neptun", "Pluto"};

	void Start()
	{
		offSet[0] = new Vector3(1, 0.5f, 0); //Mercur
		offSet[1] = new Vector3(1, 0.5f, 0); //Venus
		offSet[2] = new Vector3(1, 0.5f, 0); //Pamant
		offSet[3] = new Vector3(0.5f, 0.4f, 0); //Luna
		offSet[4] = new Vector3(1, 0.5f, 0); //Marte
		offSet[5] = new Vector3(3, 1.0f, 0); //Jupiter
		offSet[6] = new Vector3(3, 1.0f, 0); //Saturn
		offSet[7] = new Vector3(2, 0.7f, 0); //Uranus
		offSet[8] = new Vector3(2, 0.8f, 0); //Neptun
		offSet[9] = new Vector3(1, 0.5f, 0); //Pluto
	}

	void Update()
	{
		if(Camera.main.orthographic == false && index != -1)
		{
			Camera.main.transform.LookAt(planete[index].transform);
			Camera.main.transform.position = planete[index].transform.position + offSet[index];
		}
		if(index != -1 && Camera.main.orthographic == false)
			GameObject.FindGameObjectWithTag("TitluPlanetaSimulare").GetComponent<Text>().text = numePl[index];
	}

	void MoveTo(int ind)
	{
		PlayerPrefs.SetFloat("TimeCoef", 10);
		Camera.main.orthographic = false;
		Camera.main.transform.eulerAngles = Vector3.zero;

	}

	public void NextPlanet()
	{
		if(index + 1 <= maxIndex)
		{
			index ++;
			MoveTo(index);
		}
	}

	public void PrevPlanet()
	{
		if(index - 1 >= minIndex)
		{
			index --;
			MoveTo(index);
		}
	}
}
