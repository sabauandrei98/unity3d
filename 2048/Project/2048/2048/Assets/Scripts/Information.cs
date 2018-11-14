using UnityEngine;
using System.Collections;

public class Information : MonoBehaviour {
	
	public int index;
	public Sprite[] sprites;
	private SpriteRenderer spriteRenderer;
	
	float currentScale = 0.95f;
	float finalScale = 1.3f;

	Vector3 finPos;
	int type;
	
	void SetFirstSprite()
	{
		index = 0;
		spriteRenderer = this.gameObject.GetComponent<Renderer>() as SpriteRenderer;
		spriteRenderer.sprite = sprites[index];
	}

	IEnumerator SpawnAnimation(float delay)
	{
		yield return new WaitForSeconds(delay);

		int scaleSpeed = 3;

		if (currentScale < finalScale)
		{
			StartCoroutine("SpawnAnimation", delay);
			currentScale += scaleSpeed * Time.deltaTime ;
			if (currentScale > finalScale)
				currentScale = finalScale;
			transform.localScale = new Vector3(currentScale,currentScale,0);
		}
	}
	
	public void move_to(Vector3 finalPosition, int t)
	{
		finPos = finalPosition;
		type = t;

		if (type == 1)
		{
			currentScale -= Time.deltaTime * 3;
			transform.localScale = new Vector3(currentScale, currentScale, 0);
		}

		transform.position = Vector3.MoveTowards(transform.position, finalPosition, Time.deltaTime * 20);
		if (transform.position != finalPosition)
			StartCoroutine("Wait", 0.001f);
		else
			if (type == 1)
				Destroy(gameObject);

	}

	IEnumerator Wait(float delay)
	{
		yield return new WaitForSeconds (delay);
		move_to(finPos,type);
	}
	
	void Start()
	{
		float delay = 0.01f;
		SetFirstSprite();
		StartCoroutine("SpawnAnimation", delay);
	}

	public void NextNumber()
	{
		index++;
		spriteRenderer.sprite = sprites[index];
	}

}
