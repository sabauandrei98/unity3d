using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPWarningHide : MonoBehaviour {


	bool once = true;
	float time = 2f;

	// Update is called once per frame
	void Update () {
		if (this.isActiveAndEnabled && once) {
			once = false;
			StartCoroutine ("HideWarning", 1.5f);
		}
	}

	IEnumerator HideWarning(float t)
	{
		yield return new WaitForSeconds (t);

		if (time - t > 0) {
			time -= t;
			StartCoroutine ("HideWarning", 0.01f);
			gameObject.transform.localScale -= new Vector3 (0.01f, 0.01f, 0);
		} else {
			gameObject.SetActive (false);
			gameObject.transform.localScale = new Vector3 (1, 1, 1);
			once = true;
			time = 2f;
		}

	}
}
