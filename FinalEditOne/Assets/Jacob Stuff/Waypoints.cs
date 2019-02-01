using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

	public static Transform[] points;
	public static Vector3[] rotations;
	void Awake()
	{
		points = new Transform[transform.childCount];
		rotations = new Vector3[transform.childCount];
		for (int i = 0; i < points.Length; i++) {
			points[i] = transform.GetChild(i);
			rotations [i] = transform.GetChild (i).eulerAngles;
		}
	}
}
