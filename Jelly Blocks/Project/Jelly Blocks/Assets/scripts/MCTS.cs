using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCTS : MonoBehaviour {

	bool[,] board = new bool[9,9];
	Board gameBoard;

	//string[] coordsFound = new string[10005];
	int[] coordsFreq = new int[100]; 
	int coordsCnt = 0;

	void Start()
	{
		gameBoard = GameObject.Find ("Scripts").GetComponent<Board> ();
	}

	void getMap()
	{
		for (int i = 1; i <= 8; i++)
			for (int j = 1; j <= 8; j++)
				if (gameBoard.board [i, j].GetComponent<Button> ().IsInteractable ())
					board [i, j] = false;
				else
					board [i, j] = true;
	}

	void resetCoords()
	{
		//for (int i = 1; i <= coordsCnt; i++)
		//	coordsFound [i] = "";
		//coordsCnt = 0;

		for (int i = 1; i <= 99; i++)
			coordsFreq [i] = 0;
		coordsCnt = 0;
	}

	public Vector2 getPoint()
	{
		resetCoords ();
		getMap ();
		searchForMove (board);

		//for (int i = 1; i <= coordsCnt; i++) {
		//	Debug.Log (coordsFound [i]);
		//}

		Vector2 point = Vector2.zero;
		int best = 0;
		for (int i = 1; i <= 99; i++) {
			if (coordsFreq [i] > best ) {
				best = coordsFreq [i];
				point.x = i / 10;
				point.y = i % 10;
			}
		}
		Debug.Log (coordsCnt.ToString ());
		return point;
	}

	bool isMapFull(bool[,] mBoard)
	{
		for (int i = 1; i <= 8; i++)
			for (int j = 1; j <= 8; j++)
				if (mBoard [i, j] == false)
					return false;
		return true;
	}

	bool[,] markArea(int x, int y, bool[,] mBoard)
	{
		for (int i = x - 1; i <= x + 1; i++)
			for (int j = y - 1; j <= y + 1; j++)
				if (gameBoard.isOnBoard (i, j) && mBoard [i, j] == false)
					mBoard [i, j] = true;

		return mBoard;
	}

	bool[,] copyBool(bool[,] mBoard)
	{
		bool[,] aux = new bool[9, 9];
		for (int i = 1; i <= 8; i++)
			for (int j = 1; j <= 8; j++)
				aux [i, j] = mBoard [i, j];

		return aux;
	}

		
	string aux_coords = "";

	void searchForMove(bool[,] sBoard, string coords = "")
	{
		
		if (coordsCnt > 12000)
			return;

		//AI MAKES A MOVE
		if (!isMapFull (sBoard)) {
			
			for (int i = 1; i <= 8; i++)
				for (int j = 1; j <= 8; j++)
					if (sBoard [i, j] == false) {

						bool[,] auxBoard = copyBool (sBoard);
						markArea (i, j, auxBoard);

						if (coords == "")
							aux_coords = i.ToString () + " " + j.ToString ();

						//IF THE BOARD IS FULL, THIS IS A GOOD COMBINATION
						if (isMapFull (auxBoard)) {
							coordsCnt++;
							//coordsFound [coordsCnt] = aux_coords;
							coordsFreq [i * 10 + j]++;

							return;
						} else {

							for (int ii = i + 1; ii <= 8; ii++)
								for (int jj = j + 1; jj <= 8; jj++)
									if (auxBoard [ii, jj] == false) {
										
										bool[,] auxBoard2 = copyBool (auxBoard);
										markArea (ii, jj, auxBoard2);
										searchForMove (auxBoard2, aux_coords);

									}
						}
					}
		} else {
			int i = coords [0] - '0';
			int j = coords [2] - '0';
			coordsFreq [i * 10 + j]--;
		}
	}
}
