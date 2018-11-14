using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class DrawLine : MonoBehaviour {

	Vector3 pointA, pointC, pointB, noPoint, mousePos;
	GameObject lineAC, lineCB, helpLine, dot, lastDot;
	LineRenderer lr;

	float maxLineWidth = 2.15f;
	float AC_Width = 0.1f;
	float CB_Width = 0.1f;
	float dotWidth = 0.15f;
	float lastDotWidth = 0.175f;
	float randAngle, remainingLife, myAngle;

	Color AB_Color_start = new Color (0, 1, 0.42f, 1);
	Color AB_Color_end = new Color(0, 1, 0.42f, 1);
	Color CB_Color_start = new Color(0, 1, 0.42f, 1);
	Color CB_Color_end = new Color (0, 1, 0.42f, 1);
	//Color helpCB_Color_start = new Color(1, 0.68f, 0, 1);
	//Color helpCB_Color_end = new Color (0, 0.58f, 1, 1);
	Color dotColor = new Color (0, 1, 1, 1);
	Color lastDotColor = new Color(0, 1, 0.42f, 1);
	Color statusTextColor = new Color (0, 1, 1, 1);


	int rounds;
	public GameObject restartGame;
	public GameObject canvas;
	public AudioClip gameover, pop;
	//public GameObject adManager;

	private InterstitialAd interstitial;
	int timesPlayed = 0;

	/// CREATE A RANDOM LINE ON THE SCREEN
	void randomLine()
	{
		int r = Random.Range (0, 2);

		float x;
		if (r == 1)
			x = 3f;
		else
			x = -3f;
		
		float y = Random.Range (-10f, 10f);

		pointA = new Vector3 (x, y, 0);
		pointB = noPoint;
		pointC = noPoint;
	}


	void StartRound()
	{
		if (randAngle != -1) {
			remainingLife -= abs (myAngle - randAngle);
		}

		if (remainingLife > 0) {

			if (randAngle != -1)
				rounds++;

			int best = PlayerPrefs.GetInt ("best", 0);
			if (best < rounds)
				PlayerPrefs.SetInt ("best", rounds);

			Destroy (lineAC);
			Destroy (lineCB);
			pointB = noPoint;

			randAngle = Random.Range (0, 360);
			UpdateText ();

			CreateLineBetweenPoints(pointA, pointC, AB_Color_start, AB_Color_end, AC_Width, 1);
		} else {
			//GAME OVER
			UpdateText ();
			gameObject.GetComponent<AudioSource> ().PlayOneShot (gameover);
			if (timesPlayed % 2 == 0)
			if (interstitial.IsLoaded ()) {
				Debug.Log ("play ad");
				interstitial.Show ();
			}

			restartGame.gameObject.SetActive (true);
		}
	}

	private void RequestInterstitial()
	{
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-8267006366479170/7622557245";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().AddExtra("npa", "1").Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}
		
	void Start(){

		timesPlayed = PlayerPrefs.GetInt ("timesPlayed", 0);
		timesPlayed++;

		if (timesPlayed % 2 == 0) {
			RequestInterstitial ();
			Debug.Log ("req ad" + timesPlayed.ToString());
		}

		PlayerPrefs.SetInt ("timesPlayed", timesPlayed);
		PlayerPrefs.Save ();


		noPoint = new Vector3 (0, 0, 0);
		rounds = 0;
		remainingLife = 50;
		randAngle = -1;
		randomLine ();
		CreateLineBetweenPoints (pointC, pointC, dotColor, dotColor, dotWidth, 4);
		StartRound ();
		gameObject.GetComponent<DrawScript> ().offsetAngle360 (pointA, pointC);
	}
		
	public void CreateLineBetweenPoints(Vector3 start, Vector3 end, Color col_start, Color col_end, float width, int id)
	{
		//OBJECT CREATION

		if (id == 1) {
			//AC
			lineAC = new GameObject ();
			lineAC.transform.position = noPoint;
			lineAC.name = "lineAC";
			lineAC.AddComponent<LineRenderer> ();
			lr = lineAC.GetComponent<LineRenderer> ();

		} else if (id == 2) {
			//CB
			lineCB = new GameObject ();
			lineCB.transform.position = noPoint;
			lineCB.name = "lineCB";
			lineCB.AddComponent<LineRenderer> ();
			lr = lineCB.GetComponent<LineRenderer> ();
		} else if (id == 3) {
			// helpLine
			helpLine = new GameObject ();
			helpLine.transform.position = noPoint;
			helpLine.name = "helpLine";
			helpLine.AddComponent<LineRenderer> ();
			lr = helpLine.GetComponent<LineRenderer> ();
		} else if (id == 4) {
			// Dot
			dot = new GameObject ();
			dot.transform.position = noPoint;
			dot.name = "Dot";
			dot.AddComponent<LineRenderer> ();
			lr = dot.GetComponent<LineRenderer> ();
		} else {
			// last dot
			lastDot = new GameObject ();
			lastDot.transform.position = noPoint;
			lastDot.name = "lastDot";
			lastDot.AddComponent<LineRenderer> ();
			lr = lastDot.GetComponent<LineRenderer> ();
		}

		/// LINE CREATION
		if (id == 4 || id == 5)
			lr.material = new Material (Shader.Find ("Sprites/Default"));
		else
			lr.material = new Material (Shader.Find ("Particles/Alpha Blended Premultiply"));

		lr.startColor = col_start;
		lr.endColor = col_end;
	
		lr.startWidth = width;
		lr.endWidth = width;
		lr.numCapVertices = 10;

		float dist = Vector3.Distance (start, end);

		if (id != 1) {
			end = Vector3.Lerp (start, end, maxLineWidth / dist);
		}
		else
		{
			start = Vector3.Lerp (end, start, maxLineWidth / dist);
		}

		if (id == 2) {
			pointB = end;
		}

		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
	}
		
		
	Vector3 world(Vector3 point){
		return Camera.main.ScreenToWorldPoint(point);
	}
		

	float Angle360()
	{
		Vector2 p1, p2, o;

		p1 = new Vector2 (pointA.x, pointA.y);
		p2 = new Vector2 (pointB.x, pointB.y);
		o = new Vector2 (pointC.x, pointC.y);

		Vector2 v1, v2;
		if (o == default(Vector2)) {
			v1 = p1.normalized;
			v2 = p2.normalized;
		} else {
			v1 = (p1 - o).normalized;
			v2 = (p2 - o).normalized;
		}
		float angle = Vector2.Angle (v1, v2);

		return Mathf.Sign (Vector3.Cross (v1, v2).z) < 0 ? (angle) : (360 - angle) % 360;
	}


	void CreateStatus()
	{
		GameObject status = new GameObject ();
		status.name = "statusAngle";
		status.transform.SetParent (canvas.transform);
	

		float x, y;

		if (pointC.x > pointB.x)
			x = pointB.x - 0.25f;
		else
			x = pointB.x + 0.25f;

		if (pointC.y > pointB.y)
			y = pointB.y - 0.25f;
		else
			y = pointB.y + 0.25f;

		status.transform.position = new Vector3 (x, y, 0);

		status.AddComponent<Text> ();
		Text statusText = status.GetComponent<Text>();
		statusText.transform.localScale = new Vector3 (1, 1, 1);
		statusText.color = statusTextColor;
		statusText.font = (Font)Resources.Load("Fonts/monof55");
		statusText.fontSize = 25;
		statusText.text = myAngle.ToString ("F1") + "°";
		statusText.alignment = TextAnchor.MiddleCenter;


		//set rect corners
		RectTransform t = status.transform as RectTransform;
		RectTransform pt = status.transform.parent as RectTransform;

		if (t == null || pt == null) return;

		Vector2 newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
			t.anchorMin.y + t.offsetMin.y / pt.rect.height);
		Vector2 newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
			t.anchorMax.y + t.offsetMax.y / pt.rect.height);

		t.anchorMin = newAnchorsMin;
		t.anchorMax = newAnchorsMax;
		t.offsetMin = t.offsetMax = new Vector2(0, 0);
	}
		
		
	void Update () {

		if (remainingLife > 0) {

			//STORE START POS
			if (pointB == noPoint) {
				if (Input.GetMouseButtonDown (0))
					pointB = world (Input.mousePosition);
			} else
			//DRAW HELP LINE
			if (Vector3.Distance (pointC, pointB) > 0) {
				Destroy (helpLine);
				pointB = world (Input.mousePosition);
				pointB.z = 0;
					gameObject.GetComponent<DrawScript> ().CreatePoints (pointC, 0.2f, 0.2f, Angle360());
				CreateLineBetweenPoints (pointC, pointB, CB_Color_start, CB_Color_end, CB_Width, 3);
			}

			//STORE B POS
			if ((Input.GetMouseButtonUp (0) && pointC != Input.mousePosition)) {
				gameObject.GetComponent<AudioSource> ().PlayOneShot (pop);
				Destroy (helpLine);
				Destroy (lineCB);
				gameObject.GetComponent<DrawScript> ().ResetSegments ();
				pointB = world (Input.mousePosition);
				pointB.z = 0;
				CreateLineBetweenPoints (pointC, pointB, CB_Color_start, CB_Color_end, CB_Width, 2);
				CreateLineBetweenPoints (pointB, pointB, lastDotColor, lastDotColor, lastDotWidth, 5);

				myAngle = Angle360 ();
				CreateStatus ();
				StartRound ();

			} else if (Input.GetMouseButtonUp (0))
				pointB = noPoint;
		}

	}

	float abs(float a)
	{
		if (a < 0)
			return -a;
		return a;
	}
		
	public Text textAngle, textLife, textMyAngle, round, best;

	void UpdateText()
	{
		textAngle.text = "Angle: " + randAngle.ToString () + "°";
		if (remainingLife < 0)
			
			textLife.text = "<color=red>Life: " + remainingLife.ToString ("F2") + "</color>";
		else
			textLife.text = "Life: " + remainingLife.ToString ("F2");
		
		textMyAngle.text = "My angle: " + myAngle.ToString ("F2") + "°";
		best.text = "Best: " + PlayerPrefs.GetInt ("best", 0).ToString();
		round.text = "Round: " + rounds.ToString ();

	}
}
