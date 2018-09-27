using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharHealth : MonoBehaviour
{
    public float m_startinghealth = 100f; // setting the amount of health the tanks start with
    
    
    public float m_CurrentHealth;
    private bool m_Dead;

    



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

        gameObject.SetActive(false);
    }
}
