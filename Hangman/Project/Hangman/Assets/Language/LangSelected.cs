using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LangSelected : MonoBehaviour {

	// SWITCH BUTTON
	public GameObject ro,eng;
	Button roB,engB;

	// 1 - UK
	// 2 - RO
	// GET INFO AS INDEX

	public void Lang(int i)
	{
	
		if (i == 1)
		{
			PlayerPrefs.SetInt("Language",1);
			roB = ro.gameObject.GetComponent<Button>();
			engB = eng.gameObject.GetComponent<Button>();
			engB.interactable = false;
			roB.interactable = true;
		}

		if (i == 2)
		{
			PlayerPrefs.SetInt("Language",2);
			roB = ro.gameObject.GetComponent<Button>();
			engB = eng.gameObject.GetComponent<Button>();
			engB.interactable = true;
			roB.interactable = false;
		}

		if(i == 2)
			Debug.Log ("Limba Romana" + PlayerPrefs.GetInt("Language"));
		else
			Debug.Log ("Limba Engleza" + PlayerPrefs.GetInt("Language"));

		PlayerPrefs.Save();
	}
}
