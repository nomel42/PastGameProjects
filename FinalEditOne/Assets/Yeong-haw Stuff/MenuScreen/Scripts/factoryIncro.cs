using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class factoryIncro : MonoBehaviour {
	GameObject g;
	theRaycast scpt;
	// Use this for initialization
	void Start () {
		g = GameObject.Find ("Raycast");
		scpt = g.GetComponent<theRaycast> ();
		InvokeRepeating ("updateSeconds",0.0f, 1.0f);
	}

	void updateSeconds(){
		scpt.setPoints (1);
	}
}
