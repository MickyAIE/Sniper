using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //ADD HEALTH
            Debug.Log("Health Picked Up");
            Destroy(this.gameObject);
        }
    }
}
