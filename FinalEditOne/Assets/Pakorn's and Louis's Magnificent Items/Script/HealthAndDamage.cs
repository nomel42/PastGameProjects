using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndDamage : MonoBehaviour {

	[Header("Set in Inspector")]
	public int maxHealth = 0;
	public EnemyAnimation enemyAnim;

	private float damageDealt = 0;
	private float DoT = 0;
	private float health;
	AudioSource au;
	public bool playOnce = true;

	void Start () {
		health = maxHealth;
		au = gameObject.GetComponent<AudioSource> ();
	}

	void Update(){
		if (health <= 0){
			Death ();
		}

	}

	public void DamageOnce(float damageAmount){
		damageDealt = damageAmount;
		health -= damageDealt;
	}

	public void StartDoT(float damageAmount){
		StartCoroutine ("DamageOverTime", damageAmount);
	}
		
	IEnumerator DamageOverTime(float damageAmount){
		DoT = damageAmount;
		health -= DoT * Time.deltaTime * 1;
		yield return new WaitForSeconds (1f);
		if (health < 0)
			StopCoroutine ("DamageOverTime");
	}

	void Death()
	{
		enemyAnim = transform.gameObject.GetComponent<EnemyAnimation> ();
		enemyAnim.playDeathAnim ();
		if (playOnce) {
			playOnce = false;
			au.Play ();
		}
		Destroy (transform.gameObject, 1.2f);

	} 
}
