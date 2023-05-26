using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InElevator : MonoBehaviour
{
    [SerializeField] Animator _myAnimator;
    [SerializeField] GameObject _Collider;
    private void Start()
    {
        _Collider.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _myAnimator.Play("CloseElevator");
            _Collider.SetActive(true);
        }
    }
}
