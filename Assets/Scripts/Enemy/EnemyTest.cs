using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class EnemyTest : MonoBehaviour
{
    //Ref
    public Transform WalkP1, WalkP2;
    public bool IsWalkP1Set, IsWalkP2Set;
    public bool IsIdleEnd = true;
    public GameObject ProjectTile;
    public LayerMask WhatIsGround, WhatIsPlayer;
    private Transform _player;
    private NavMeshAgent _meshAgent;
    private Animator _animator;


    //Patrolling 
    public float WalkPointRange;
    private Vector3 _walkPoint;
    bool _isWalkPointSet;

    //Attack
    public float TimeBetweenAttack;
    bool _alreadyAttack;


    //States
    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _meshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckPlayer();
    }

    void CheckPlayer()
    {
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);


        //if (!PlayerInAttackRange && !PlayerInSightRange) Patroll();
        if (PlayerInSightRange && !PlayerInAttackRange) ChasePlayer();
        else if (PlayerInAttackRange && PlayerInSightRange) AttackPlayer();
    }
    void Patroll()
    {
        if (!_isWalkPointSet && IsIdleEnd) SetWalkPoint();
        else if (IsIdleEnd && _isWalkPointSet) _meshAgent.SetDestination(_walkPoint);

        Vector3 distanceWalkP = transform.position - _walkPoint;

        if (distanceWalkP.magnitude < .9f)
        {
            StartCoroutine(WalkIdle());
            _isWalkPointSet = false;
        }
    }
    void ChasePlayer()
    {
        _meshAgent.SetDestination(_player.position);
    }
    void AttackPlayer()
    {
        _meshAgent.SetDestination(transform.position);

        if (Mathf.Abs(transform.position.y - _player.position.y) < 3f)
        {
        }
        else
        {
        }
        transform.LookAt(_player);
        if (!_alreadyAttack)
        {
            //Attack
            //Rigidbody rb = Instantiate(ProjectTile, transform.position, Quaternion.identity ).GetComponentInChildren<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //_alreadyAttack = true;

            Invoke("ResetAttack", TimeBetweenAttack);
        }
    }
    void SetWalkPoint()
    {
        if (!IsWalkP1Set)
        {
            _walkPoint = WalkP1.position;
            IsWalkP1Set = true;
        }
        else if (IsWalkP1Set)
        {
            _walkPoint = WalkP2.position;
            IsWalkP1Set = false;
        }

        if (Physics.Raycast(_walkPoint, -transform.up, 2f, WhatIsGround))
        {
            _isWalkPointSet = true;

        }
    }
    void ResetAttack()
    {
        _alreadyAttack = false;
    }

    IEnumerator WalkIdle()
    {
        _animator.SetBool("IsStop", true);
        IsIdleEnd = false;
        yield return new WaitForSeconds(1.2f);
        IsIdleEnd = true;
        _animator.SetBool("IsStop", false);
    }

}

