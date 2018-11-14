using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerColor : MonoBehaviour {

	public Color returnColor(string color)
	{
		if (color == "red")
			return new Color (0.72f, 0.15f, 0.15f, 1);
		if (color == "pink")
			return new Color (1, 0.22f, 0.85f, 1);
		if (color == "darkblue")
			return new Color (0.14f, 0.21f, 1, 1);
		if (color == "lightblue")
			return new Color (0.19f, 0.43f, 1, 1);
		if (color == "green")
			return new Color (0.19f, 1, 0.12f, 1);
		if (color == "yellow")
			return new Color (0.94f, 1, 0.02f, 1);
		if (color == "orange")
			return new Color (0.94f, 0.31f, 0.02f, 1);
		if (color == "default")
			return new Color (0.51f, 0.62f, 0, 1);
		if (color == "neon")
			return new Color (0, 0.83f, 1, 1);
		if (color == "white")
			return new Color (1, 1, 1, 1);
		if (color == "grey")
			return new Color (0.32f, 0.32f, 0.32f, 1);
		if (color == "black")
			return new Color (0, 0, 0, 1);

		//return default
		return new Color (0.51f, 0.62f, 0, 1);
	}
}
