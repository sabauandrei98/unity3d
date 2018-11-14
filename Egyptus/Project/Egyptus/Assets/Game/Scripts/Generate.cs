using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generate : MonoBehaviour {

	GameObject gameBoard;
	public Sprite[] arrowsSprites;

	void Awake () {
		gameBoard = GameObject.Find ("Panel");
	}

	int abs(int a)
	{
		if (a < 0)
			return -a;
		return a;
	}

	public void GeneratePattern(bool isWrong)
	{
		List<int> arrowsList = new List<int>();
		for (int i = 1; i <= 8; i++)
			arrowsList.Add (i);

		for (int i = 1; i <= 8; i++) {
			int rand = Random.Range (0, arrowsList.Count); 
			int item = arrowsList [rand];

			arrowsList.RemoveAt (rand);
			gameBoard.transform.GetChild (i - 1).GetComponent<Image> ().sprite = arrowsSprites [item - 1];
		}

		if (isWrong) {
			int randPos1 = Random.Range (0, 7);
			int randPos2 = Random.Range (0, 7);
			while (abs (randPos1 - randPos2) <= 2)
				randPos2 = Random.Range (0, 8);

			gameBoard.transform.GetChild (randPos2).GetComponent<Image> ().sprite = gameBoard.transform.GetChild (randPos1).GetComponent<Image> ().sprite;
		}
	}
}
