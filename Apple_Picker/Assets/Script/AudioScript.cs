using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioScript : MonoBehaviour {

	AudioSource BGM;

	// Use this for initialization
	void Start () {
		BGM = GetComponent<AudioSource>();
		BGM.loop = true;
		BGM.Play ();

	}
}
