using UnityEngine;
using System.Collections;

public class ScreenTimeOut : MonoBehaviour {
	
	void Start () {
		//Screen never sleeps
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

}
