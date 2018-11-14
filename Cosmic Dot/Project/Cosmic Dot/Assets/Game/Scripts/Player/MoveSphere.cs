using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSphere : MonoBehaviour {

	float speed = 7.5f;
	float timeUntilRespawn = 2.5f;
	public bool loadingRespawn = false;
	public Transform spawnPoint;
	public GameObject deathParticles;
	public Slider healthBar;
	public GameObject trailGetter;
	GameObject playerTrail;
	public float xAccOffSet = 0, yAccOffSet = 0;
	public bool spawnProtection = true;

	Text speedText;

	Vector3 oldPos, pos;
	int countFrames = 0;
	float medDist = 1;

	//SOUND
	public AudioSource audioSource;
	public AudioClip deathSound, gameOver;
	float deathSoundVolume = 1f, gameOverVolume = 0.8f;


	void SetPlayerColors ()
	{
		//SPHERE COLOR
		string playerColor = PlayerPrefs.GetString ("PlayerColor", "default");
		GetComponent<Renderer>().material.color = GetComponent<GetPlayerColor> ().returnColor (playerColor);
		GetComponent<Light>().color = GetComponent<GetPlayerColor> ().returnColor (playerColor);
	}

	public void SetPlayerTrail()
	{
		playerTrail = trailGetter.GetComponent<GetPlayerTrail>().getTrail();
		playerTrail = Instantiate (playerTrail, transform.position, Quaternion.identity) as GameObject;
		playerTrail.transform.SetParent (gameObject.transform);
		playerTrail.transform.localScale = Vector3.one;
		playerTrail.transform.localRotation = Quaternion.Euler (-90, 0, 0);
		playerTrail.transform.localPosition = new Vector3 (0, 0.25f, 0);
	}

	void Start()
	{
		//CHECK IF ANY HEARTS LEFT
		loadingRespawn = true;
		if (GameObject.Find ("HeartsSys").transform.Find ("Text").GetComponent<HeartsSys> ().hearts > 0) {
			SetPlayerColors ();
			SetPlayerTrail ();
			StartCoroutine (waitUntilShowStartButton (0.5f));
			oldPos = transform.position;
			speedText = GameObject.Find ("Speed").GetComponent<Text> ();
		}
	}

	Vector3 dir;
	void MovePhone()
	{
		transform.rotation = Quaternion.Euler (0, 0, 0);

		dir = new Vector3(-Input.acceleration.y - yAccOffSet, 0, Input.acceleration.x + xAccOffSet);
		GameObject.Find ("TextDebug").GetComponent<Text> ().text = dir.ToString ();

		//NORMALIZE SPEED
		if (dir.x < -0.7f)
			dir.x = -0.7f;
		else if (dir.x > 0.7f)
			dir.x = 0.7f;

		if (dir.sqrMagnitude > 1)
			dir.Normalize ();

		dir *= Time.deltaTime;

		transform.Translate (dir * speed);
	}

	void MoveKeyboard()
	{
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (Vector3.left * speed * 0.01f);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (Vector3.right * speed * 0.01f);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.forward * speed * 0.01f);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.back * speed * 0.01f);
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Enemy" && !loadingRespawn)
		{
			healthBar.GetComponent<HealthBar> ().HealthBarChangeValue (-100);
		}
	}
		
	public void RespawningProcess()
	{
		//DISABLE THINGS
		loadingRespawn = true;
		StartCoroutine ("RespawnPlayer", timeUntilRespawn);
		GetComponent<Light> ().enabled = false;
		GetComponent<Rigidbody> ().useGravity = false;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<MeshRenderer> ().enabled = false;

		//DESTROY TRAIL AND RESPAWNING TIME
		deltaTime = 0;

		//PLAY SOUNDS
		audioSource.GetComponent<AudioSource> ().PlayOneShot (deathSound, deathSoundVolume * PlayerPrefs.GetInt ("effectVol", 1));
		audioSource.GetComponent<AudioSource> ().PlayOneShot (gameOver, gameOverVolume * PlayerPrefs.GetInt ("effectVol", 1));

		//CAMERA SHAKE
		Camera.main.GetComponent<CameraControl>().shakeTime = 0.75f;

		//CLEAR KEYS
		GameObject.Find ("KeysSystem").GetComponent<KeysSystem> ().ResetKeys ();

		//REBUILD MAP
		GameObject.Find ("PlatformManager").GetComponent<PlatformManager> ().respawnPlatform ();

		//DEATH PARTICLES
		GameObject go = Instantiate (deathParticles, transform.position, Quaternion.identity) as GameObject;
		Destroy (go, timeUntilRespawn);
	}

	IEnumerator RespawnPlayer(float t)
	{
		yield return new WaitForSeconds (t);

		//ENABLE THINGS

		//if hearts left -> enable movement
		if (GameObject.Find ("HeartsSys").transform.Find ("Text").GetComponent<HeartsSys> ().hearts > 0) {
			healthBar.GetComponent<HealthBar> ().HealthBarChangeValue (100);
			StartCoroutine (waitUntilShowStartButton (0.5f));
		}

		//SEND PLAYER TO SPAWNPOINT
		transform.position = spawnPoint.position;

		//ENABLE THE LIGHT
		GetComponent<Light> ().enabled = true;

		//ENABLE THE GRAVITY
		GetComponent<Rigidbody> ().useGravity = true;

		//RESET THE VELOCITY
		GetComponent<Rigidbody> ().velocity = Vector3.zero;

		//ENABLE THE TEXTURE
		GetComponent<MeshRenderer> ().enabled = true;
	}

	IEnumerator waitUntilShowStartButton(float time)
	{
		yield return new WaitForSeconds (time);
		GameObject.Find("StartGameButton").GetComponent<Button>().interactable = true;
		GameObject.Find ("StartGameButton").transform.GetChild (0).GetComponent<Image> ().enabled = true;
	}


	void ComputeSpeed()
	{
		countFrames++;
		pos = transform.position;
		medDist += Vector3.Distance (oldPos, pos);
		oldPos = pos;

		if (countFrames % 10 == 0)
		{
			float dist = medDist / (10 * (countFrames / 10));
			speedText.text = "Speed: " + (dist * 30).ToString("F1") + "m /s";
		}

		if (countFrames == 30) {
			countFrames = 0;
			medDist /= 30;


			if (medDist < 0.02f && !spawnProtection)
				healthBar.GetComponent<HealthBar> ().HealthBarChangeValue (-100);

			medDist = 0;
		}
	}

	Text remainingTime;
	float deltaTime = 2f;

	public void PlayerSpawnProtection()
	{
		remainingTime = GameObject.Find ("SpawnProtectionTime").GetComponent<Text> ();
		deltaTime = 2f;
		spawnProtection = true;
		StartCoroutine(countDown(0f));
	}

	IEnumerator countDown(float time)
	{
		yield return new WaitForSeconds (time);

		if (deltaTime - time > 0) {
			deltaTime -= time;
			remainingTime.text = "Spawn protection:\n <color=#FF5900FF>" + deltaTime.ToString("F2") + "</color>";
			StartCoroutine(countDown(0.1f));
		} else {
			spawnProtection = false;
			remainingTime.text = "";
		}
	}
		
	void Update () {


		if (!loadingRespawn) {
			MovePhone ();
			MoveKeyboard ();
			ComputeSpeed ();
		}

		//FALL OFF THE PLATFORM
		if (transform.position.y < -4f && !loadingRespawn) {
			healthBar.GetComponent<HealthBar> ().HealthBarChangeValue (-100);
		}
			
	}
}
