using UnityEngine;

public class CameraControl : MonoBehaviour
{
	[Range(0,100)]
	public float amplitude = 1;
	[Range(0.00001f, 0.99999f)]
	public float frequency = 0.98f;

	[Range(1,4)]
	public int octaves = 2;

	[Range(0.00001f,5)]
	public float persistance = 0.2f;
	[Range(0.00001f,100)]
	public float lacunarity = 20;

	[Range(0.00001f, 0.99999f)]
	public float burstFrequency = 0.5f;

	[Range(0,5)]
	public int  burstContrast = 2;

	float x, y, z;
	public GameObject player;
	public float shakeTime = 0;

	void Update ()
	{
		if (shakeTime > 0) {
			transform.position = Camera.main.transform.position + NoiseGeneration.Shake2D (amplitude, frequency, octaves, persistance, lacunarity, burstFrequency, burstContrast, Time.time);
			shakeTime -= Time.deltaTime;
		}
	}
}