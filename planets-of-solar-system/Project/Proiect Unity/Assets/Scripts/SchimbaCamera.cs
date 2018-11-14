using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchimbaCamera : MonoBehaviour {

	public Vector3[] cameraPositions;
	public Vector3[] cameraOrientation;
	public float[] cameraSize;
	float speed = 30f;

	int index = 0;
	int maxIndex;

	void Start()
	{
		maxIndex = cameraPositions.Length;
	}

	IEnumerator nextPosition(float t)
	{
		Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraPositions[index], speed * Time.deltaTime);
		yield return new WaitForSeconds(t);
		if(Camera.main.transform.position != cameraOrientation[index] && Camera.main.orthographic == true)
			StartCoroutine("nextPosition", 0.001f);
		
	}

	public void Schimba()
	{
		Camera.main.orthographic = true;
		Camera.main.transform.eulerAngles = new Vector3(90, 0, 0);

		if (index + 1 == maxIndex)
			index = 0;
		else
			index++;
		 
		StartCoroutine("nextPosition", 0f);

		Camera.main.transform.eulerAngles = cameraOrientation[index];
		Camera.main.orthographicSize = cameraSize[index];
	}
}
