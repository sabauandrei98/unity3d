using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	public Transform outPosition;
	public TextMesh info;

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			if (GameObject.Find ("KeysSystem").GetComponent<KeysSystem> ().squareIndex == 4) {
				info.text = "You will \nbe teleported";
			} else
				info.text = "You need \nmore keys";
		}
	}
}
