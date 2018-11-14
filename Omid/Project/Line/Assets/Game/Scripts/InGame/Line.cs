using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

	//Height
	float down = 0, up = 0;
	float yCoord = 0;

	//Rotation
	public float lineAngle = 0;

	bool normalizeVelocity = false;

	//Game panel script
	GamePanel gPanel;

	/// CONSTANTS

	//Distance between (0,0,0) and line position
	public float circleRadius = 10f;

	//Rotation speed coef
	float rotationSpeed = 0.2f;

	//Height increase coef per frame
	float heightIncreaseCoef = 0.006f;

	//Reduce the speed of the exponential function
	float heightCutCoef = 1.05f;

	//Max exponential speed
	float heightVelocityMax = 0.15f;

	//When releasing the key, slowly making a curve
	float heightReduceCoef = 0.0020f;

	//Line action interval Y (-lineActionInterval, lineActionInteval) <- while playing, the line is always between these values
	float lineActionInterval = 5.5f;

	float maxLineLength = 1.15f;

	float lengthIncreaseCoef = 0.05f;


	//DEMO MOVEMENT

	//A swith bool for up/down movement
	bool upMovementDemo = true;

	//Move the line between Y = (-yIntervalDemo, yIntervalDemo)
	float yIntervalDemo = 0.15f;


	void Start()
	{
		gPanel = GameObject.Find ("GamePanel").GetComponent<GamePanel> ();
		GetComponent<TrailRenderer> ().colorGradient = GetComponent<GetGradient>().getOmidColors (PlayerPrefs.GetString ("currentItem", "Default"));
		Instantiate (GameObject.Find ("ParticlesForLine").GetComponent<GetParticles> ().getOmidParticles (PlayerPrefs.GetString ("currentItem", "Default")), gameObject.transform).name = "Particles";
	}
		
	void NormalLine()
	{
		if (Input.GetMouseButton(0)) {

			up = heightIncreaseCoef + (float)(up)/heightCutCoef;
			if (up > heightVelocityMax)
				up = heightVelocityMax;

			down -= heightReduceCoef;
			if (down < 0)
				down = 0;

			yCoord = up - down;

		} else {

			down = heightIncreaseCoef + (float)(down)/heightCutCoef;
			if (down > heightVelocityMax)
				down = heightVelocityMax;

			up -= heightReduceCoef;
			if (up < 0)
				up = 0;

			yCoord = -down + up;
		}
	}

	void DrunkLine()
	{
		if (Input.GetMouseButton (0)) {

			down = heightIncreaseCoef + (float)(down) / heightCutCoef;
			if (down > heightVelocityMax)
				down = heightVelocityMax;

			up -= heightReduceCoef;
			if (up < 0)
				up = 0;

			yCoord = -down + up;

		} else {
			up = heightIncreaseCoef + (float)(up) / heightCutCoef;
			if (up > heightVelocityMax)
				up = heightVelocityMax;

			down -= heightReduceCoef;
			if (down < 0)
				down = 0;

			yCoord = up - down;

		}
	}

	void DemoLine()
	{
		if (upMovementDemo) {

			if (gameObject.transform.position.y >= yIntervalDemo)
				upMovementDemo = false;

			up = heightIncreaseCoef + (float)(up)/heightCutCoef;
			if (up > heightVelocityMax)
				up = heightVelocityMax;

			down -= heightReduceCoef;
			if (down < 0)
				down = 0;

			yCoord = up - down;

		} else 
		{
			if (gameObject.transform.position.y <= -yIntervalDemo)
				upMovementDemo = true;

			down = heightIncreaseCoef + (float)(down)/heightCutCoef;
			if (down > heightVelocityMax)
				down = heightVelocityMax;

			up -= heightReduceCoef;
			if (up < 0)
				up = 0;

			yCoord = -down + up;
		}

	}

	void UpdateLineRotation()
	{
		lineAngle += rotationSpeed;
		lineAngle %= 360;

		float x = circleRadius * Mathf.Cos(lineAngle * Mathf.PI/180);
		float z = circleRadius * Mathf.Sin(lineAngle * Mathf.PI/180);

		gameObject.transform.position = new Vector3(z, gameObject.transform.position.y + yCoord, x);
	}

	bool outOfBounds()
	{
		if (gameObject.transform.position.y > lineActionInterval)
			return true;
		
		if (gameObject.transform.position.y < -lineActionInterval)
			return true; 

		return false;
	}

	public void IncreaseSize()
	{
		float length = gameObject.GetComponent<TrailRenderer> ().time;
		if (length == maxLineLength)
			return;

		gameObject.GetComponent<TrailRenderer> ().time = length + lengthIncreaseCoef;
	}

	void Update () {

		if (gPanel.gameOver)
			return;

		if (outOfBounds ()) {
			gPanel.setGameOver ();
		}

		if (gPanel.touchToPlay == true) {

			if (gPanel.drunkMode == true) {

				if (normalizeVelocity == true) {
					down = 0;
					up = 0;
					normalizeVelocity = false;
				}
				DrunkLine ();
			} else {
				NormalLine ();
				normalizeVelocity = true;
			}
		} else {
			DemoLine ();
		}

		UpdateLineRotation ();
	}
}
