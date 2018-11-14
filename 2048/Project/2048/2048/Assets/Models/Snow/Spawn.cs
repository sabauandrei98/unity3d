using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	// Use this for initialization

	public GameObject[] snow;
	GameObject flake;
	
	void Start () {
		StartCoroutine("Snow", 0f);
	}

	IEnumerator Snow(float delay)
	{
		yield return new WaitForSeconds(delay);

		float newTime = Random.Range(2f, 4f);
		float snowSize = Random.Range(0.08f, 0.18f);
		int type = Random.Range(0,2);
		float x = Random.Range(-3f, 3f);
		float y = 7f;
		float z = 9.5f;

		flake = Instantiate(snow[type], new Vector3(x, y, z), Quaternion.identity) as GameObject;
		flake.transform.localScale = new Vector3(snowSize, snowSize, snowSize);
		StartCoroutine("Snow", newTime);
	}
	

}
