using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public float health = 100;
	public Text healthText;

	void Start()
	{
		GetComponent<Slider> ().value = health;
		healthText.text = "Health " + health.ToString () + "%";
	}

	public void HealthBarChangeValue(float value)
	{
		if (health + value <= 0) {

			//update hearts
			GameObject.Find ("HeartsSys").transform.Find ("Text").GetComponent<HeartsSys> ().ReduceHeart ();

			health = 0;
			GameObject.FindWithTag ("Player").GetComponent<MoveSphere> ().RespawningProcess ();

		} else {
			health += value;

			if (health > 100)
				health = 100;
		}

		GetComponent<Slider> ().value = health;
		healthText.text = "Health " + health.ToString ("F1") + "%";
	}
}
