using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private Rigidbody rb;
	public float speed;

	private int count;
	public Text countText;
	public Text winText;


	void Start()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal , 0.0f, moveVertical);

		rb.AddForce (movement * speed);

		if (transform.position.y < -10) 
		{
			transform.position = new Vector3 (0,1,0);
			rb.velocity = Vector3.zero;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count += 1;
			SetCountText ();
		}
	}

	void SetCountText()
	{
		countText.text = "Count [ " + count.ToString () + " ]";

		if (count >= 20) 
		{
			winText.text = "You Win!";
		}
	}
}
