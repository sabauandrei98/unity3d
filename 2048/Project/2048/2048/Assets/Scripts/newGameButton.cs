using UnityEngine;
using System.Collections;

public class newGameButton : MonoBehaviour {

	GUITexture img;
	
	void Start () {
		img = GetComponent<GUITexture>();
	}
	
	void Update () {
		
		if (Input.touches.Length > 0)
			if (img.HitTest(Input.GetTouch(0).position))
		{
			if(Input.GetTouch(0).phase == TouchPhase.Ended)
				Application.LoadLevel(0);
		}
	}
}
