using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGradient : MonoBehaviour {

	public Gradient getOmidColors(string colorName)
	{
		Gradient gradient = new Gradient();

		if (colorName == "Default") {
			gradient.SetKeys(
				new GradientColorKey[] { new GradientColorKey(new Color(0.55f, 0.717f, 0), 0), new GradientColorKey(new Color(0.55f, 0.717f, 0), 1) },
				new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(0, 1) }
			);
		}

		if (colorName == "Frostmid") {
			gradient.SetKeys(
				new GradientColorKey[] { new GradientColorKey(new Color(1, 1, 1), 0), new GradientColorKey(new Color(0, 0.87f, 1), 0.7f) },
				new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(0.53f, 1) }
			);
		}

		if (colorName == "Lavamid") {
			gradient.SetKeys(
				new GradientColorKey[] { new GradientColorKey(new Color(1, 0, 0), 0), new GradientColorKey(new Color(0.71f, 0.61f, 0), 0.7f) },
				new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(1, 1) }
			);
		}

		if (colorName == "Earthmid") {
			gradient.SetKeys(
				new GradientColorKey[] { new GradientColorKey(new Color(0.44f, 0.13f, 0), 0), new GradientColorKey(new Color(1, 0.5f, 0), 0.7f) },
				new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(0.5f, 1) }
			);
		}

		if (colorName == "Aquamid") {
			gradient.SetKeys(
				new GradientColorKey[] { new GradientColorKey(new Color(0, 0.55f, 0.63f), 0), new GradientColorKey(new Color(0, 1, 1), 0.5f) },
				new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(0, 1) }
			);
		}

		if (colorName == "Airmid") {
			gradient.SetKeys(
				new GradientColorKey[] { new GradientColorKey(new Color(1, 1, 1), 0), new GradientColorKey(new Color(1, 1, 1), 1) },
				new GradientAlphaKey[] { new GradientAlphaKey(0.07f, 0), new GradientAlphaKey(0, 0.5f), new GradientAlphaKey(0.07f, 1) }
			);
		}

		if (colorName == "Rastamid") {
			gradient.SetKeys(
				new GradientColorKey[] { new GradientColorKey(new Color(1, 0.18f, 0), 0.23f), new GradientColorKey(new Color(0.86f, 0.86f, 0), 0.34f), new GradientColorKey(new Color(0.86f, 0.86f, 0), 0.53f), new GradientColorKey(new Color(0.3f, 0.4f, 0), 0.61f) },
				new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(1, 1) }
			);
		}

		if (colorName == "Rainbowmid") {
			gradient.SetKeys (
				new GradientColorKey[] { 
					new GradientColorKey (new Color (1, 0, 0), 0), new GradientColorKey (new Color (1, 1, 0), 0.22f),
					new GradientColorKey (new Color (0, 1, 0), 0.37f), new GradientColorKey (new Color (0, 1, 1), 0.54f),
					new GradientColorKey (new Color (0, 0, 1), 0.71f), new GradientColorKey (new Color (1, 0, 1), 0.83f),
					new GradientColorKey (new Color (1, 0, 0), 1)
				},
				new GradientAlphaKey[] { new GradientAlphaKey (1, 0), new GradientAlphaKey (1, 1) }
			);
		}

		if (colorName == "Drunkomid") {
			gradient.SetKeys (
				new GradientColorKey[] { 
					new GradientColorKey (new Color (0.62f, 0, 0.42f), 0), new GradientColorKey (new Color (0, 0, 0), 0.5f)
				},
				new GradientAlphaKey[] { new GradientAlphaKey (1, 0), new GradientAlphaKey (0.1f, 1) }
			);
		}

		if (colorName == "Bubblegummid") {
			gradient.SetKeys (
				new GradientColorKey[] { 
					new GradientColorKey (new Color (1, 0, 0.75f), 0.33f), new GradientColorKey (new Color (1, 1, 1), 0.72f)
				},
				new GradientAlphaKey[] { new GradientAlphaKey (1, 0), new GradientAlphaKey (0, 1) }
			);
		}

		if (colorName == "Sunnymid") {
			gradient.SetKeys (
				new GradientColorKey[] { 
					new GradientColorKey (new Color (1, 1, 0), 0.55f), new GradientColorKey (new Color (0.9f, 1, 0.65f), 0.66f)
				},
				new GradientAlphaKey[] { new GradientAlphaKey (1, 0), new GradientAlphaKey (0, 1) }
			);
		}

		if (colorName == "Toxicmid") {
			gradient.SetKeys (
				new GradientColorKey[] { 
					new GradientColorKey (new Color (0, 1, 0), 0), 
					new GradientColorKey (new Color (0.05f, 0.25f, 0.1f), 0.23f),
					new GradientColorKey (new Color (0.14f, 1, 0.6f), 0.4f),
					new GradientColorKey (new Color (0, 0, 0), 0.56f)
				},
				new GradientAlphaKey[] { new GradientAlphaKey (1, 0), new GradientAlphaKey (0.20f, 1) }
			);
		}

		if (colorName == "Chillimid") {
			gradient.SetKeys (
				new GradientColorKey[] { 
					new GradientColorKey (new Color (0.11f, 0.68f, 0), 0.08f), 
					new GradientColorKey (new Color (1, 0, 0), 0.62f),
				},
				new GradientAlphaKey[] { new GradientAlphaKey (1, 0), new GradientAlphaKey (1, 1) }
			);
		}
			


		return gradient;
	}
}
