using UnityEngine;
using System.Collections;

public class GameOverWord : MonoBehaviour {

	TextMesh gameOver;
	
	void Start () {

		gameOver = gameObject.GetComponent<TextMesh>();
		string word = PlayerPrefs.GetString("mainSequence");

		for(int i = 0; i < word.Length; i++)
			if (word[i] == ' ')
				gameOver.text += '\n';
			else
				gameOver.text += word[i];
	
	}

}
