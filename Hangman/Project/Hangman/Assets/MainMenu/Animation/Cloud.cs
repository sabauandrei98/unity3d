using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	//SET CLOUD TO MOVE BETWEEN TWO POSITIONS

	public Transform[] patrol;
	float moveSpeed = 0.16f;
	int index = 0;

	void Start () {
		//SET THE CLOUD'S X RANDOM 
		float y = patrol[0].transform.position.y;
		float x = Random.Range(-7.5f,7.5f);
		transform.position = new Vector3(x,y,0);
	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.transform.position != patrol[index].transform.position)
			gameObject.transform.position = Vector3.MoveTowards(transform.position, patrol[index].transform.position, Time.deltaTime * moveSpeed);
		else
			index ++;

		if (index > patrol.Length - 1)
			index = 0;

	}
}
