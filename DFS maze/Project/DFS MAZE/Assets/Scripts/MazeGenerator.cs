using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour {

	//Public variables
	public int Height = 10;
	public int Width = 10;
	private int [,] maze;
	private int [,] seen;

	private int startX, startY;
	private int endX, endY;
	
	public GameObject WallGo;
	public GameObject RedGo;
	public GameObject YellowGo;
	Queue<int> coord = new Queue<int>();
	private bool Finish = false;

	void Start () {

		Height ++;
		Width ++;
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt("count",0);
		maze = GenerateMaze(Height,Width);


		seen = Init_Seen(Height, Width);


		Create_End_Point(endX, endY);
		Show_Maze_Map();


		Final_Coords();
		Color_DFS(startX,startY);
		StartCoroutine("WaitTime");


		//Screen 
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

	}


	void Create_End_Point(int x, int y)
	{
		Vector3 ObjectPosition = new Vector3(x, 0, y); 
		
		// Create the Object which will have this positon
		GameObject wall = Instantiate(YellowGo) as GameObject;
		
		// Move the Object to position
		if (wall != null)
			wall.transform.position = ObjectPosition;
		
	}




	private int[,] Init_Seen(int x, int y)
	{
		int[,] seen = new int[Height, Width];
		for(int i = 0; i < Height; i++)
			for(int j = 0; j < Width; j++)
				seen[i,j] = 0;

		return seen;
	}
	
	void Final_Coords()
	{
		bool ok = false;
		for(int i = 1; i <= 4; i++)
			for(int j = i; j <= 4 && ok == false; j++)
				if (maze[i,j] == 0)
			{
				startX = i;
				startY = j;
				ok = true;
			}

	}

	void Color_DFS(int x, int y)
	{
		if (x == endX && y == endY)
			Finish = true;

		if (Finish == false)
		{
			seen[x,y] = 1;

			PlayerPrefs.SetInt("count", PlayerPrefs.GetInt("count") + 1);

			coord.Enqueue(x);
			coord.Enqueue(y);

			int [] direction = new int[] { 1, 2, 3, 4};
			Rand(direction);

			for(int i = 0; i < direction.Length; i++)
			{
				switch(direction[i])
				{
				case 1:
					if (maze[x+1,y] == 0 && seen[x+1,y] == 0)
						Color_DFS(x+1,y);
					break;

				case 2:
					if (maze[x-1,y] == 0 && seen[x-1,y] == 0)
						Color_DFS(x-1,y);
					break;

				case 3:
					if (maze[x,y+1] == 0 && seen[x,y+1] == 0)
						Color_DFS(x,y+1);
					break;

				case 4:
					if (maze[x,y-1] == 0 && seen[x,y-1] == 0)
						Color_DFS(x,y-1);
					break;

				}
			}
		}

	}

	void Color_DFS_Show(int i, int j)
		 {
			
			int[] directions = new int[] { 3, 3, 3, 3};

			Vector3 ObjectPosition = new Vector3(i, 0, j); 
			
			// Create the Object which will have this positon
			GameObject wall = Instantiate(RedGo) as GameObject;

			// Move the Object to position
			if (wall != null)
			{
					wall.transform.position = ObjectPosition;

					for(int q = 0; q < directions.Length; q++)
					{
					switch(directions[q])
						{
						case 1:
							wall.GetComponent<Renderer>().material.color = Color.green;
							break;

						case 2:
							wall.GetComponent<Renderer>().material.color = Color.red;
							break;

						case 3:
							wall.GetComponent<Renderer>().material.color = Color.blue;
							break;

						case 4:
							wall.GetComponent<Renderer>().material.color = Color.yellow;
							break;
						}
					}
			}
		}

	

	//create the map
	private int[,] GenerateMaze(int Height, int Width)
	{
		int[,] maze = new int[Height+1, Width+1];

		for(int i = 0; i <= Height ; i++)
			for(int j = 0; j <= Width ; j++)
				maze[i,j] = 1;

		int posX = Random.Range(1,Height-1); 
		int posY = Random.Range(1,Width-1); 

		maze[posX, posY] = 0;


		MazeDigger(maze, posX, posY);

		return maze;
	}

	//DFS for the Maze map
	private void MazeDigger(int[,] maze, int x, int y)
	{
		// 1 - N
		// 2 - S
		// 3 - E
		// 4 - W

		int[] directions = new int[] { 1, 2, 3, 4};
		Rand(directions);


		for(int i = 0; i < directions.Length ; i++)
		{
			switch(directions[i])
			{
				//NORTH  i > 0
				case 1:
					if (x - 2 > 0 && maze[x-2,y] == 1 )
						{
							maze[x-1,y] = 0;
							maze[x-2,y] = 0;
							MazeDigger(maze, x-2, y);

						endX = x-2;
						endY = y;
						}
					break;

				//SOUTH i < Height
				case 2:
					if (x + 2 < Height-1  && maze[x+2,y] == 1 )
						{
							maze[x+1,y] = 0;
							maze[x+2,y] = 0;
							MazeDigger(maze, x+2, y);

						endX = x+2;
						endY = y;
						}
					break;

				//EAST j < Width
				case 3:
				if (y + 2 < Width-1 && maze[x,y+2] == 1 )
						{
							maze[x,y+1] = 0;
							maze[x,y+2] = 0;
							MazeDigger(maze, x, y+2);

						endX = x;
						endY = y+2;
						}
					break;

				//WEST j > 1
				case 4:
				if (y - 2 > 0 && maze[x,y-2] == 1 )
						{
							maze[x,y-1] = 0;
							maze[x,y-2] = 0;
							MazeDigger(maze, x, y-2);

						endX = x;
						endY = y-2;
						}
					break;
			}
		}
	}

	//Shuffle a vector 
	private int[] Rand(int[] directions)
	{
		
		for(int i = 0; i < directions.Length; i++)
		{
			int r = Random.Range(0,directions.Length);
			
			int aux = directions[i];
			directions[i] = directions[r];
			directions[r] = aux;
		}
		
		return directions;
	}


	void Show_Maze_Map()
	{
		for(int i = 0; i < Height ; i++)
			for(int j = 0; j < Width ; j++)
		{
			if (maze[i, j] == 1)
			{
				// Create the position
				Vector3 ObjectPosition = new Vector3(i, 0, j); 
				
				// Create the Object which will have this positon
				GameObject wall = Instantiate(WallGo) as GameObject;
				
				// Move the Object to position
				if (wall != null)
					wall.transform.position = ObjectPosition;
				
			}
		}
	}



	IEnumerator WaitTime() {
	
		yield return new WaitForSeconds(0);

		int cnt = PlayerPrefs.GetInt("count");
		PlayerPrefs.SetInt("count", PlayerPrefs.GetInt("count") - 1);

		Color_DFS_Show(coord.Dequeue(), coord.Dequeue());

		if (cnt > 1)
			StartCoroutine("WaitTime");
		else
		{
			Application.LoadLevel(0);
		}
			
	}
	

	
}



