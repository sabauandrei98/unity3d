using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lava : MonoBehaviour {

	bool inProgress = false;
	float timeBetweenDamage = 0.05f;
	GameObject healthBar;


	void Start()
	{
		healthBar = GameObject.Find ("HealthBar");
	}

	IEnumerator LavaDamage(float time)
	{
		yield return new WaitForSeconds (time);
		inProgress = false;
		if(healthBar.GetComponent<HealthBar> ().health > 0)
			healthBar.GetComponent<HealthBar> ().HealthBarChangeValue (-2f);
	}

	void OnTriggerStay (Collider col)
	{
		if(col.gameObject.tag == "Player" && !inProgress)
		{
			inProgress = true;
			StartCoroutine("LavaDamage", timeBetweenDamage);
		}
	}
}
