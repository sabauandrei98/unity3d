using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelIntroSkip : MonoBehaviour {

	public void SkipIntro()
	{
		string sName = SceneManager.GetActiveScene ().name;
		int level = 0;

		for (int i = 0; i < sName.Length; i++) {
			if ('0' <= sName [i] && sName [i] <= '9')
				level = level * 10 + (sName [i] - '0');
		}

		SceneManager.LoadScene (level.ToString());
	}
}
