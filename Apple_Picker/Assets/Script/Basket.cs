using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //This line enables use of uGUI features.

public class Basket : MonoBehaviour {
	
	[Header("Set Dynamically")]
	public Text scoreGT;
	AudioSource AppleCrunch;
	public GameObject AppleTreePrefab;

	// Use this for initialization
	void Start () {
		AppleCrunch = GetComponent<AudioSource>();
		GameObject scoreGO = GameObject.Find ("ScoreCounter");
		scoreGT = scoreGO.GetComponent<Text> ();
		scoreGT.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		//Get position of mouse
		Vector3 mousePos2D = Input.mousePosition;

		//how far z go
		mousePos2D.z = -Camera.main.transform.position.z;

		//convert 2D to 3D position 
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );

		//move the x 
		Vector3 pos = this.transform.position;
		pos.x = mousePos3D.x;
		this.transform.position = pos;
	
	}

	void OnCollisionEnter( Collision coll){
		GameObject collidedWith = coll.gameObject;
		if (collidedWith.tag == "Apple") {
			AppleCrunch.Play ();
			Destroy (collidedWith);

			int score = int.Parse (scoreGT.text);
			score += 50;
			scoreGT.text = score.ToString ();

			//Track the high score
			if (score > HighScore.score) {
				HighScore.score = score;
			}
		}
	}
}
