using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour {
	[Header("Set in Inspector")]

	public GameObject ApplePrefab;

	public float speed = 3f;

	public float rotationSpeed = 40f;

	public float leftAndRightEdge = 25f;

	public float changeToChangeDirections = .005f;

	public float secondsBetweenAppleDrops = 1f;

	// Use this for initialization
	void Start () {
		Invoke ("DropApple", 2f);
	}

	void FixedUpdate()
	{
		if (Random.value < changeToChangeDirections) {
			speed *= -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//rotate
		if (speed < 0) {
			transform.Rotate ((Vector3.up * rotationSpeed) * Time.deltaTime, Space.World);
		} else {
			transform.Rotate ((Vector3.down * rotationSpeed) * Time.deltaTime, Space.World);
		}


		//movement
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;

		//Changing Directions
		if (pos.x < -leftAndRightEdge) {
			speed = Mathf.Abs (speed);
		} else if (pos.x > leftAndRightEdge) {
			speed = -Mathf.Abs (speed);
		}
	}
		

	void DropApple(){
		GameObject apple = Instantiate<GameObject> ( ApplePrefab );
		apple.transform.position = transform.position;
		Invoke ( "DropApple", secondsBetweenAppleDrops);
	}

}


