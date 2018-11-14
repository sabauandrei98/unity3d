using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonFunctions : MonoBehaviour {

	public void LevelLoader(string scene)
	{
		StartCoroutine (LoadAsynchronously (scene));
	}

	IEnumerator LoadAsynchronously(string scene)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync (scene);
		while (!operation.isDone) {
			//Debug.Log (operation.progress);
			yield return null;
		}
	}

	public void ButtonPlayAnimation()
	{
		GameObject go = EventSystem.current.currentSelectedGameObject.gameObject;
		go.GetComponent<Animator> ().SetBool ("PlayAnim", true);
	}

	public void SettingsPanelShow()
	{
		GameObject.Find ("SettingsPanel").GetComponent<Animator> ().SetBool ("PopUp", true);
		GameObject.Find ("SettingsPanel").GetComponent<Animator> ().SetBool ("Hide", false);
	}

	public void SettingsPanelHide()
	{
		GameObject.Find ("SettingsPanel").GetComponent<Animator> ().SetBool ("Hide", true);
		GameObject.Find ("SettingsPanel").GetComponent<Animator> ().SetBool ("PopUp", false);
	}
		

	public void OnePlayer()
	{
		GameObject.Find ("Scripts").GetComponent<Board> ().gamePlayers = 1;
		GameObject.Find ("SelectPlayersPanel").GetComponent<Animator> ().SetBool ("Hide", true);
	}

	public void TwoPlayers()
	{
		GameObject.Find ("Scripts").GetComponent<Board> ().gamePlayers = 2;
		GameObject.Find ("SelectPlayersPanel").GetComponent<Animator> ().SetBool ("Hide", true);
	}

	public void ExitGame()
	{
		Application.Quit ();
	}


	void Update()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
