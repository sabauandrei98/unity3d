using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPlaneteSelect : MonoBehaviour {

	int index = 0;
	int maxIndex = 9, minIndex = 0;
	GameObject infoPlanete;

	void Start()
	{
		infoPlanete = GameObject.FindWithTag("InfoSchimbaPlaneta");
		Planet(0);
	}

	void Planet(int ind)
	{
		infoPlanete.GetComponent<InfoSchimbaPlaneta>().SchimbaPlaneta(ind);
	}



	public void NextPlanet()
	{
		if(index + 1 <= maxIndex)
		{
			infoPlanete.GetComponent<InfoSchimbaPlaneta>().TurnVisibleOff(index);
			index ++;
			Planet(index);
		}
	}

	public void PrevPlanet()
	{
		if(index - 1 >= minIndex)
		{
			infoPlanete.GetComponent<InfoSchimbaPlaneta>().TurnVisibleOff(index);
			index --;
			Planet(index);
		}
	}
}
