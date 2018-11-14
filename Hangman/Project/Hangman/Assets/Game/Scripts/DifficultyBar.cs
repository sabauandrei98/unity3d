using UnityEngine;
using System.Collections;

public class DifficultyBar : MonoBehaviour {

	public Sprite[] diff;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		int difficulty = PlayerPrefs.GetInt("Difficulty",0);
		spriteRenderer =gameObject.GetComponent<Renderer>() as SpriteRenderer;
		spriteRenderer.sprite = diff[difficulty];
	}

}
