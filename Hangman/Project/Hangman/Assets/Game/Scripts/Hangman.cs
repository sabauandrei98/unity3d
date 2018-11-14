using UnityEngine;
using System.Collections;

public class Hangman : MonoBehaviour {

	public Sprite[] sprites;
	private SpriteRenderer spriteRenderer;
	int index = 0;

	public void nextSprite()
	{
		index++;

		if (index > sprites.Length - 1)
			index = sprites.Length - 1;

		spriteRenderer = gameObject.GetComponent<Renderer>() as SpriteRenderer;
		spriteRenderer.sprite = sprites[index];
	}
}
