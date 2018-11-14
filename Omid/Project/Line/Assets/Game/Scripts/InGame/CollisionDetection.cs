using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetection : MonoBehaviour {

	//Game panel script
	GamePanel gPanel;

	Line line;


	void Start()
	{
		gPanel = GameObject.Find ("GamePanel").GetComponent<GamePanel> ();
		line = GameObject.Find ("Line").GetComponent<Line> ();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "space") {
			col.gameObject.transform.parent.GetComponent<ReduceWallSize>().startSizeReduction();
			gPanel.setScore ();
			gPanel.manageDrunkMode ();
			line.IncreaseSize ();
		} else {
			if (col.gameObject.tag == "wall") {
				gPanel.setGameOver ();
			}
		}
	}
}
