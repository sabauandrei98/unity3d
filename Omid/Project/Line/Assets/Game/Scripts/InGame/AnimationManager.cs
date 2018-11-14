using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	public Animator score;

	public void playScoreAnimation()
	{
		score.SetTrigger ("startAnimation");
	}
}
