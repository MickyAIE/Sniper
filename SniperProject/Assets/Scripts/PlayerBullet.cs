using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public float damage = 1f;
    // Use this for initialization
    void Start () {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    void OnParticleCollision(GameObject other)
    {
           EnemyHealth targetHealth = other.GetComponent<EnemyHealth>();
            if (targetHealth != null)
            {
                // Calculate the amount of damage the target should take
                // based on it's distance from the shell.
                // Deal this damage to the tank
                targetHealth.TakeDamage(damage);
            
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
