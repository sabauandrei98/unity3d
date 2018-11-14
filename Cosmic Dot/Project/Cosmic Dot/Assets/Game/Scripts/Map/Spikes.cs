using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Spikes : MonoBehaviour {

	public Transform[] spike;
	bool meshEnabled = true;

	IEnumerator startLoop(float t)
	{
		yield return new WaitForSeconds (t);

		float rTime = Random.Range (1f, 1.5f);

		for (int i = 0; i < spike.Length; i++) {
			Vector3 trans = spike [i].transform.position;

			spike [i].DOMove (new Vector3 (trans.x, trans.y - 0.5f, trans.z), rTime).SetLoops(-1, LoopType.Yoyo);
			spike [i].DOScale (new Vector3 (0, 0, 0), rTime).SetLoops (-1, LoopType.Yoyo);
		}
	}
	void Update()
	{
		if (meshEnabled) {
			if (spike [0].transform.position.y < 0) {
				for (int i = 0; i < spike.Length; i++)
					spike [i].GetComponent<MeshRenderer> ().enabled = false;

				meshEnabled = false;
			}
		}
		else
			if (spike [0].transform.position.y > 0) {
				for (int i = 0; i < spike.Length; i++)
					spike [i].GetComponent<MeshRenderer> ().enabled = true;

				meshEnabled = true;
			}
	}

	void Start () {
		float startTime = Random.Range (0f, 2f);
		StartCoroutine ("startLoop", startTime);
	}

}
