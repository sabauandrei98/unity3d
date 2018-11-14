using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Expandor : MonoBehaviour {

	public Transform obj;

	IEnumerator startLoop(float t)
	{
		yield return new WaitForSeconds (t);

		float rTime = Random.Range (1f, 1.5f);

		obj.DOScale (new Vector3 (1, 1, 1), rTime).SetLoops (-1, LoopType.Yoyo);
	}

	void Start () {
		float startTime = Random.Range (0f, 2f);
		StartCoroutine ("startLoop", startTime);
	}
}