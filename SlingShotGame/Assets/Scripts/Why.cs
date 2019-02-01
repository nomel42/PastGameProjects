using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Why : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Goal"))
		{
			Debug.Log ("WHY");
			Goal.goalMet = true;
			Material mat = GetComponent<Renderer> ().material;
			Color c = mat.color;
			c.a = 1;
			mat.color = c;
		}
	}
}
