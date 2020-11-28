using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSphere : MonoBehaviour
{
    private float radius = 1f;
    public float damage;
    public LayerMask layer;
    void Update()
    {
        //gets the hits and stores them
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layer);
        if (hits.Length > 0)
        {
            //gets the first component to apply the damage
            hits[0].GetComponent<HealthMgmt>().dealDamage(damage);

        }
        
    }
}
