using UnityEngine;
using System.Collections;

public class LanguageInGame : MonoBehaviour {

	public Sprite[] sprites;
	private SpriteRenderer spriteRenderer;

	// 0 - uk
	// 1 - ro

	void Start()
	{
		spriteRenderer = gameObject.GetComponent<Renderer>() as SpriteRenderer;
		spriteRenderer.sprite = sprites[PlayerPrefs.GetInt("Language") - 1];
	}
}
