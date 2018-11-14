using UnityEngine;
using System.Collections;

public class GoUp : MonoBehaviour {

	float lifeTime = 1.5f;

	void Start () {
		Destroy(gameObject,lifeTime);
	}

	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(0.3f, 7f, 9.97f), Time.deltaTime * 0.85f);
	}

}
