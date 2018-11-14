using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	Vector3 finalPos;
	float moveSpeed;

	void Start()
	{
		finalPos = new Vector3(transform.position.x, -6f, 9.5f);
		moveSpeed = Random.Range(0.25f, 1f);
	}

	// Update is called once per frame
	void Update () {
		if (transform.position != finalPos)
			transform.position = Vector3.MoveTowards(transform.position, finalPos, Time.deltaTime * moveSpeed);
			else
				Destroy(gameObject);
	}
}
