using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	//AudioSource au;
	private Transform target;
	private EnemyAnimation enemyAnim;

	public float speed = 70f;
	public float explosionRadius = 0f;
	public GameObject impactEffect;

	[Header("Set in Inspector")]
	public float bulletDamage = 0;
	public float explosionDamage = 0;
	public float damageOverTimeAmount = 0;

	public void Seek(Transform _target)
	{
		target = _target;
	}
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (target == null) 
		{
			Destroy (gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame) 
		{
			HitTarget ();
			return;
		}
		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
		// may turn dudes
		transform.LookAt (target);
	}
	void HitTarget()
	{
		GameObject effecIns = (GameObject)Instantiate (impactEffect, transform.position, transform.rotation);
		Destroy (effecIns, 2f);

		if (explosionRadius > 0f) {
			//au = gameObject.GetComponent<AudioSource> ();
			//au.Play ();
			Explode ();
		} else {
			//Damage(target);
			HealthAndDamage dmgType = target.transform.GetComponent<HealthAndDamage> ();
			dmgType.DamageOnce(bulletDamage);
		}
		Destroy (gameObject);

	}

	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders) {
			if(collider.tag == "Enemy")
			{
				HealthAndDamage temp = collider.transform.gameObject.GetComponent<HealthAndDamage> ();
				temp.DamageOnce (explosionDamage);
			}
		}
	}

}
