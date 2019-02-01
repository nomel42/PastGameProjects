using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R) && SceneManager.GetActiveScene().name == "Game Over") {
			SceneManager.LoadScene ("TheGame1");
		} 
	}
}
