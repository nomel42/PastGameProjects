using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class baseHealthTut : MonoBehaviour {
	private float baseHe;
	Text gui;
	// Use this for initialization
	void Start () {
		baseHe = 100f;
		GameObject guiTemp = GameObject.Find ("BaseStam");
		gui = guiTemp.GetComponent<Text> ();
	}

	void Update () {
		gui.text = "Base Health: " + baseHe;

		if (baseHe < 100) {
			baseHe = 100;
		}
	}
		
	public float getBHealth()
	{
		return baseHe;
	}

	public void setBHealth(float i)
	{
		 baseHe = i;
	}
}
