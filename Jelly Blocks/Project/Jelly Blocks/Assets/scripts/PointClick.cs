using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PointClick : MonoBehaviour {

	Board gameBoard;
	AI gameAI;
	GUI gameUI;

	int turn = 0;
	string[] playerNames = new string[2] {"Player1", "Player2"};
	string[] playerSymbols = new string[2] {"X", "0"};

	void Start()
	{
		gameBoard = GameObject.Find ("Scripts").GetComponent<Board> ();
		gameAI = GameObject.Find ("Scripts").GetComponent<AI> ();
		gameUI = GameObject.Find ("Scripts").GetComponent<GUI> ();
	}
		
	public void onPointClick()
	{
		if (!gameBoard.boardInteractable)
			return;

		string name = EventSystem.current.currentSelectedGameObject.name;
		Vector2 point = Vector2.zero;
		point = gameBoard.numberToCoords (int.Parse (name));

		if (gameBoard.gamePlayers == 1) {
			gameBoard.markArea ((int)point.x, (int)point.y, "X");
			if (!gameBoard.isWinner ("Player")) {
				gameUI.updateInfo ("0");
				gameAI.moveAI ();
			} else {
				gameUI.updateInfo ("0");
				gameUI.gameOverUpdateText ("Player");
				gameUI.showRestartButton ();
				gameUI.updateWinRate ("Player");
				gameUI.gameOverShowAd ();
			}
		} 
		else if (gameBoard.gamePlayers == 2) {
			gameBoard.markArea ((int)point.x, (int)point.y, playerSymbols[turn % 2]);
			if (!gameBoard.isWinner (playerNames [turn % 2])) {
				gameUI.updateInfo (playerSymbols [(turn + 1) % 2]);
				turn++;
			} else {
				gameUI.updateInfo (playerSymbols [(turn + 1) % 2]);
				gameUI.gameOverUpdateText (playerNames [turn % 2]);
				gameUI.showRestartButton ();
				gameUI.gameOverShowAd ();
			}
		}

	}
}
