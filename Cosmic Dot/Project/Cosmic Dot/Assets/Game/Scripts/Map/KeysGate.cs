using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysGate : MonoBehaviour {

	public GameObject portalParticle;
	public AudioClip portalOpen, portalClose;
	float portalOpenSound = 1f;
	float portalCloseSound = 1f;
	bool done = false;

	IEnumerator ResetText(float t)
	{
		yield return new WaitForSeconds (t);
		transform.GetChild (0).gameObject.GetComponent<TextMesh> ().text = "";
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			Debug.Log ("Portal Check");

			if (GameObject.Find ("KeysSystem").GetComponent<KeysSystem> ().squareIndex == 4 && done == false) {
				GameObject.Find ("KeysSystem").GetComponent<KeysSystem> ().ResetKeys ();
				gameObject.transform.parent.GetComponent<MeshRenderer> ().enabled = false;
				gameObject.transform.parent.GetComponent<BoxCollider> ().enabled = false;
				portalParticle.SetActive (true);
				transform.GetChild (0).gameObject.GetComponent<TextMesh> ().text = "Woohoo!";
				StartCoroutine (ResetText (3));
				gameObject.GetComponent<AudioSource> ().PlayOneShot (portalOpen, portalOpenSound * PlayerPrefs.GetInt ("effectVol", 1));
				done = true;
			} else {
				gameObject.GetComponent<AudioSource> ().PlayOneShot (portalClose, portalCloseSound * PlayerPrefs.GetInt ("effectVol", 1));
				transform.GetChild (0).gameObject.GetComponent<TextMesh> ().text = "You need more \nkeys to pass!";
				StartCoroutine (ResetText (3));
			}
		}
	}
}
