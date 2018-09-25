using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public float m_startinghealth = 100f; // setting the amount of health the tanks start with
    public GameObject m_ExplosionPrefab; // a prefab will be instantiated in 'Awake, 
    //once a tank is dead it will be used

    private float m_CurrentHealth;
    private bool m_Dead;

    private ParticleSystem m_ExplosionParticles; // the particles system that will be used

    private void Awake()
    {
        m_ExplosionParticles =
            Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionParticles.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        m_CurrentHealth = m_startinghealth;
        m_Dead = false;

        SetHealthUI();
    }

    private void SetHealthUI()
    {
        // update the user interface showing the tank's health
    }

    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;
        SetHealthUI();
        // if the health is less than the set amount call OnDeath
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        // If one of the tanks die the particles system will play on the tanks position and 
        // de -activate the tank
        m_Dead = true;
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        m_ExplosionParticles.Play();
        gameObject.SetActive(false);
    }
}
