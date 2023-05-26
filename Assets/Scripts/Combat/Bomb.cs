using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject ExplosionEffect;
    public float BlastRadius;
    public float Force = 750f;

    public void Explode()
    {
        Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Collider[]  colliders = Physics.OverlapSphere(transform.position,BlastRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            EnemyController EnemyScript = nearbyObject.GetComponent<EnemyController>();
            if (rb != null)
            {
                rb.mass = 1f;
                rb.drag = 0;
                rb.AddExplosionForce(Force, transform.position, BlastRadius, 0.05f);
            }
            if (EnemyScript != null)
            {
                EnemyScript.health -= 25f;
            }
        }
        Destroy(gameObject,0.05f);
    }
}
