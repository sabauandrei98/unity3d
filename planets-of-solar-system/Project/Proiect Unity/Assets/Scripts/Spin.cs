using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	float speed = 0.2f;

	// Update is called once per frame
	void Update () {
		float y = (transform.eulerAngles.y + speed) % 360;
		transform.eulerAngles = new Vector3(0, y, 0);
	}
}
