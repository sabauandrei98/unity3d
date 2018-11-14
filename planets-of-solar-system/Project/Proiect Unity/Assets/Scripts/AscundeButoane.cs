using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscundeButoane : MonoBehaviour {


	public GameObject[] UI;
	// Update is called once per frame
	void Update () {

		UI[0].SetActive(Camera.main.orthographic);
		UI[1].SetActive(!Camera.main.orthographic);
	
	}
}
