﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioExist : MonoBehaviour {
	
	static audioExist Instance;

	// Use this for initialization
	void Start () {
		if (Instance != null) {
			GameObject.Destroy (gameObject);
		} else {
			GameObject.DontDestroyOnLoad (gameObject);
			Instance = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}