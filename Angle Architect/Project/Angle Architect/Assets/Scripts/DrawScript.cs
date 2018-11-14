using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour
{
	int segments = 6;
	LineRenderer line;
	float offsetAngle;

	void Start ()
	{
		line = gameObject.GetComponent<LineRenderer>();
		line.material = new Material (Shader.Find ("Sprites/Default"));
		line.startColor = new Color(0, 1, 0.42f, 1);
		line.endColor = new Color(0, 1, 0.42f, 1);
		line.useWorldSpace = false;
	}
		
	public void CreatePoints (Vector3 offset, float xradius, float yradius, float a)
	{
		float x;
		float y;
		float z = 0f;
		line.SetVertexCount (segments + 1);

		//Debug.Log ("OffsetAngle: " + offsetAngle.ToString ());
		float angle = offsetAngle - 80;

		for (int i = 0; i < (segments + 1); i++)
		{
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius + offset.x;
			y = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius + offset.y;

			line.SetPosition (i,new Vector3(x,y,z) );

			angle += (a / segments);
		}
	}

	public void offsetAngle360(Vector3 pointA, Vector3 pointC)
	{
		Vector2 p1, p2, o;

		p1 = new Vector2 (pointC.y - 1, pointC.y);
		p2 = new Vector2 (pointA.x, pointA.y);
		o = new Vector2 (pointC.x, pointC.y);

		Vector2 v1, v2;
		if (o == default(Vector2)) {
			v1 = p1.normalized;
			v2 = p2.normalized;
		} else {
			v1 = (p1 - o).normalized;
			v2 = (p2 - o).normalized;
		}
		float angle = Vector2.Angle (v1, v2);

		offsetAngle = Mathf.Sign (Vector3.Cross (v1, v2).z) < 0 ? (angle) : (360 - angle) % 360;
	}

	public void ResetSegments()
	{
		line.positionCount = 0;
	}
}