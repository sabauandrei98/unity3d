using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Mouse : MonoBehaviour {

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;

	private Information from,to;
	public TextMesh score,best,countDown, goingUp;
	public GameObject two,pattern;

	int[,] map = new int[5,5];
	GameObject[,] table = new GameObject[5,5];
	Vector3[,] coord = new Vector3[5,5];

	float spawnDelay = 0.15f;
	int newScore = 0, OldScore;
	float TimeToRestart = 5;

	void RestartGame()
	{
		pattern.transform.position = new Vector3(0,1,-0.5f);
		countDown.transform.position = new Vector3(0,0,-0.75f);
		StartCoroutine("CountDown",0f);
	}

	IEnumerator CountDown(float delay)
	{
		TimeToRestart -= delay;
		yield return new WaitForSeconds(delay);

		countDown.text = "Restart in ...\n" + "       " + TimeToRestart.ToString("F1");
		if (TimeToRestart > 0.1)
			StartCoroutine("CountDown", Time.deltaTime);
		else
			Application.LoadLevel(0);

	}

	void Spawn()
	{
		int End_Game = 1;
		Queue<Vector3> pos = new Queue<Vector3>();

		for(int i = 1; i <= 4; i++)
			for(int j = 1; j <= 4; j++)
			if (map[i,j] == 0)
			{
				pos.Enqueue(new Vector3(i, j, 0));
				End_Game = 0;
			}

		if (End_Game == 0)
		{	
			int rand = Random.Range(0, pos.Count);
			while(rand > 0)
			{
				rand--;
				pos.Dequeue();
			}
			int x = (int)pos.Peek().x;
			int y = (int)pos.Peek().y;

			table[x,y] = Instantiate(two, coord[x,y], Quaternion.identity) as GameObject;
			map[x,y] = 1;

		}
	}

	IEnumerator Spawn_Random(float delay)
	{
		yield return new WaitForSeconds(delay);
		Spawn ();
	}


	void UpdateScore(int index)
	{
		newScore += 1 << index;

		if (newScore == 1)
			newScore = 0;

		if (newScore > OldScore)
		{
			OldScore = newScore;
			PlayerPrefs.SetInt("Best",newScore);
			PlayerPrefs.Save();
		}
		score.text = "" + newScore;
		best.text = "" + OldScore;
	}

	void ScoreGoesUp(int score)
	{
		goingUp.text = "+" + score;
		Instantiate(goingUp, new Vector3(0.3f, 4.6f, 9.97f), Quaternion.identity);
	}



    void Start()
    {
		Initial_Config();
		StartCoroutine("Spawn_Random", spawnDelay);
		StartCoroutine("Spawn_Random", spawnDelay);
    }

    void Update () {
        Swipe();
	}

	int verify(int i, int j)
	{
		if (i > 0 && i <= 4 && j > 0 && j <= 4)
			return 1;
		return 0;
	}

	void EndGame()
	{
		bool End = true;

		for(int i = 1; i<=4; i++)
			for(int j = 1; j<=4; j++)
				if (map[i,j] == 1)
			{
				if (verify(i-1,j) == 1)
					if (map[i-1,j] == 1)
					{
						if (table[i-1,j].GetComponent<Information>().index == table[i,j].GetComponent<Information>().index)
							End = false;
					}
					else
						End = false;
				if (verify(i+1,j) == 1)
					if (map[i+1,j] == 1)
					{
						if (table[i+1,j].GetComponent<Information>().index == table[i,j].GetComponent<Information>().index)
							End = false;
					}
					else
						End = false;

				if (verify(i,j-1) == 1)
					if (map[i,j-1] == 1)
					{
						if (table[i,j-1].GetComponent<Information>().index == table[i,j].GetComponent<Information>().index)
							End = false;
					}
					else
						End = false;

				if (verify(i,j+1) == 1)
					if (map[i,j+1] == 1)
					{
						if (table[i,j+1].GetComponent<Information>().index == table[i,j].GetComponent<Information>().index)
							End = false;
					}
					else
						End = false;
			}

		if (End == true)
			RestartGame();

	}

    public void Swipe()
    {

		//Get the first position
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
		//Get the second positon
        if (Input.GetMouseButtonUp(0))
        {
            
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();


			int[,] used = new int[5,5];

			EndGame();

			int scoreThisTurn = 0;

			//swipe up

            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {

				bool Spawned = false;
				
				for(int col = 1; col <= 4; col++)
				{
					for(int line = 2; line <= 4; line++)
					{
						bool anyNumber = false;
						
						if (map[line,col] == 1)
						{
							GameObject gFrom;
							from = table[line,col].GetComponent<Information>();
							gFrom = table[line,col];
							
							for(int j = line - 1; j >= 1 && anyNumber == false; j--)
								if (map[j,col] == 1)
							{
								
								anyNumber = true; 
								to = table[j,col].GetComponent<Information>();

								if (from.index == to.index && used[j,col] == 0)
								{
									to.NextNumber();
									map[line,col] = 0;
									from.move_to(coord[j,col],1);
									Spawned = true;
									UpdateScore(from.index + 2);
									scoreThisTurn += 1 << (from.index + 2);
									used[j,col] = 1;
								}
								else
								{
									map[line,col] = 0;
									map[j + 1, col] = 1;
									table[j + 1, col] = gFrom;
									from.move_to(coord[j + 1, col],0);
									
									if (line != j + 1)
										Spawned = true;
								}
							}
							
							if (anyNumber == false)
							{
								map[line,col] = 0;
								map[1,col] = 1;
								table[1,col] = gFrom;
								from.move_to(coord[1,col],0);
								Spawned = true;
							}
							
						}
					}
				}
				if (Spawned == true)
					StartCoroutine("Spawn_Random", spawnDelay);

            }
           
			//swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {

				bool Spawned = false;

				for(int line = 1; line <= 4; line++)
				{
					for(int col = 2; col <= 4; col++)
					{
						bool anyNumber = false;

						if (map[line,col] == 1)
						{
							GameObject gFrom;
							from = table[line,col].GetComponent<Information>();
							gFrom = table[line,col];

								for(int j = col - 1; j >= 1 && anyNumber == false; j--)
									if (map[line,j] == 1)
								{

									anyNumber = true; 
									to = table[line,j].GetComponent<Information>();

									if (from.index == to.index && used[line,j] == 0)
									{
										to.NextNumber();
										map[line,col] = 0;
										from.move_to(coord[line,j],1);
										Spawned = true;
										UpdateScore(from.index + 2);
										scoreThisTurn += 1 << (from.index + 2);
										used[line,j] = 1;
								}
									else
									{
										map[line,col] = 0;
										map[line,j + 1] = 1;
										table[line,j + 1] = gFrom;
										from.move_to(coord[line,j + 1],0);

										if (col != j + 1)
											Spawned = true;
									}
								}

							if (anyNumber == false)
							{
								map[line,col] = 0;
								map[line,1] = 1;
								table[line,1] = gFrom;
								from.move_to(coord[line,1],0);
								Spawned = true;
							}

						}
					}
				}
				if (Spawned == true)
					StartCoroutine("Spawn_Random", spawnDelay);

            }

			//swipe down
			if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
			{
				
				bool Spawned = false;
				
				for(int col = 4; col >=  1; col--)
				{	
					for(int line = 3; line >= 1; line--)
					{
						bool anyNumber = false;
						
						if (map[line,col] == 1)
						{
							GameObject gFrom;
							from = table[line,col].GetComponent<Information>();
							gFrom = table[line,col];
							
							for(int j = line + 1; j <= 4 && anyNumber == false; j++)
								if (map[j,col] == 1)
							{
								
								anyNumber = true; 
								to = table[j,col].GetComponent<Information>();

								if (from.index == to.index && used[j,col] == 0)
								{
									to.NextNumber();
									map[line,col] = 0;
									from.move_to(coord[j,col],1);
									Spawned = true;
									UpdateScore(from.index + 2);
									scoreThisTurn += 1 << (from.index + 2);
									used[j,col] = 1;
								}
								else
								{
									map[line,col] = 0;
									map[j - 1, col] = 1;
									table[j - 1, col] = gFrom;
									from.move_to(coord[j - 1, col],0);
									
									if (line != j - 1)
										Spawned = true;
								}
							}
							
							if (anyNumber == false)
							{
								map[line,col] = 0;
								map[4,col] = 1;
								table[4,col] = gFrom;
								from.move_to(coord[4,col],0);
								Spawned = true;
							}
							
						}
					}
				}
				if (Spawned == true)
					StartCoroutine("Spawn_Random", spawnDelay);
			}

			//swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
				bool Spawned = false;
				
				for(int line = 1; line <= 4; line++)
				{
					for(int col = 3; col >=  1; col--)
					{
						bool anyNumber = false;
						
						if (map[line,col] == 1)
						{
							GameObject gFrom;
							from = table[line,col].GetComponent<Information>();
							gFrom = table[line,col];
							
							for(int j = col + 1; j <= 4 && anyNumber == false; j++)
								if (map[line,j] == 1)
							{
								
								anyNumber = true; 
								to = table[line,j].GetComponent<Information>();

								if (from.index == to.index && used[line,j] == 0)
								{
									to.NextNumber();
									map[line,col] = 0;
									from.move_to(coord[line,j],1);
									Spawned = true;
									UpdateScore(from.index + 2);
									scoreThisTurn += 1 << (from.index + 2);
									used[line,j] = 1;
								}
								else
								{
									map[line,col] = 0;
									map[line,j - 1] = 1;
									table[line,j - 1] = gFrom;
									from.move_to(coord[line,j - 1],0);
									
									if (col != j - 1)
										Spawned = true;
								}
							}
							
							if (anyNumber == false)
							{
								map[line,col] = 0;
								map[line,4] = 1;
								table[line,4] = gFrom;
								from.move_to(coord[line,4],0);
								Spawned = true;
							}
										
						}
					}
				}
				if (Spawned == true)
					StartCoroutine("Spawn_Random", spawnDelay); 
            }


			if (scoreThisTurn != 0)
				ScoreGoesUp(scoreThisTurn);
        }
    }
	


	void Initial_Config()
	{
		OldScore = PlayerPrefs.GetInt("Best");
		UpdateScore(0);

		for(int i = 1; i <= 4; i++)
			for(int j = 1; j <= 4; j++)
				map[i,j] = 0;
				
		coord[1,1] = new Vector3(-2.19f, 2.19f, 10);   coord[2,1] = new Vector3(-2.19f, 0.74f, 10);
		coord[1,2] = new Vector3(-0.74f, 2.19f, 10);   coord[2,2] = new Vector3(-0.74f, 0.74f, 10);
		coord[1,3] = new Vector3(0.74f , 2.19f, 10);   coord[2,3] = new Vector3(0.74f , 0.74f, 10);
		coord[1,4] = new Vector3(2.19f , 2.19f, 10);   coord[2,4] = new Vector3(2.19f , 0.74f, 10);
		
		coord[3,1] = new Vector3(-2.19f,-0.74f, 10);   coord[4,1] = new Vector3(-2.19f, -2.19f, 10);
		coord[3,2] = new Vector3(-0.74f,-0.74f, 10);   coord[4,2] = new Vector3(-0.74f, -2.19f, 10);
		coord[3,3] = new Vector3(0.74f, -0.74f, 10);   coord[4,3] = new Vector3( 0.74f, -2.19f, 10);
		coord[3,4] = new Vector3(2.19f, -0.74f, 10);   coord[4,4] = new Vector3( 2.19f, -2.19f, 10);
		
	}
}
