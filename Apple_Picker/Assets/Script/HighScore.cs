using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

	static public int score = 0;

	// Use this for initialization
	void Start () {
		Text gt = this.GetComponent<Text> ();
		gt.text = "High Score: " + score;
	}

	void Awake(){
		if(PlayerPrefs.HasKey("HighScore")){
			score = PlayerPrefs.GetInt ("HighScore");
		}

		//Assign the high score to HighScore
		PlayerPrefs.SetInt("HighScore", score);
	}
	
	// Update is called once per frame
	void Update () {
		Text gt = this.GetComponent<Text> ();
		gt.text = "High Score: " + score;

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		//Update the PlayerPrefs HighScore if necessary
		if(score > PlayerPrefs.GetInt("HighScore")){
			
			PlayerPrefs.SetInt ("HighScore", score);

		}
	}
}
