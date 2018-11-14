using UnityEngine;
using System.Collections;

public class FromLetterToWord : MonoBehaviour {

	private Core game;
	GameObject coreObj;

	public void Send(string letter)
	{
		//GET THE LETTER AND SEND IT TO CORE TO BE PROCESSED
		coreObj = GameObject.FindGameObjectWithTag("Core");
		game = coreObj.GetComponent<Core>();
		game.UpdateTable(letter[0]);
	}
}
