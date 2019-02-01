using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bright : MonoBehaviour {
	public Light myLight;
	float startTime;

	public bool changeColors = true;
	public float colorSpeed = 4.0f;
	public Color startColor;
	public Color endColor;
	public bool repeatColor = true;


	void Start(){

		myLight = GetComponent<Light> ();
		startTime = Time.time;
	}

	void FixedUpdate () {
		if (changeColors) {
			if (repeatColor) {
				float t = (Mathf.Sin (Time.time - startTime * colorSpeed));
				myLight.color = Color.Lerp (startColor, endColor, t);

			} else {
				float t = Time.time - startTime * colorSpeed;
				myLight.color = Color.Lerp (startColor, endColor, t);

			}
		}

	}
}
