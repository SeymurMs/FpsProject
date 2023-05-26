using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Animator _myAnimator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _myAnimator.Play("Elevator");
        }
    }
}
