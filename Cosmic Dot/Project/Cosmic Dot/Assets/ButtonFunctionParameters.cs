using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonFunctionParameters : MonoBehaviour {

	int exitCount = 0;
	public Text tapTwiceText;

	IEnumerator disableDoubleClick(float time)
	{
		yield return new WaitForSeconds (time);
		exitCount = 0;
		tapTwiceText.text = "";
	}

	void Start()
	{
		tapTwiceText.text = "";
	}

	 
	 void Update(){
		if (Input.GetKeyUp (KeyCode.Escape)) {
			exitCount++;
				
			string sceneName = SceneManager.GetActiveScene ().name;
			if (sceneName == "mainMenu") {
				tapTwiceText.text = "TAP TWICE TO EXIT THE GAME";
			} else if (sceneName == "levelSelection")
				tapTwiceText.text = "TAP TWICE TO GO BACK TO MAIN MENU";
			else if (sceneName == "Settings")
				tapTwiceText.text = "TAP TWICE TO GO BACK TO MAIN MENU";
			else if (sceneName == "CalibrationMenu")
				tapTwiceText.text = "TAP TWICE TO GO BACK TO MAIN MENU";
			else
				tapTwiceText.text = "TAP TWICE TO GO BACK TO LEVEL SELECTION";

			StartCoroutine ("disableDoubleClick", 0.8f);
		}
		if (exitCount == 2) {

			string sceneName = SceneManager.GetActiveScene ().name;

			tapTwiceText.text = "";

			if (sceneName == "mainMenu")
				MainMenuExit ();
			else if (sceneName == "levelSelection")
				LevelSelectionMenu ();
			else if (sceneName == "Settings")
				SettingsToMainMenu ();
			else if (sceneName == "CalibrationMenu")
				CalibrationMenuToSettings ();
			else
				SceneManager.LoadScene ("levelSelection");
			
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////	

	IEnumerator CalibrationMenuToSettings(float time)
	{
		yield return new WaitForSeconds (time);

		SceneManager.LoadScene ("Settings");
	}
	IEnumerator CalibrationMenuToMainMenu(float time)
	{
		yield return new WaitForSeconds (time);

		SceneManager.LoadScene ("mainMenu");
	}

	IEnumerator CalibrationMenuNextButton(float time)
	{
		yield return new WaitForSeconds (time);

		SceneManager.LoadScene ("levelSelection");
	}

	public void CalibrationMenuToSettings(int type = 0)
	{
		if (type == 0) {
			StartCoroutine (CalibrationMenuToMainMenu (0.55f));
		} else if (type == 1) {
			StartCoroutine (CalibrationMenuToSettings (0.55f));
		} else if (type == 2) {
			StartCoroutine (CalibrationMenuNextButton (0.55f));
		}

		GameObject.Find("Panel").GetComponent<Animator>().SetBool("Selected",true);
		GameObject.Find("Title").GetComponent<Animator>().SetBool("Selected",true);
	}



	//////////////////////////////////////////////////////////////////////////////////////	

	IEnumerator SettingsToMainMenu(float time)
	{
		yield return new WaitForSeconds (time);

		SceneManager.LoadScene ("mainMenu");
	}

	public void SettingsToMainMenu()
	{
		StartCoroutine (SettingsToMainMenu(0.6f));
		GameObject.Find("SettingsContainer").GetComponent<Animator>().SetBool("MainMenu",true);
		GameObject.Find("SettingsTitle").GetComponent<Animator>().SetBool("MainMenu",true);
	}
		

	//////////////////////////////////////////////////////////////////////////////////////	

	IEnumerator LevelSelectionMenuToGame(float time)
	{
		yield return new WaitForSeconds (time);

		int level = int.Parse (EventSystem.current.currentSelectedGameObject.name);
		SceneManager.LoadScene ("intro" + EventSystem.current.currentSelectedGameObject.name);
	}

	IEnumerator LevelSelectionMenuToMainMenu(float time)
	{
		yield return new WaitForSeconds (time);

		SceneManager.LoadScene ("mainMenu");
	}

	public void LevelSelectionMenu(int type = 0)
	{
		if(exitCount == 2 || type == 1)
			StartCoroutine (LevelSelectionMenuToMainMenu(0.7f));
		else
			StartCoroutine (LevelSelectionMenuToGame(0.7f));
		
		GameObject.Find("BackImage").GetComponent<Animator>().SetBool("Selected",true);
		GameObject.Find("InfoPanel").GetComponent<Animator>().SetBool("Selected",true);
		GameObject.Find("LevelsText").GetComponent<Animator>().SetBool("Selected",true);
	}
	 
	//////////////////////////////////////////////////////////////////////////////////////	

	private IEnumerator MainMenuPlay(float time)
	{
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene ("CalibrationMenu");
	}

	public void MainMenuPlay() {

		StartCoroutine (MainMenuPlay(0.6f));
		GameObject.Find("Menu").GetComponent<Animator>().SetBool("Play",true);
		GameObject.Find("Title").GetComponent<Animator>().SetBool("HideText",true);
	}

	private IEnumerator MainMenuSettings(float time)
	{
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene ("Settings");
	}

	public void MainMenuSettings() {

		StartCoroutine (MainMenuSettings(0.6f));
		GameObject.Find("Menu").GetComponent<Animator>().SetBool("Play",true);
		GameObject.Find("Title").GetComponent<Animator>().SetBool("HideText",true);
	}
		

	private IEnumerator MainMenuExit(float time)
	{
		yield return new WaitForSeconds (time);
		Application.Quit();
	}

	public void MainMenuExit() {

		StartCoroutine (MainMenuExit(0.6f));
		GameObject.Find("Menu").GetComponent<Animator>().SetBool("Play",true);
		GameObject.Find("Title").GetComponent<Animator>().SetBool("HideText",true);
	}

	////////////////////////////////////////////////////////////////////
	// IN GAME NEXT LEVEL

	private IEnumerator NextLevel(float time)
	{
		yield return new WaitForSeconds (time);
		int nextLevel = int.Parse (SceneManager.GetActiveScene ().name) + 1;
		SceneManager.LoadScene ("intro" + nextLevel.ToString());
	}

	public void NextLevel() {

		StartCoroutine (NextLevel(0.6f));
		GameObject.Find("LevelFinish").GetComponent<Animator>().SetBool("Selected",true);
	}




	private IEnumerator LevelDoneToMainMenu(float time)
	{
		yield return new WaitForSeconds (time);
		//Level selection
		SceneManager.LoadScene ("mainMenu");
	}

	public void LevelDoneToMainMenu() {

		StartCoroutine (LevelDoneToMainMenu(0.6f));
		GameObject.Find("LevelFinish").GetComponent<Animator>().SetBool("Selected",true);

	}


	////////////////////////////////////////////////////////////////////

	bool dropDownMenuOpened = false;

	public void DropDownMenu() {


		if (dropDownMenuOpened == false) {
			dropDownMenuOpened = true;
			GameObject.Find ("InGameMenu").GetComponent<Animator> ().SetBool ("Selected", true);
		} else {
			dropDownMenuOpened = false;
			GameObject.Find ("InGameMenu").GetComponent<Animator> ().SetBool ("Selected", false);
		}
			
	}

	public void InGameShop(){
		SceneManager.LoadScene ("Shop");
	}
	public void InGameMainMenu(){
		SceneManager.LoadScene ("mainMenu");
	}
	public void InGameOptions(){
		SceneManager.LoadScene ("Settings");
	}

	public Sprite soundEffectOn, soundEffectOff;
	public void InGameSoundEffect(){
		int effect = PlayerPrefs.GetInt ("effectVol", 1);
		string sceneName = SceneManager.GetActiveScene ().name;

		if (effect == 1) {
			if(sceneName == "Settings")
				EventSystem.current.currentSelectedGameObject.transform.GetComponent<Image> ().sprite = soundEffectOff;
			else
				EventSystem.current.currentSelectedGameObject.transform.GetChild (0).GetComponent<Image> ().sprite = soundEffectOff;
			effect = 0;
		} else {
			if(sceneName == "Settings")
				EventSystem.current.currentSelectedGameObject.transform.GetComponent<Image> ().sprite = soundEffectOn;
			else
				EventSystem.current.currentSelectedGameObject.transform.GetChild (0).GetComponent<Image> ().sprite = soundEffectOn;
			effect = 1;
		}

		PlayerPrefs.SetInt ("effectVol", effect);
		PlayerPrefs.Save ();
	}

	public Sprite musicEffectOn, musicEffectOff;
	public void InGameSoundMusic(){
		int music = PlayerPrefs.GetInt ("musicVol", 1);
		string sceneName = SceneManager.GetActiveScene ().name;

		if (music == 1) {
			if(sceneName == "Settings")
				EventSystem.current.currentSelectedGameObject.transform.GetComponent<Image> ().sprite = musicEffectOff;
			else
				EventSystem.current.currentSelectedGameObject.transform.GetChild (0).GetComponent<Image> ().sprite = musicEffectOff;
			music = 0;
		} else {
			if(sceneName == "Settings")
				EventSystem.current.currentSelectedGameObject.transform.GetComponent<Image> ().sprite = musicEffectOn;
			else
				EventSystem.current.currentSelectedGameObject.transform.GetChild (0).GetComponent<Image> ().sprite = musicEffectOn;
			music = 1;
		}

		GameObject.Find ("BackgroundMusic").GetComponent<DoNotDestroyOnLoad> ().ApplySettings (music);
		PlayerPrefs.SetInt ("musicVol", music);
		PlayerPrefs.Save ();
	}

	private IEnumerator HideCalibrationText(float time)
	{
		yield return new WaitForSeconds (time);
		GameObject.Find ("CalibrationText").GetComponent<Text> ().text = "";
	}

	public void InGameCalibration()
	{
		GameObject.Find ("CalibrationText").GetComponent<Text> ().text = "Calibrated !";
		StartCoroutine (HideCalibrationText (1));
		GameObject.Find ("Player").GetComponent<MoveSphere> ().xAccOffSet = Input.acceleration.x;
		GameObject.Find ("Player").GetComponent<MoveSphere> ().yAccOffSet = -Input.acceleration.y;

	}


	public void InGameStartGame()
	{
		GameObject.Find ("Player").GetComponent<MoveSphere> ().loadingRespawn = false;
		GameObject.Find ("Player").GetComponent<MoveSphere> ().PlayerSpawnProtection();
	}

	//////////////////////////////////////////////////////////////////////////////////////	

	public void ShopToLevelSelection()
	{
		SceneManager.LoadScene ("levelSelection");
	}


}
