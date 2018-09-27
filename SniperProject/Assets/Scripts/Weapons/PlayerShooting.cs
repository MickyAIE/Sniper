using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public float m_LaunchForce = 30f;

    void Update()
    {
        // later on, we'll check with the 'Gamer Manager' to make 
        // sure the game isn't over

        if (Input.GetButtonUp("Fire1"))
        {
            Fire();
        }
    }
    private void Fire()
    {
        // creating a shell that will be shoot and alsoi storing a reference to the rigidbody
        Rigidbody shellInstance = Instantiate(m_Shell,
                                  m_FireTransform.position,
                                  m_FireTransform.rotation) as Rigidbody;
        //setting the velocity of the shell to the launch force in the foward position of the shell spawn
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
    }
}