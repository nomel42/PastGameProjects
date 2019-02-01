using UnityEngine;

public class MineExplosion : MonoBehaviour
{
	public LayerMask m_TankMask;
	public LayerMask m_ShellMask;
	public ParticleSystem m_ExplosionParticles;       
	public AudioSource m_ExplosionAudio;              
	public float m_MaxDamage = 100f;                  
	public float m_ExplosionForce = 1000f;                             
	public float m_ExplosionRadius = 5f;              




	// Find all the tanks in an area around the shell and damage them.
	private void OnTriggerEnter(Collider other)
	{

			Collider[] colliders = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_TankMask);
			Collider[] collidersTwo = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_ShellMask);
			for(int i =0; i < colliders.Length; i++){

				Rigidbody targetRigidbody = colliders [i].GetComponent<Rigidbody> ();

				if (!targetRigidbody)
					continue;

				targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);

				TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

				if (!targetHealth)
					continue;

				float damage = CalculateDamage (targetRigidbody.position);
				targetHealth.TakeDamage (damage);
			}

			for(int i =0; i < collidersTwo.Length; i++){

				Rigidbody targetRigidbody = colliders [i].GetComponent<Rigidbody> ();

				if (!targetRigidbody)
					continue;

				targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);

				TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

				if (!targetHealth)
					continue;
				
				float damage = CalculateDamage (targetRigidbody.position);
				targetHealth.TakeDamage (damage);
			}

			m_ExplosionParticles.transform.parent = null;
			m_ExplosionParticles.Play();
			m_ExplosionAudio.Play();
			Destroy(m_ExplosionParticles.gameObject,m_ExplosionParticles.main.duration);
			Destroy (gameObject);
	}

	// Calculate the amount of damage a target should take based on it's position.
	private float CalculateDamage(Vector3 targetPosition)
	{
		Vector3 explosionToTarget = targetPosition - transform.position;

		float explosionDistance = explosionToTarget.magnitude;

		float relativeDistance = (m_ExplosionRadius - explosionDistance)/ m_ExplosionRadius;

		float damage = relativeDistance * m_MaxDamage;

		damage = Mathf.Max (0f, damage);

		return damage;
	}
}