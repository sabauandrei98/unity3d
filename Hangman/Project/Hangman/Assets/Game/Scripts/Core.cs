using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Core : MonoBehaviour {

	string mainSequence;
	int difficulty = 0;

	//THESE ARE THE BUTTONS FROM THE BOTTOM
	public GameObject[] buttons;

	//TABLE LETTERS AS GAME OBJECT
	GameObject[,] tableLetters = new GameObject[10,26];

	//FINAL WORDS AND MY WORDS
	char[,] words = new char[10,26], tableWords = new char[10,26] ;

	
///######################################################
/// CREATE GAPS BY : 
///	- NUMBER OF WORDS
/// - WORD LENGTH
///######################################################

	public	GameObject letter;
	float letter_dist;
	float width, height;

	//IF THE LENGHT IS DIVISIBLE BY 2
	void Create_Gaps_0(string subWord, int line)
	{
		int r = subWord.Length / 2 - 1;
		int mid = subWord.Length / 2;

		// ABCDEF
		// 123456
		tableLetters[line,mid+1] = Instantiate(letter, new Vector3( letter_dist/2 + width,height,0), Quaternion.identity) as GameObject;
		tableLetters[line,mid]   = Instantiate(letter, new Vector3(-letter_dist/2 + width,height,0), Quaternion.identity) as GameObject;
		  
		for(int i = 1; r != 0; i++){	
			tableLetters[line,mid+i+1] = Instantiate(letter, new Vector3( i * letter_dist + letter_dist/2 + width,height,0), Quaternion.identity) as GameObject;
			tableLetters[line,r]     = Instantiate(letter, new Vector3(-i * letter_dist - letter_dist/2 + width,height,0), Quaternion.identity) as GameObject;
			r--;
		}
	}

	//IF THE LENGHT IS NOT DIVISIBLE BY 2
	void Create_Gaps_1(string subWord, int line)
	{
		int r = subWord.Length / 2;
		int mid = r;

		// ABCDEFG
		// 1234567
		tableLetters[line,mid+1] = Instantiate(letter, new Vector3(width,height,0), Quaternion.identity) as GameObject;
		
		for(int i = 1; r != 0; i++){

			tableLetters[line,mid+i+1] = Instantiate(letter, new Vector3( i * letter_dist + width,height,0), Quaternion.identity) as GameObject;
			tableLetters[line,r]       = Instantiate(letter, new Vector3(-i * letter_dist + width,height,0), Quaternion.identity) as GameObject;
			r--;
		}
	}
	
///######################################################
/// CUT THE SEQUENCE
/// - FIND THE NUMBER OF WORDS
///######################################################

	float dist_between_lines;
	int line;
	int[] word_size = new int[10];

	void Cut_Word(string mainSequence)
	{
		height -= dist_between_lines;
		string subString = "";

		for(int i = 0; i < mainSequence.Length; i++)
		{
			if (mainSequence[i] != ' ' && i != mainSequence.Length - 1)
			{
				subString += mainSequence[i];
				words[line+1, subString.Length] = mainSequence[i];
			}
			else
			{
				//NUMBER OF LINES == NUMBER OF WORDS
				line ++;

				if (i == mainSequence.Length - 1)
					subString += mainSequence[i];

				//THE DISTANCE BETWEEN WORDS
				height += dist_between_lines;

				//CREATE GAP BY LENGTH % 2
				if (subString.Length % 2 == 0)
					Create_Gaps_0(subString,line);
				else
					Create_Gaps_1(subString,line);

				//IN CASE OF EASY DIFFICULTY WE STORE FIRST AND THE LAST LETTER
				if (difficulty == 0)
				{
					tableWords[line,1] = subString[0];
					tableWords[line,subString.Length] = subString[subString.Length-1];
				}

				//STORE THE LAST LETTER OF THE WORD
				words[line,subString.Length] = subString[subString.Length-1];

				//COUNT THE WORD LENGTH FOR EACH LINE
				word_size[line] = subString.Length;

				subString = "";
			}
		}
	}

///######################################################
/// FIRST AND LAST LETTERS FOR THE BEGINNERS
///###################################################### 
	private SpriteRenderer spriteRenderer;
	public Sprite[] alphabetLetters;
	void Initial_Letters()
	{
		for(int i = 1; i <= line; i++)
		{
			//SET THE FIRST LETTER
			spriteRenderer = tableLetters[i,1].GetComponent<Renderer>() as SpriteRenderer;
			spriteRenderer.sprite = alphabetLetters[tableWords[i,1] - 'A'];

			//SET THE SECOND LETTER
			spriteRenderer = tableLetters[i,word_size[i]].GetComponent<Renderer>() as SpriteRenderer;
			spriteRenderer.sprite = alphabetLetters[tableWords[i,word_size[i]] - 'A'];	
		}
	}
	
///######################################################
/// TABLE ASPECT
///  
/// max number of WORDS & SIZE: 
// EASY - (1-24) 1 CUVANT
// MEDIUM (1-18) 3 CUVINTE
// MASTER (1-20) 8 CUVINTE  
///######################################################

	int numberOfWords = 0, longestWord;
	public GameObject hangman;
	float lettersScale = 0;

	void Get_Info(string mainWord)
	{
		int lastPos = 0 ;
		for(int i = 0; i < mainWord.Length; i++)
			if (mainWord[i] == ' ' || i == mainWord.Length - 1)
			{
				numberOfWords++;

				if (longestWord < i - lastPos)
					longestWord = i - lastPos;
		
				lastPos = i+1;
			}
	}
	
	void TableAspect()
	{
		Get_Info(mainSequence);

		int comp;
		if (longestWord > numberOfWords + 4)
			comp = longestWord;
		else
			comp = numberOfWords + 4;

		float scale = 0;

		//TABLE SET FOR EASY
		if (difficulty == 0)
		{
			height = -1;
			width = 0;
			scale = 0.6f - comp * 0.018f;
			Instantiate(hangman, new Vector3(0, 2.75f, 0), Quaternion.identity);
			lettersScale = scale;
		}

		//TABLE SET FOR MEDIUM
		if (difficulty == 1)
		{	
			height = numberOfWords * 0.8f;

			if (longestWord > 6)
					width = longestWord * 0.12f;
			else 
				if(longestWord > 3)
					width = longestWord * 0.3f;
				else
					width = longestWord * 0.9f;

			if (comp > 13)
				comp = 13;

			scale = 0.6f - comp * 0.03f;
			Instantiate(hangman, new Vector3(-6, 1.25f, 0), Quaternion.identity);
			lettersScale = scale;
		}

		//TABLE SET FOR MASTER
		if (difficulty == 2)
		{
			height = 3.5f - numberOfWords * 0.07f;
			
			if (longestWord > 6)
				width = longestWord * 0.1f;
			else 
				if(longestWord > 3)
				width = longestWord * 0.4f;
			else
				width = longestWord * 0.9f;
			
			if (comp > 13)
				comp = 13;
			
			scale = 0.7f - comp * 0.04f;
			Instantiate(hangman, new Vector3(-6, 1.25f, 0), Quaternion.identity);
			lettersScale = scale;
		}

		letter.transform.localScale = new Vector3(scale, scale, 0);
		letter_dist = scale * 2.8f;
		dist_between_lines = -scale * 2.8f;
	}

///######################################################
///  YOU WIN !!!! 
///######################################################

	public GameObject gWin, nextButton;

	void Win()
	{
		gWin.SetActive(true);
		nextButton.SetActive(true);

		// GAMESPLAYED ++
		// GAMES WON ++
		int gamesPlayed = PlayerPrefs.GetInt("Played",0);
		int won = PlayerPrefs.GetInt("Won",0);
		PlayerPrefs.SetInt("Played", gamesPlayed + 1);
		PlayerPrefs.SetInt("Won", won + 1);
		PlayerPrefs.Save();
	}

///######################################################
/// GAME OVER 
///######################################################

	void GameOver()
	{
		Application.LoadLevel(2);

		// GAMESPLAYED ++
		// GAMES LOST ++
		int gamesPlayed = PlayerPrefs.GetInt("Played",0);
		int lost = PlayerPrefs.GetInt("Lost",0);
		PlayerPrefs.SetInt("Played", gamesPlayed + 1);
		PlayerPrefs.SetInt("Lost", lost + 1);
		PlayerPrefs.Save();
	}
		
///######################################################
///  CHECK IF ALL LETTERS ARE PLACED 
///######################################################
	
	int IsTheGameDone()
	{
		int remaining = 0;
		for(int i = 1; i <= line; i++)
			for(int j = 1; j <= word_size[i]; j++)
				if (tableWords[i,j] == 0)
					remaining ++;

		//IF NO SPACE IS EMPTY IN MY WORD
		//THEN I WIN !
		if (remaining == 0)
			return 1;

		return 0;
	}

///######################################################
/// UPDATE TABLE 
///######################################################

	int health = 8;
	GameObject hMan, teleP;
	private Button bt;
	private Hangman hm;
	public GameObject UniversalLetter;
	private UniversalLett ul;

	public void UpdateTable(char pressedLetter)
	{
		bool wrongLetter = true;

		for(int i = 1; i <= line; i++)
			for(int j = 1; j <= word_size[i]; j++)
				//IF LETTER IN MAIN WORD
				//AND IT'S NOT IN MYWORD YET
				if (words[i,j] == pressedLetter && tableWords[i,j] != pressedLetter)
				{
					//MARK THAT THE LETTER IS OK
					wrongLetter = false;

					//STORE ON MY TABLE THE LETTER
					tableWords[i,j] = pressedLetter;

					//GET THE SPRITE OF MYTABLE
					spriteRenderer = tableLetters[i,j].GetComponent<Renderer>() as SpriteRenderer;	
					spriteRenderer.sprite = alphabetLetters[pressedLetter - 'A'];

					//CREATE THE LETTER WHICH WILL MOVE TO THAT PLACE
					teleP = Instantiate(UniversalLetter, buttons[pressedLetter - 'A'].transform.position, Quaternion.identity) as GameObject;

					//GET THE SPRITE OF THE CREATED LETTER
					spriteRenderer = teleP.GetComponent<Renderer>() as SpriteRenderer;
					spriteRenderer.sprite = alphabetLetters[pressedLetter - 'A'];

					//GET THE SCRIPT OF THE CREATED LETTER
					ul = teleP.GetComponent<UniversalLett>();
					
					//SET CREATED LETTER TO MOVE TO POSITION
					ul.Move(tableLetters[i,j].transform.position,lettersScale);
				}

		//CHECK IF THERE ARE NO WORDS TO BE PLACED
		if (IsTheGameDone() == 1)
			Win();

		//IF THE WORD PRESSED IS NOT IN THE WORD
		if (wrongLetter == true)
		{
			health--;

			//GET THE HANGMAN
			hMan = GameObject.FindGameObjectWithTag("Hangman");
			hm = hMan.GetComponent<Hangman>();

			//SET THE NEXT SPRITE
			hm.nextSprite();
		}

		if (health == 0)
			GameOver();

		//GET THE BUTTON OF THE PRESSED LETTER
		bt = buttons[pressedLetter - 'A'].GetComponent<Button>();

		//SET IT INTERACTABLE
		bt.interactable = false;
	}
	
///######################################################
/// INITIALIZATION
///######################################################

	private Words_Data words_db;
	GameObject db;

	void Start () {

		//ACCESS THE WORDS DATABASE
		db = GameObject.FindGameObjectWithTag("words_db");
		words_db = db.GetComponent<Words_Data>();

		//GET THE DIFFICULTY
		difficulty = PlayerPrefs.GetInt("Difficulty",0);

		//GET THE WORD FROM WORDS DATABASE
		mainSequence = words_db.getWord(difficulty);

		//STORE THE MAINSEQUENCE
		PlayerPrefs.SetString("mainSequence",mainSequence);

		//SET THE WORD TO LOOK AWESOME
		TableAspect();

		//CUT THE WORD TO CREATE THE TABLE
		Cut_Word(mainSequence);

		//IN CASE OF EASY DIFFICULTY WE SHOW FIRST AND LAST LETTERS
		if (difficulty == 0)
			Initial_Letters();
	}

}
