using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextShake : MonoBehaviour {

	public float range;
	public float time;
	// Use this for initialization
	void Start () {
		gameObject.transform.DOMove (new Vector3 (transform.position.x, transform.position.y + range, transform.position.z), time).SetLoops (-1, LoopType.Yoyo);
	}

}
