using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMov : MonoBehaviour
{

    public float stopDistance = 9.0f; // the tank will stop movingt when in a certain range
    public Transform m_turret; // this is the tanks turret object

    private GameObject m_player; // gives a reference the the player 
    private NavMeshAgent m_navAgent; // a reference to the Nav Mesh 
    //agent in Unity so the tank knows where it can and can not go in the scene

    private Rigidbody m_rigid; // A reference to the tanks rigid body

    private bool m_follow; // will set the tank to true in relation to following the player tank

    void Start()
    {
        // this is optional in the final game, makes the enemy tank seek out the player
        //m_player = GameObject.FindGameObjectsWithTag("player");

        m_navAgent = GetComponent<NavMeshAgent>();
        m_rigid = GetComponent<Rigidbody>();
        m_follow = false;
    }

    void Update()
    {
        if (m_follow == false)
            return;
        // this bit is getting the distance from the player to the enemy tank
        float distance = (m_player.transform.position - transform.position).magnitude;
        // if the enemy is less than the stopping distance from the player t it will stop moving
        if (distance > stopDistance)
        {
            m_navAgent.SetDestination(m_player.transform.position);
            m_navAgent.isStopped = false;
        }
        else
        {
            m_navAgent.isStopped = true;
        }

        if (m_turret != null)
        {
            m_turret.LookAt(m_player.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") == true)
        {
            m_player = other.gameObject;
            m_follow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player") == true)
        {
            m_follow = false;

        }
    }

}