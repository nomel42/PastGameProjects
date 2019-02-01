using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerModel : MonoBehaviour {

	Animator anim;

	float speed = 10f;
	float rotationSpeed = 80f;
	float rotationSet = 0f;
	float gravity = 8f;
	float jumpSpeed = 10f;

	private Vector3 moveDirection = Vector3.zero;

	CharacterController controller;

	void Start () {
		anim = GetComponent<Animator> ();
		controller = GetComponent<CharacterController>();
	}
	

	void FixedUpdate () {
		float move = Input.GetAxis ("Vertical");
		anim.SetFloat("Speed", move);

		if (controller.isGrounded) {
			if (Input.GetKey (KeyCode.W)) {
				moveDirection = new Vector3 (0, 0, 1);
				moveDirection *= speed;
				moveDirection = transform.TransformDirection (moveDirection);
			}
			else if (Input.GetKey (KeyCode.S)) {
				moveDirection = new Vector3 (0, 0, -1);
				moveDirection *= speed;
				moveDirection = transform.TransformDirection (moveDirection);
			}
			else {
				moveDirection = Vector3.zero;
			}
		}

		if (Input.GetButton("Jump"))
		{
			moveDirection.y = jumpSpeed;
		}

		rotationSet += Input.GetAxis ("Horizontal") * rotationSpeed * Time.deltaTime;
		transform.eulerAngles = new Vector3 (0, rotationSet, 0);

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);
	}
}
