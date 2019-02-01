using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour {

	public Animator anim;

	void Awake(){
		anim = GetComponent<Animator> ();
	}

	public void playDeathAnim(){
		anim.SetBool ("Die", true);
	}
		
}
