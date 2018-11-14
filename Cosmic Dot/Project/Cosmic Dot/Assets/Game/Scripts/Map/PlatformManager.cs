using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

	public GameObject platform;
	GameObject copyPlatformInstance;
	GameObject copyOfPlatform;

	// Use this for initialization
	void Start () {
		copyOfPlatform = platform;
		copyPlatformInstance = Instantiate (copyOfPlatform, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		copyPlatformInstance.SetActive (true);
	}
	
	// Update is called once per frame
	public void respawnPlatform () {
		Destroy (copyPlatformInstance);
		copyPlatformInstance = Instantiate (platform, new Vector3 (0, 0, 0), Quaternion.identity)as GameObject;
		copyPlatformInstance.SetActive (true);
	}
}
