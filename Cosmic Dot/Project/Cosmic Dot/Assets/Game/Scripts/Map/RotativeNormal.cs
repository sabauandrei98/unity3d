using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotativeNormal : MonoBehaviour {

	public Material yoyo, incremenental;

	// Use this for initialization
	void Start () {
		int type = PlayerPrefs.GetInt ("rotativeType", 0);

		if (type == 1) {
			gameObject.transform.DORotate (new Vector3 (0, 360, 0), 5, RotateMode.FastBeyond360).SetLoops (-1, LoopType.Yoyo);
			gameObject.GetComponent<Renderer> ().material = yoyo;
			type = 0;
		} else {
			gameObject.transform.DORotate (new Vector3 (0, 360, 0), 3, RotateMode.FastBeyond360).SetLoops (-1, LoopType.Incremental);
			gameObject.GetComponent<Renderer> ().material = incremenental;
			type = 1;
		}

		PlayerPrefs.SetInt ("rotativeType", type);
	}
}
