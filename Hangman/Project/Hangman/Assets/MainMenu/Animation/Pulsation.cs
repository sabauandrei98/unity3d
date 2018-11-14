using UnityEngine;
using System.Collections;

public class Pulsation : MonoBehaviour {

	public Sprite[] sprites;
	private SpriteRenderer spriteRenderer;

	float maxScale = 0.22f;
	float minScale = 0.10f;
	float curScale = 0;
	float scaleCoeficient = 0.001f;
	float delay = 0.001f;

	float curRot = 0;
	float rotCoeficint = 0.25f;
	int rotDir;

	bool increase = true;


	IEnumerator Puls(float delay)
	{
		yield return new WaitForSeconds(delay);

		if (curScale < maxScale && increase == true)
			curScale += scaleCoeficient;
		else
			increase = false;
		
		if (curScale > minScale && increase == false)
			curScale -= scaleCoeficient;
		else
			increase = true;

		StartCoroutine("Puls",delay);

		if (rotDir == 1)
			curRot += rotCoeficint;
		else
			curRot -= rotCoeficint;

		curRot %= 360;

		gameObject.transform.localScale = new Vector3(curScale, curScale, 0);
		gameObject.transform.eulerAngles = new Vector3(0, 0, curRot);


	}


	void Start () {

		//GET A RANDOM LETTER
		int spriteIndex = Random.Range(0,sprites.Length - 1);

		//GET THE SPRITE COMPONENT
		spriteRenderer = gameObject.GetComponent<Renderer>() as SpriteRenderer;
		spriteRenderer.sprite = sprites[spriteIndex];

		//GET A RANDOM SCALE FOR LETTER, RANDOM ROTATION, RANDOM DIRECTION
		curScale = Random.Range(minScale,maxScale);
		rotDir = Random.Range (0,2);
		curRot = Random.Range(0,360);

		StartCoroutine("Puls",delay);

	}
	

}
