using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonate : MonoBehaviour {

	public float power;
	public float radius;
	public float upforce;
	public ForceMode forceMode;


	public GameObject explosionParticles;
	public GameObject smoke;
	GameObject smokePref, explosionPref, audioSource;

	public AudioClip tss, boom;
	float tssVolume = 0.8f, boomVolume = 0.8f;

	Collision player;

	void Start()
	{
		audioSource = GameObject.Find ("GameAudioEffects").gameObject;
	}

	IEnumerator LightIntensity(float t)
	{
		yield return new WaitForSeconds (t);

		gameObject.transform.GetChild (0).gameObject.GetComponent<Light> ().range = 7;
		gameObject.transform.GetChild (0).gameObject.GetComponent<Light> ().intensity = 20;
	}
		

	IEnumerator Explosion(float t)
	{
		yield return new WaitForSeconds (t);

		audioSource.GetComponent<AudioSource> ().PlayOneShot (boom, boomVolume * PlayerPrefs.GetInt ("effectVol", 1));
		explosionPref = Instantiate (explosionParticles, new Vector3 (transform.position.x + 0.25f, transform.position.y + 0.75f, transform.position.z + 0.25f), Quaternion.identity) as GameObject;
		Destroy (smokePref, 0);
		Destroy (explosionPref, 3f);
		player.gameObject.GetComponent<Rigidbody> ().AddExplosionForce (power, transform.position, radius, upforce, forceMode);
		Destroy (this.gameObject, 0);

		//Shake camera
		Camera.main.GetComponent<CameraControl>().shakeTime = 0.75f;
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			player = col;
			audioSource.GetComponent<AudioSource> ().PlayOneShot (tss, tssVolume * PlayerPrefs.GetInt ("effectVol", 1));
			smokePref = Instantiate (smoke, new Vector3 (transform.position.x + 0.25f, transform.position.y + 0.05f, transform.position.z + 0.25f), Quaternion.identity) as GameObject;
			StartCoroutine ("Explosion", 2f);
			StartCoroutine ("LightIntensity", 1.95f);
		}
	}
}
