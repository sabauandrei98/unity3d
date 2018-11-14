using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkModeManager : MonoBehaviour {

	//Sound manager script
	SoundManager soundManager;

	//UI Manager script
	UIManager uiManager;

	GameObject line;

	public GameObject DrunkomidText;

	void Start()
	{
		soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		uiManager = GameObject.Find ("UIManager").GetComponent<UIManager> ();
		line = GameObject.Find ("Line");
	}
		
	public void SwitchDrunkMode(bool mode)
	{
		if (mode == true) {
			DrunkomidText.SetActive (true);
			soundManager.PlayBubbleSound ();
			uiManager.SetBackground ("drunk");
			line.GetComponent<TrailRenderer> ().colorGradient = line.GetComponent<GetGradient>().getOmidColors ("Drunkomid");
			Destroy (line.transform.Find ("Particles").gameObject);
			Instantiate (GameObject.Find ("ParticlesForLine").GetComponent<GetParticles> ().getOmidParticles ("Drunkomid"), line.transform).name = "Particles";
		} else {
			DrunkomidText.SetActive (false);
			uiManager.SetBackground ("normal");
			line.GetComponent<TrailRenderer> ().colorGradient = line.GetComponent<GetGradient>().getOmidColors (PlayerPrefs.GetString ("currentItem", "Default"));
			Destroy (line.transform.Find ("Particles").gameObject);
			Instantiate (GameObject.Find ("ParticlesForLine").GetComponent<GetParticles> ().getOmidParticles (PlayerPrefs.GetString ("currentItem", "Default")), line.transform).name = "Particles";
		}
	}
}
