using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetParticles : MonoBehaviour {


	public GameObject[] particles;

	public GameObject getOmidParticles(string particlesName)
	{
		for (int i = 0; i < particles.Length; i++)
			if (particles [i].name == particlesName)
				return particles [i];

		//Default
		return particles [0];
	}
}
