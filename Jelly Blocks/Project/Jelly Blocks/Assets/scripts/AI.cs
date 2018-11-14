using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AI : MonoBehaviour {

	GameObject[,] board = new GameObject[9,9];
	Board gameBoard;
	GUI gameUI;
	MCTS gameMTCS;

	void Start()
	{
		gameBoard = GameObject.Find ("Scripts").GetComponent<Board> ();
		gameUI = GameObject.Find ("Scripts").GetComponent<GUI> ();
		gameMTCS = GameObject.Find ("Scripts").GetComponent<MCTS> ();
	}
		
	Vector2 bestPoint()
	{
		List<Vector2> coords = new List<Vector2> ();

		for (int i = 1; i <= 8; i++)
			for (int j = 1; j <= 8; j++)
				if (board [i, j].GetComponent<Button> ().IsInteractable ()) 
					coords.Add (new Vector2 (i, j));

		int rand = Random.Range (0, coords.Count);
		
		return coords[rand];
	}

	IEnumerator moveAITime(float time)
	{
		Vector2 point = Vector2.zero;
		if (gameBoard.boardEmptySquares () > 25)
			point = bestPoint ();
		else {
			point = gameMTCS.getPoint ();
			if (point.x == 0 && point.y == 0)
				point = bestPoint ();
		}

		yield return new WaitForSeconds (time);

		gameBoard.markArea ((int)point.x, (int)point.y, "0");
		gameBoard.boardInteractable = true;
		if (!gameBoard.isWinner ("AI")) {
			gameUI.updateInfo ("X");
		} else {
			gameUI.updateInfo ("X");
			gameUI.gameOverUpdateText ("AI");
			gameUI.showRestartButton ();
			gameUI.updateWinRate ("AI");
			gameUI.gameOverShowAd ();
		}
	}

	public void moveAI()
	{
		board = gameBoard.board;
		gameBoard.boardInteractable = false;
		StartCoroutine ("moveAITime", 1);
	}
}
