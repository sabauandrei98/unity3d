using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceWallSize : MonoBehaviour {

	float sizeReductionCoef = 0.08f;

	public void startSizeReduction()
	{
		//Reduce the size for each wall
		StartCoroutine (reduceSize (0, gameObject.transform.GetChild (0).gameObject));
		StartCoroutine (reduceSize (0, gameObject.transform.GetChild (1).gameObject));

		//Destroy them after reducing the size
		Destroy (gameObject.gameObject, 3);
	}

	//Reduce the size of the walls when going through
	IEnumerator reduceSize(float time, GameObject go)
	{
		yield return new WaitForSeconds (time);

		bool canReduce = true;
		Vector3 pos;

		if (go.name == "Up")
			pos = go.GetComponent<LineRenderer> ().GetPosition (1);
		else
			pos = go.GetComponent<LineRenderer> ().GetPosition (0);

		if (go.name == "Up" && pos.y + sizeReductionCoef < 6f) {
			pos.y += sizeReductionCoef;
			go.GetComponent<LineRenderer> ().SetPosition (1, pos);

		} else if (go.name == "Down" && pos.y - sizeReductionCoef > -6f) {
			pos.y -= sizeReductionCoef;
			go.GetComponent<LineRenderer> ().SetPosition (0, pos);
		} else {
			canReduce = false;
		}

		if (canReduce == true)
			StartCoroutine (reduceSize (0.01f, go));
	}
}
