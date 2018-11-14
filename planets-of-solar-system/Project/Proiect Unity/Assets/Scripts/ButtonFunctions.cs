using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour {

	public void Exit()
	{
		Application.Quit();
	}

	public void StartSimulation()
	{
		SceneManager.LoadScene(1);
	}

	public void InformatiiPlanete()
	{
		SceneManager.LoadScene(2);
	}

	public void Despre()
	{
		SceneManager.LoadScene(3);
	}

	public void Introducere()
	{
		SceneManager.LoadScene(4);
	}

	public void Meniu()
	{
		SceneManager.LoadScene(0);
	}
		
}
