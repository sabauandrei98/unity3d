using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObstacleHard : MonoBehaviour {

	//Angles
	float currentAngle;
	float lastAngle = 0; 

	//Line script
	Line lineScript;

	//Game pannel
	GamePanel gPanel;

	///CONSTANTS

	//Angles between wall A and wall B
	float anglesBetweenWalls = 15;

	//Distance in angles between Line angle and Wall angle
	float anglesBetweenLineAndWall = 30;

	//Distance between pipes abs(up - down)
	float distanceBetweenPipes = 1f;

	//Line action interval Y (-lineActionInterval, lineActionInteval) <- while playing, the line is always between these values
	float lineActionInterval = 5.5f;

	//Wall action interval
	float wallActionInteval = 5.75f;

	float minDistanceBetweenPipesHeight = 1.5f;
	float maxDistanceBetweenPipesHeight = 4f;

	//Wall prefab
	public GameObject wall;

	float lastPipeHeight = 0f;


	// Use this for initialization
	void Start () {
		lineScript = GameObject.FindWithTag ("line").GetComponent<Line> ();
		gPanel = GameObject.Find ("GamePanel").GetComponent<GamePanel> ();

	}

	float absoluteValue(float a)
	{
		if (a < 0)
			return -a;
		return a;
	}

	float GetWallHeight()
	{
		float upperBound = lineActionInterval;
		float lowerBound = -lineActionInterval + distanceBetweenPipes;

		float randomPipeY = Random.Range (lowerBound, upperBound);

		while (absoluteValue (lastPipeHeight - randomPipeY) < minDistanceBetweenPipesHeight || absoluteValue (lastPipeHeight - randomPipeY) > maxDistanceBetweenPipesHeight) {
			randomPipeY = Random.Range (lowerBound, upperBound);
		}
			
		lastPipeHeight = randomPipeY;

		return randomPipeY;
	}

	void ModifyWall(GameObject wallInstance)
	{
		Vector3 wallInstancePos = wallInstance.transform.position;

		float randomPipeY = GetWallHeight ();

		
		float pipeStartY = randomPipeY;
		float pipeEndY = randomPipeY - distanceBetweenPipes;

		//LINE RENDERER
		LineRenderer upperWall = wallInstance.transform.GetChild (0).GetComponent<LineRenderer> ();
		LineRenderer lowerWall = wallInstance.transform.GetChild (1).GetComponent<LineRenderer> ();

		upperWall.SetPosition (0, new Vector3 (wallInstancePos.x, wallActionInteval, wallInstancePos.z));
		upperWall.SetPosition (1, new Vector3 (wallInstancePos.x, pipeStartY, wallInstancePos.z));

		lowerWall.SetPosition (0, new Vector3 (wallInstancePos.x, pipeEndY, wallInstancePos.z));
		lowerWall.SetPosition (1, new Vector3 (wallInstancePos.x, -wallActionInteval, wallInstancePos.z));

		//LINE COLLIDER
		wallInstance.transform.GetChild (2).transform.position = new Vector3 (upperWall.transform.position.x, 0, upperWall.transform.position.z);
		wallInstance.transform.GetChild (3).transform.position = new Vector3 (lowerWall.transform.position.x, 0, lowerWall.transform.position.z);
		wallInstance.transform.GetChild (4).transform.position = new Vector3 (lowerWall.transform.position.x, 0, lowerWall.transform.position.z);

		BoxCollider upperWallCol = wallInstance.transform.GetChild (2).GetComponent<BoxCollider> ();
		BoxCollider lowerWallCol = wallInstance.transform.GetChild (3).GetComponent<BoxCollider> ();
		BoxCollider spaceWallCol = wallInstance.transform.GetChild (4).GetComponent<BoxCollider> ();

		upperWallCol.center = new Vector3 (0, (upperWall.GetPosition (0).y + upperWall.GetPosition (1).y) / 2, 0);
		lowerWallCol.center = new Vector3 (0, (lowerWall.GetPosition (0).y + lowerWall.GetPosition (1).y) / 2, 0);
		spaceWallCol.center = new Vector3 (0, (lowerWall.GetPosition (0).y + upperWall.GetPosition (1).y) / 2, 0);

		upperWallCol.size = new Vector3 (0.2f, absoluteValue (upperWall.GetPosition (0).y - upperWall.GetPosition (1).y), 0.05f);
		lowerWallCol.size = new Vector3 (0.2f, absoluteValue (lowerWall.GetPosition (0).y - lowerWall.GetPosition (1).y), 0.05f);
		spaceWallCol.size = new Vector3 (0.1f, absoluteValue (lowerWall.GetPosition (0).y - upperWall.GetPosition (1).y) - 0.15f, 0.02f);
	}

	void InstantiateWall()
	{
		float wallAngle = currentAngle + anglesBetweenLineAndWall;
		wallAngle %= 360;

		float x = lineScript.circleRadius * Mathf.Cos(wallAngle * Mathf.PI/180);
		float z = lineScript.circleRadius * Mathf.Sin(wallAngle * Mathf.PI/180);

		GameObject wallInstance = Instantiate (wall, new Vector3 (z, 0, x), Quaternion.identity) as GameObject;
		ModifyWall (wallInstance);
	}

	void ReduceDistanceBetweenPipes()
	{
		if (distanceBetweenPipes == 0.6f)
			return;
		distanceBetweenPipes -= 0.03f;
	}

	void CheckWallAvailability()
	{
		currentAngle = lineScript.lineAngle;

		//A normalization for 360 modulo
		if (lastAngle > currentAngle) {
			lastAngle = lastAngle - 360;
		}

		if (absoluteValue (currentAngle - lastAngle) > anglesBetweenWalls) {
			lastAngle = currentAngle;
			ReduceDistanceBetweenPipes ();
			InstantiateWall ();

		} else
			return;
	}

	void Update () {

		if (gPanel.gameOver)
			return;

		if (gPanel.touchToPlay == true)
			CheckWallAvailability ();
		else
			//Avoid greating a wall right after starting the game
			lastAngle = lineScript.lineAngle;
	}
}
