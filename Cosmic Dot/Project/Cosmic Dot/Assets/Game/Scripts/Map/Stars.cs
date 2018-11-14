using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stars : MonoBehaviour {

	public AudioClip starCollectSound;
	public GameObject particles;
	float starVolume = 0.8f;
	GameObject gameAudioEffects;

	void Start () {
		//animations

		Sequence mySequence = DOTween.Sequence();
		mySequence.Append(gameObject.transform.DOMoveY(1, 2));
		mySequence.Insert(0, gameObject.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), mySequence.Duration()));
		mySequence.SetLoops(-1, LoopType.Yoyo);
		gameObject.transform.DORotate(new Vector3(0, 360, 0), 20, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo);

		gameAudioEffects = GameObject.Find ("GameAudioEffects").gameObject;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			GameObject.Find ("StarsSys").transform.Find ("Text").GetComponent<StarsSys> ().AddStar ();
			GameObject.Find ("KeysSystem").GetComponent<KeysSystem> ().AddKeys ();
			gameAudioEffects.GetComponent<AudioSource> ().PlayOneShot (starCollectSound, starVolume * PlayerPrefs.GetInt ("effectVol", 1));
			GameObject go = Instantiate (particles, transform.position, Quaternion.identity) as GameObject;
			Destroy (go, 1f);
			Destroy (gameObject, 0);
		}
	}
	

}
