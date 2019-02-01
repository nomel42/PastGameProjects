using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
	private Transform target;
	private Animator IsDead;
	private HealthAndDamage fireDamage;
	AudioSource au;
	[Header("Gerneral")]
	public float range = 15f;

	[Header("Bullets(Default)")]
	public float fireRate = 1f;
	private float fireCountdown = 0f;
	public GameObject bulletPrefab;
	[Header("Fire")]
	public bool useLaser = false;
	public LineRenderer lineRenderer;
	public ParticleSystem Flame;
	public float damageOVERTime = 5f;
	//	public ParticleSystem impactEffect; 
	public GameObject BurnSphere;
	[Header("Unity Setup Fields")]
	public string enemyTag = "Enemy";

	public float turnSpeed = 10f;
	public Transform partToRotate;
	public Transform firePoint;
	// Use this for initialization

	void Start () {
		au = gameObject.GetComponent<AudioSource> ();
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach(GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance) 
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
				IsDead = nearestEnemy.GetComponent<Animator> ();
			}
		}
		if(nearestEnemy != null && shortestDistance<= range && IsDead.GetBool("Die") == false)
		{
			target = nearestEnemy.transform;
		}
		else 
		{
			target = null;
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if (target == null) 
		{
			if (useLaser) {
				if (lineRenderer.enabled) {
					lineRenderer.enabled = false;
					//impactEffect.Stop ();
					BurnSphere.SetActive (false);
					if (au.isPlaying) {
						au.Stop();
					}
					//Flame.Stop ();
				}
			}
			return;
		}
		//Target lock on
		//end destination minus start destination

		LockOn();

		if (useLaser) {
			if (!(au.isPlaying)) {
				au.Play ();
			}
			Laser ();
		}
		else{

			if (fireCountdown <= 0f) 
			{
				Shoot ();
				fireCountdown = 1f / fireRate;
			}
			fireCountdown -= Time.deltaTime;
		}

	}
	void Shoot()
	{
		au.Play ();
		GameObject bulletGO = (GameObject)Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();
		if (bullet != null) 
		{
			bullet.Seek (target);
		}
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	void LockOn()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation, Time.deltaTime* turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);
	}
	void Laser()
	{
		if (!lineRenderer.enabled) {
			lineRenderer.enabled = true;
			//impactEffect.Play ();
			//Flame.Play ();
		}
		lineRenderer.SetPosition (0, firePoint.position);
		lineRenderer.SetPosition (1, target.position);
		BurnSphere.SetActive (true);
		BurnSphere.transform.position = target.position;
		//mpactEffect.transform.position = target.position;
		Flame.transform.position = firePoint.position;
		Flame.transform.LookAt (target);
		fireDamage = target.gameObject.GetComponent<HealthAndDamage> ();

		//fireDamage.StartDoT (damageOVERTime);
		fireDamage.StartDoT (damageOVERTime);
	}
}
