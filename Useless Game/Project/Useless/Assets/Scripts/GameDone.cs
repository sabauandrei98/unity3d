using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDone : MonoBehaviour {

	public GameObject[] go;

	// Use this for initialization
	void Start () {
		StartCoroutine (Done (1f));
	}
	
	IEnumerator Done(float t)
	{
		yield return new WaitForSeconds (t);
		int g = 0;
		for (int i = 0; i < go.Length; i++)
			if (go [i].name == "1")
				g++;

		if (g == 7)
			SceneManager.LoadScene ("gameDone");
		else
			StartCoroutine (Done (1f));
	}
}
