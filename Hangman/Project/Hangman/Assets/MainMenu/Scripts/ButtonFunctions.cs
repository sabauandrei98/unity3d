using UnityEngine;
using System.Collections;

public class ButtonFunctions : MonoBehaviour {

	public void Easy()
	{
		PlayerPrefs.SetInt("Difficulty",0);
		PlayerPrefs.Save();
		Application.LoadLevel(1);
	}

	public void Medium()
	{
		PlayerPrefs.SetInt("Difficulty",1);
		PlayerPrefs.Save();
		Application.LoadLevel(1);
	}

	public void Master()
	{
		PlayerPrefs.SetInt("Difficulty",2);
		PlayerPrefs.Save();
		Application.LoadLevel(1);
	}

	public void MainMenu()
	{
		Application.LoadLevel(0);
	}

	public void Restart()
	{
		Application.LoadLevel(1);
	}

	public void Stats()
	{
		Application.LoadLevel(3);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
