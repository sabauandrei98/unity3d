using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Board : MonoBehaviour {

	public GameObject[,] board = new GameObject[9,9];
	GUI gameUI;
	public int gamePlayers = 0;
	public bool boardInteractable = true;

	void Start () {
		buildMap ();
		gameUI = GameObject.Find ("Scripts").GetComponent<GUI> ();
		gameUI.updateInfo ("X");
	}

	public bool isOnBoard(int x, int y)
	{
		if (x < 1 || x > 8 || y < 1 || y > 8)
			return false;
		return true;
	}

	public int getAvailableSquares(int x, int y)
	{
		int goodSquares = 0;
		for (int i = x - 1; i <= x + 1; i++)
			for (int j = y - 1; j <= y + 1; j++)
				if (isOnBoard (i, j) && board [i, j].GetComponent<Button> ().IsInteractable ())
					goodSquares++;
			
		return goodSquares;
	}

	public int boardEmptySquares()
	{
		int empty = 0;
		for (int i = 1; i <= 8; i++)
			for (int j = 1; j <= 8; j++)
				if (board [i, j].GetComponent<Button> ().IsInteractable () == true)
					empty++;

		return empty;
	}
		

	public Vector2 numberToCoords(int n)
	{
		int x = n / 8 + 1;
		if (n % 8 == 0)
			x--;
		int y = n - (x - 1) * 8;

		return new Vector2 (x, y);
	}

	public bool isWinner(string winner)
	{
		for (int i = 1; i <= 8; i++)
			for (int j = 1; j <= 8; j++)
				if (board [i, j].GetComponent<Button> ().IsInteractable () == true)
					return false;
		return true;
	}

	public void markArea(int x, int y, string symbol)
	{
		for (int i = x - 1; i <= x + 1; i++)
			for (int j = y - 1; j <= y + 1; j++) {
				if (isOnBoard (i, j) && board [i, j].GetComponent<Button> ().IsInteractable ()) {
					board [i, j].GetComponent<Button> ().interactable = false;
					gameUI.updateButtonColor (i, j, symbol);
					gameUI.buttonAnimation (i, j);
				}
			}
	}
		
	void buildMap()
	{
		foreach (Transform child in GameObject.Find("GameBoard").transform) {
			Vector2 point = Vector2.zero;
			point = numberToCoords (int.Parse (child.name));
	
			board [(int)point.x, (int)point.y] = child.gameObject;
		}
	}
}
