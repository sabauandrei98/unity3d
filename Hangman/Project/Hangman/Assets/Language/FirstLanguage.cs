using UnityEngine;
using System.Collections;

public class FirstLanguage : MonoBehaviour {
	
	// 1 - uk
	// 2 - ro

	public GameObject langSel;
	private LangSelected ls;

	//CREATE AN INSTANCE OF LANG
	//IF NO LANG SAVED, SET ROMANIAN AS MAIN

	void Start () {
		ls = langSel.GetComponent<LangSelected>();
		ls.Lang(PlayerPrefs.GetInt("Language",2));
	}
}
