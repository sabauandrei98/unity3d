using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInfoSend : MonoBehaviour {

	Button btn;
	public void OnBtnClick(int buttonIndex)
	{
		GameObject.FindWithTag("GameCore").GetComponent<GameCore>().SelectColor(buttonIndex);	
	}
}
