using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

	public Transform pausedMenu;
	public Transform canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			pause ();
		}

		if (pausedMenu.gameObject.activeInHierarchy == true) {
			Time.timeScale = 0;
			AudioListener.pause = true;
			canvas.gameObject.SetActive (false);
		}
	}

	public void pause(){
		if (pausedMenu.gameObject.activeInHierarchy == false) {
			pausedMenu.gameObject.SetActive (true);
			canvas.gameObject.SetActive (false);
			Time.timeScale = 0;
			AudioListener.pause = true;
			//camera.GetComponent<Camera>.enabled = false;
		} else {
			pausedMenu.gameObject.SetActive (false);
			canvas.gameObject.SetActive (true);
			Time.timeScale = 1;
			AudioListener.pause = false;
		}

	}
}
