using UnityEngine;
using System.Collections;

public class perfectPixelsCamera : MonoBehaviour {

	public static float pixelsToUnits = 1f;
	public static float scale = 1;

	Vector2 nativeResolution = new Vector2(240, 160);

	void Awake () {
		var camera = GetComponent<Camera> ();
		scale = 1;
		pixelsToUnits = 1f;
		if (camera.orthographic) {
			scale = Screen.height / nativeResolution.y;
			pixelsToUnits *= scale;
			camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnits;
		}

	}
}