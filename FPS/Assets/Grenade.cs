using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public float Damage = 20f;

    public GameObject explosionEffect;

    bool hasExploded = false;
    float countdown;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void Explode()
    {
        //show effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //ger nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }


            Enemy enemy = nearbyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                float proximity = (transform.position - enemy.transform.position).magnitude;
                float effect = 1 - (proximity / radius);
                enemy.TakeDamage(Damage);
            }
        }
        //Add Force
        //damage

        Destroy(gameObject);
    }
}
