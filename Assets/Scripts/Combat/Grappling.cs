using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    public PlayerController PmMaster;
    public Transform GunTip;
    public Transform Cam;
    public LayerMask WhatIsEnemy;
    public LineRenderer Line;
    GameObject _gb;



    [Header("Variables")]
    public float MaxGrabAbleDistance;
    public float MaxGrabAbleDelay;
    public float OverShotYpos;
    Vector3 _grappPoint;


    [Header("Cooldown")]
    public float GrapplingCooldown, GrapplingTimer;

    public  bool IsGrappling;
    [SerializeField] bool _isEnemyToUs;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) StartGrappling();

        if (GrapplingTimer > 0) GrapplingTimer -= Time.deltaTime;
    }
    void StartGrappling()
    {
        if (GrapplingTimer > 0) return;

        IsGrappling = true;

        RaycastHit hit;
        if (Physics.Raycast(Cam.position, Cam.forward, out hit, MaxGrabAbleDistance, WhatIsEnemy))
        {
            _gb = hit.collider.gameObject;
            _grappPoint = hit.point;
            Invoke(nameof(ExecuteGrappling), MaxGrabAbleDelay);

        }
        else
        {
            _grappPoint = Cam.position + Cam.forward * MaxGrabAbleDistance;

            Invoke(nameof(StopGrappling), MaxGrabAbleDelay);
        }
        Line.enabled = true;
    }
    void ExecuteGrappling()
    {
        if (_isEnemyToUs)
        {
            Vector3 pos = (PmMaster.transform.position - _gb.transform.position).normalized;
            if (_gb.GetComponent<Rigidbody>()) _gb.GetComponent<Rigidbody>().AddForce(pos * 50, ForceMode.Impulse);
        }
        else
        {
            Vector3 lowestPoint = new Vector3(transform.position.x,transform.position.y - 1, transform.position.z);
            float grapableYpos = _grappPoint.y - lowestPoint.y;
            float HighPoint = grapableYpos + OverShotYpos;
            if (grapableYpos < 0)
            {
                HighPoint = OverShotYpos;
            }

           
        }
        Invoke(nameof(StopGrappling), MaxGrabAbleDelay);
    }
    void StopGrappling()
    {
        IsGrappling = false;

        GrapplingTimer = GrapplingCooldown;
        Line.enabled = false;
    }

    public Vector3 GetGrapplePoint()
    {
        return _grappPoint;
    }
}
