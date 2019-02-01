using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CheckIfVideoIsOver : MonoBehaviour {
	
	VideoPlayer vid;

	// Use this for initialization
	void Start () {
		GameObject guiTemp = GameObject.Find ("videoOne");
		vid = guiTemp.GetComponent<VideoPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (vid.isPlaying == false || Input.GetKey (KeyCode.Escape)) {
			SceneManager.LoadScene ("scene_1");
		}
	}
}
