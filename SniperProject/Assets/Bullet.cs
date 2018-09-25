using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float m_maxlifetime = 6f; // the lifetime of the shell so it disappears after some time
    public float m_maxdamage = 500f; // the damage of the shell to the tanks if at the center of the explosion
    public float m_explosionradius = 5f; // the radius of the eplosion and damage to the tanks if in the range
    public float m_explosionforce = 50f; // the amount of force from the sheel when it hits the tanks


	private void Start () {
		Destroy(gameObject, m_maxlifetime); // if the shell hasn't hit anything by the end of its 
        //lifetime it will be destroyed
	}
	
    private void OnCollisionEnter(Collision other)
    {
        // Finding the rigidbody that the shell is colliding with
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (targetRigidbody != null)
        {
            targetRigidbody.AddExplosionForce(m_explosionforce,
                            transform.position, m_explosionradius);

            CharHealth targetHealth = targetRigidbody.GetComponent<CharHealth>();

            if (targetHealth != null)
            {
                float damage = CalculateDamage(targetRigidbody.position);
                targetHealth.TakeDamage(damage);
            }
        }
        //Destroying the shell
        Destroy(gameObject);

    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        // creates a vector from the shell to the target in front
        Vector3 explosionToTarget = targetPosition - transform.position;
        // Finds the distance from the shell and target
        float explosionDistance = explosionToTarget.magnitude;
        // Calculates the proportion of the maximum explosion radius the target can be away from the shell
        float relativeDistance =
            (m_explosionradius - explosionDistance) / m_explosionradius;
        // Calculates the max damge inb relation to how close or far the shell was to the target
        float damage = relativeDistance * m_maxdamage;
        // Makes surew the minimum damge possible is 0
        damage = Mathf.Max(0f, damage);
        return damage;

    }
}
