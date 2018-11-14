using UnityEngine;
using System.Collections;

public class UniversalLett : MonoBehaviour {

	public float lifetime = 10f, speed = 200f;
	Vector3 finPos;

	IEnumerator Go(float delay)
	{
		yield return new WaitForSeconds(delay);

		if (transform.position != finPos)
		{
			gameObject.transform.position = Vector3.MoveTowards(transform.position, finPos, speed * Time.deltaTime);
			StartCoroutine("Go",0.01f);
		}
	}
	
	public void Move(Vector3 pos, float scale)
	{
		Destroy(gameObject, lifetime);
		finPos = pos;
		StartCoroutine("Go",0);
		gameObject.transform.localScale = new Vector3(scale, scale, 0);
	}
}
