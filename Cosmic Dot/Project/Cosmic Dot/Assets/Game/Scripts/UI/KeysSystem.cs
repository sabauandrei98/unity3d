using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KeysSystem : MonoBehaviour {

	public Sprite squareYellow;
	public Sprite squareEmpty;
	public Image[] squares;
	public int squareIndex = -1;

	public void AddKeys()
	{
		squareIndex++;
		if (squareIndex > 4)
			squareIndex = 4;
		squares [squareIndex].GetComponent<Image> ().sprite = squareYellow;
	}
	public void ResetKeys()
	{
		squareIndex = -1;

		for(int i = 0; i < 5; i++)
			squares [i].GetComponent<Image> ().sprite = squareEmpty;
	}
}
