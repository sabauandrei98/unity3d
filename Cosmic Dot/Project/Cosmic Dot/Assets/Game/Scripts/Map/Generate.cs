using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Generate : MonoBehaviour {

	public bool GenerateMap = false;

	/// <summary>
	/// 0. normal road
	/// 1. spikes 
	/// 2. rotative normal
	/// 3. barrel
	/// 4. star
	/// </summary>

	GameObject parent;
	public GameObject[] objects;


	float map_curX, map_curY, map_curZ;

	// 1 - stanga, 2 - fata, 3 - dreapta, 4 - spate
	int road_direction; 
	float[] road_direction_x = new float[]{0,-1, 0, 1, 0};
	float[] road_direction_z = new float[]{0, 0, 1, 0,-1};

	int tiles_generated;
	int last_spike_ind;
	int last_rotative_ind;

	void Start()
	{
		map_curX = 0;
		map_curY = 0;
		map_curZ = 0;
		road_direction = 0;
		tiles_generated = 0;
		last_spike_ind = 0;
		last_rotative_ind = 0;
		GenerateMap = false;
	}

///================================================================
	void Update () {

		if (GenerateMap) {
			parent = new GameObject ();
			parent.name = "Parent";

			CreateMap ();
			GenerateMap = false;

		}
	}

///================================================================

	int RandomRoadDirection(int last_dir)
	{
		int new_dir = Random.Range (1, 4);
		while (new_dir + last_dir == 4 || last_dir == new_dir)
			new_dir = Random.Range (1, 4);

		return new_dir;
	}

	int RandomTypeOfRoad()
	{
		return Random.Range (1, 8);
	}

	void CreateMap()
	{
		int randomRoadType;

		for (int i = 1; i <= 15; i++) {
			
			randomRoadType = RandomTypeOfRoad();


			if (randomRoadType >= 1 && randomRoadType <= 3) {

				road_direction = RandomRoadDirection (road_direction);
				CreateRoad ();

			} else if (randomRoadType > 3 && randomRoadType <= 5 && tiles_generated - last_spike_ind >= 3) {
				last_spike_ind = tiles_generated;
				CreateSpikes ();

			} else if (randomRoadType > 5 && tiles_generated - last_rotative_ind >= 2) {
				last_rotative_ind = tiles_generated;
				CreateRotative ();
			}

			tiles_generated++;
		}
	}
		
///================================================================

	void CreateBarrel()
	{
		float range = 0.7f;
		float x = Random.Range (map_curX - range, map_curX + range);
		float y = map_curY + 0.25f;
		float z = Random.Range (map_curZ - range, map_curZ + range);

		GameObject obj;

		obj = Instantiate (objects [3], new Vector3 (x, y, z), Quaternion.identity) as GameObject;
		obj.transform.SetParent (parent.transform);
	}

	void CreateStar()
	{
		float range = 0.7f;
		float x = Random.Range (map_curX - range, map_curX + range);
		float y = map_curY + 0.25f;
		float z = Random.Range (map_curZ - range, map_curZ + range);

		GameObject obj;

		obj = Instantiate (objects [4], new Vector3 (x, y, z), Quaternion.identity) as GameObject;
		obj.transform.SetParent (parent.transform);
	}

	void CreateRoad()
	{
		int road_length = 2;
		float road_scale = 2.5f;
		float distance = 2.5f;

		for (int i = 1; i <= road_length; i++) {
			//Debug.Log (map_curX.ToString () + " " + map_curY.ToString () + " " + road_scale.ToString() + " " + road_direction.ToString());
			map_curX += distance * road_direction_x [road_direction];
			map_curZ += distance * road_direction_z [road_direction];
			Debug.Log (road_direction_x [road_direction].ToString () + " " + road_direction_z [road_direction].ToString ());

			int hasBarrel = Random.Range (0, 5);
			if (hasBarrel == 1)
				CreateBarrel ();
			int hasStar = Random.Range (0, 4);
			if (hasStar == 1)
				CreateStar ();
				

			GameObject obj = Instantiate (objects [0], new Vector3 (map_curX, map_curY, map_curZ), Quaternion.identity) as GameObject;
			obj.transform.localScale = new Vector3 (road_scale, road_scale/10, road_scale);
			obj.transform.SetParent (parent.transform);


		}
	}

///================================================================
	void CreateSpikes()
	{
		float spikes_scale = 2.5f;
		float distance = 2.5f;

		map_curX += distance * road_direction_x [road_direction];
		map_curZ += distance * road_direction_z [road_direction];

		GameObject obj = Instantiate (objects [1], new Vector3 (map_curX, map_curY, map_curZ), Quaternion.identity) as GameObject;
		obj.transform.localScale = new Vector3 (spikes_scale, spikes_scale/10, spikes_scale);
		obj.transform.SetParent (parent.transform);
	}
///=================================================================

	void CreateRotative()
	{
		float rotative_scale = 2.5f;
		float distance = 2.5f;

		map_curX += distance * road_direction_x [road_direction];
		map_curZ += distance * road_direction_z [road_direction];

		GameObject obj = Instantiate (objects [2], new Vector3 (map_curX, map_curY, map_curZ), Quaternion.identity) as GameObject;
		obj.transform.localScale = new Vector3 (rotative_scale, rotative_scale/10, rotative_scale);
		obj.transform.SetParent (parent.transform);
	}

}