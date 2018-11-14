using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitezaReprezentare : MonoBehaviour {

	float speed;
	Text viteza;

	void UpdateText()
	{
		string velocity;
		if (speed == 20)
			velocity = "1";
		else
			velocity = (speed/50).ToString();
		
		GameObject.FindWithTag("VitezaText").GetComponent<Text>().text = velocity;
	}

	void Awake()
	{
		PlayerPrefs.SetFloat("TimeCoef", 50);
		speed = PlayerPrefs.GetFloat("TimeCoef", 0);
		UpdateText();
	}
		

	public void Up()
	{
		
		if (speed + 50 <= 1000 && Camera.main.orthographic == true)
		{
			speed += 50;
			PlayerPrefs.SetFloat("TimeCoef", speed);
			UpdateText();
		}
	}

	public void Down()
	{
		if (speed - 50 >= 0 && Camera.main.orthographic == true)
		{
			speed -= 50;
			PlayerPrefs.SetFloat("TimeCoef", speed);
			UpdateText();
		}
	}
}
