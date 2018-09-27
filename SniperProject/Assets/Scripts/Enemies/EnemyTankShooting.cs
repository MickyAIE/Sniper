using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankShooting : MonoBehaviour
    // Basically the same as the player shotting script but 
    // with a set timer for the enemy tank to shoot
{
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public float m_LaunchForce = 30f;
    public float m_Shootdelay = 1f;
    private bool m_CanShoot;
    private float m_ShootTimer;

    private void Awake()
    {
        m_CanShoot = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (m_CanShoot == true)
        {
            m_ShootTimer -= Time.deltaTime;
            if (m_ShootTimer <= 0)
            {
                m_ShootTimer = m_Shootdelay;
                Fire();
            }
        }
    }

    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_Shell,
                          m_FireTransform.position,
                          m_FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_CanShoot = true;
        m_ShootTimer = m_Shootdelay;
    }

    private void OnTriggerExit(Collider other)
    {
        m_CanShoot = false;
    }
}
