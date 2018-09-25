using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public float Health = 100f;
    public GameObject NPC;
    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        HumanoidChase HealthScript1 = NPC.GetComponent<HumanoidChase>();
        if (HealthScript1)
        { 
        HealthScript1.Health = Health;
    }
        FlyingAI HealthScript2 = NPC.GetComponent<FlyingAI>();
        if (HealthScript2)
        {
            HealthScript2.Health = Health;
        }
        TankAI HealthScript3 = NPC.GetComponent<TankAI>();
        if (HealthScript3)
        {
            HealthScript3.Health = Health;
        }
        RavagerAI HealthScript4 = NPC.GetComponent<RavagerAI>();
        if (HealthScript4)
        {
            HealthScript4.Health = Health;
        }
        GeneratorIGuess HealthScript5 = NPC.GetComponent<GeneratorIGuess>();
        if (HealthScript5)
        {
            HealthScript5.Health = Health;
        }
        CorruptedRobotArmAI HealthScript6 = NPC.GetComponent<CorruptedRobotArmAI>();
        if (HealthScript6)
        {
            HealthScript6.Health = Health;
        }
    }
    public void TakeDamage(float amount)
    {
        Health -= amount;
        CorruptedRobotArmAI HealthScript6 = NPC.GetComponent<CorruptedRobotArmAI>();
        if (HealthScript6)
        {
            HealthScript6.PlayerHit = true;
        }
    }
}
