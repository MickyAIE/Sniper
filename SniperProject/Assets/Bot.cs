using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class Bot : MonoBehaviour {

    public float health = 100f;
    public AudioClip pain;
    private GameObject player;
    public bool bAlert;
    public float AlertTime;
    public bool canShoot = true;
    public float AlertDistance = 10f;
    private NavMeshAgent nav;

    public List<Transform> waypoints = new List<Transform>();
    int waypointCounter = 0;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(Death());
        }
        shoot();
        Alert();
        WaypointMovement();
    }

    void WaypointMovement()
    {
        if (bAlert == false)
        {
            if (Vector3.Distance(transform.position, waypoints[waypointCounter].position) > 1f)
            {
                nav.SetDestination(waypoints[waypointCounter].position);
            }
            else
            {
                if (waypointCounter < waypoints.Count-1)
                {
                    waypointCounter++;
                }
                else
                {
                    waypointCounter = 0;
                }
            }
        }
    }

    void Alert()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < AlertDistance)
        {
            bAlert = true;
            if (Physics.Raycast(transform.position, (player.transform.position - transform.position), out rhit, maxRange))
            {
                if (rhit.collider.gameObject.tag == "Player")
                {
                    bAlert = true;
                    AlertTime = 0;
                }
                else
                {
                    AlertTime += Time.deltaTime;
                    if (AlertTime == 30)
                    {
                        bAlert = false;
                    }
                }
            }
        }
    }

    //Can see the player
    void sight(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        nav.CalculatePath(targetPosition, path);

        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;

        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        // The points inbetween are the corners of the path.
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        nav.SetDestination(path.corners[1]);
    }

    public bool isPlayer()
    {
        if (Physics.Raycast(transform.position, (player.transform.position - transform.position), out rhit, maxRange))
        {
            if (rhit.collider.gameObject.tag == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    float maxRange = 50;
    RaycastHit rhit;
    //Shooting the player
    void shoot()
    {
        if (bAlert == true && canShoot)
        {
            Debug.DrawRay(transform.position, (player.transform.position - transform.position), Color.green);
            if (Vector3.Distance(transform.position, player.transform.position) < maxRange)
            {
                if (isPlayer())
                {
                    StartCoroutine(NextShot());
                    canShoot = false;
                    rhit.collider.gameObject.SendMessage("hit", 5F);
                }
                else
                {
                    sight(player.transform.position);
                }
            }
        }
    }

    IEnumerator NextShot()
    {
        yield return new WaitForSeconds(1F);
        canShoot = true;
    }

    void hit(float damage)
    {
        shoot();
        bAlert = true;
        health -= damage;
        GetComponent<AudioSource>().PlayOneShot(pain);
    }

    IEnumerator Death()
    {
        nav.isStopped = true;
        canShoot = false;
        yield return new WaitForSeconds(3F);

        Destroy(gameObject);
    }

}
