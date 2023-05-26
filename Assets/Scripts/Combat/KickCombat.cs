using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickCombat : MonoBehaviour
{
    //public LayerMask Interactable;
    public int Damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Vector3 pos = (gameObject.transform.position - other.gameObject.transform.position).normalized;
            other.gameObject.GetComponent<Rigidbody>().AddForce(-pos * 50, ForceMode.Impulse);
            IDamageable damageable = other.GetComponent<IDamageable>();
            Bomb bombScript = other.GetComponent<Bomb>();
            if (damageable != null)
            {
                damageable.TakeDamage(Damage);
            }
            if (bombScript != null)
            {
                bombScript.Explode();
            }
        }
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.layer == 10)
    //    {
    //        Vector3 pos = (gameObject.transform.position - other.gameObject.transform.position).normalized;
    //        other.gameObject.GetComponent<Rigidbody>().AddForce(-pos * 50, ForceMode.Impulse);
    //        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
    //        Bomb bombScript = other.gameObject.GetComponent<Bomb>();
    //        if (damageable != null)
    //        {
    //            damageable.TakeDamage(Damage);
    //        }
    //        if (bombScript != null)
    //        {
    //            bombScript.Explode();
    //        }
    //    }
    //}
}
