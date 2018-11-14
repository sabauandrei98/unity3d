using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPlayerCalibrationMove : MonoBehaviour {

	float speed = 4.5f;
	public float xAccOffSet = 0, yAccOffSet = 0;


	public void Calibrate()
	{
		xAccOffSet = Input.acceleration.x;
		yAccOffSet = Input.acceleration.y;
		PlayerPrefs.SetFloat ("xAccOffSet", xAccOffSet);
		PlayerPrefs.SetFloat ("yAccOffSet", yAccOffSet);
		PlayerPrefs.Save ();
	}

	Vector3 dir;
	void MovePhone()
	{
		transform.rotation = Quaternion.Euler (0, 0, 0);

		dir = new Vector3(-Input.acceleration.y - yAccOffSet, 0, Input.acceleration.x + xAccOffSet);
		GameObject.Find ("CalibrationXY").GetComponent<Text> ().text = "X:" + xAccOffSet.ToString ("F2") + "\nY:" + yAccOffSet.ToString ("F2");

		if (dir.sqrMagnitude > 1)
			dir.Normalize ();

		dir *= Time.deltaTime;

		transform.Translate (dir * speed);
	}
		
	void Update () {
		MovePhone ();
	}
}
