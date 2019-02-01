using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneMangerScript : MonoBehaviour {

	static SceneMangerScript Instance;
	static int currentLevel = 1;
	void Start()
	{
		Time.timeScale = 1;
		if (Instance != null) {
			GameObject.Destroy (gameObject);
		} else {
			GameObject.DontDestroyOnLoad (gameObject);
			Instance = this;
		}
	}

	void Update()
	{
		Debug.Log ("MyCurrentLevel is" + currentLevel);
		if (Input.GetKeyDown (KeyCode.R) && SceneManager.GetActiveScene().name == "Game Over") {
			if (currentLevel == 1)
				SceneManager.LoadScene ("TheGame1");
			else if (currentLevel == 2) {
				SceneManager.LoadScene ("TheGame2");
			} else {
				SceneManager.LoadScene ("TheGame3");
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			resetLevel ();
			SceneManager.LoadScene ("scene_1");
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public void updateLevel()
	{
		Debug.Log("called update");
		currentLevel += 1;
	}

	public void resetLevel()
	{
		currentLevel = 1;
	}
}
