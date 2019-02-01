using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentPlacement : MonoBehaviour {

	public Color hoverColor;

	private Renderer rend;
	private Color startColor;
	private bool hasTurret = false;

	void Start(){
		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;
	}

	public void ChangeColor(){
			rend.material.color = hoverColor;
	}

	public void ChangeColorBack () {
			rend.material.color = startColor;
	}

	public bool hasTurretGet(){
		return hasTurret;
	}

	public void hasTurretTrue(){
		hasTurret = true;
	}
	public void hasTurretFalse(){
		hasTurret = false;
	}
}

