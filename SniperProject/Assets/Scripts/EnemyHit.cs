using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (tag == "Head")
        {
            Debug.Log("Head Hit!");
        }
        else if (tag == "Stomach")
        {
            Debug.Log("Stomach Hit!");
        }
        else if (tag == "Legs")
        {
            Debug.Log("Legs Hit!");
        }

        Debug.Log("I have collided");
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
