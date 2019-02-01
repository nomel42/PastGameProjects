using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public float speed = 10f;
	//public float rotationSpeed = 10f;

	private Transform target;
	private Vector3 rotation;
	private int wavepointIndex = 0;
	private Animator isDead;
	private CapsuleCollider coll;
	public baseHealth baseH;

	void Start()
	{
		target = Waypoints.points [0];
		rotation = Waypoints.rotations [0];
		isDead = GetComponent<Animator> ();
		GameObject a = GameObject.Find ("Barrack");
		baseH = a.GetComponent<baseHealth> ();

		//coll = GetComponent<CapsuleCollider> ();


	}
	void Update()
	{
		Vector3 dir = target.position - transform.position;

		if (isDead.GetBool ("Die") == false) // Moving when not dead
			transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position,target.position)<=0.4f)
		{
			transform.Rotate (rotation);
			GetNextWaypoint();
		}
	}
	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1) 
		{
			baseH.setBHealth ((baseH.getBHealth()-10));

			Destroy (gameObject);
			return;
		}
		wavepointIndex++;
		target = Waypoints.points [wavepointIndex];
		rotation = Waypoints.rotations [wavepointIndex];
	}
}
