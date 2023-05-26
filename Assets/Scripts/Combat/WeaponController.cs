using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class WeaponController : MonoBehaviour
{
    [Header("Gun variables")]
    public int Damage, MagazineSize, BulletsPerTap, BulletsLeft, BulletsShot;
    public float TimeBetweenShooting, spread, Range, ReloadTime, TimeBetweenShots;
    public float impactForce = 30f;


    [Header("Bools")]
    public bool AllowButtonHold;
    bool _readyToShot;
    bool _isShooting;
    bool allowInvoke;

    [Header("References")]
    public Camera FpsCam;
    public Transform AttachPoint;
    public LayerMask WhatIsEnemy;
    public CameraController _camScript;
    public GameObject PrefabWeapon;

    [Header("Effects")]
    public ParticleSystem MuzzleFlash;
    public ParticleSystem HitPointFlash;
    public Image CrossHairImage;
    public WeaponRecoil WeaponRecoilScript;

    private void Awake()
    {
        MagazineSize = 15;
        BulletsLeft = MagazineSize;
        _readyToShot = true;
        allowInvoke = true;
    }

    private void Update()
    {
        MyInputs();
    }

    void MyInputs()
    {
        if (AllowButtonHold) _isShooting = Input.GetButton("Fire");
        else _isShooting = Input.GetButtonDown("Fire");

        if (_readyToShot && _isShooting && BulletsLeft > 0)
        {
            BulletsShot = BulletsPerTap;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.G) || (BulletsLeft <= 0  && Input.GetMouseButtonDown(0)) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            float r;
            GameObject gb = Instantiate(PrefabWeapon, new Vector3(AttachPoint.position.x, AttachPoint.position.y, AttachPoint.position.z + 2), Quaternion.Euler(0,0,r = gameObject.CompareTag("Shotgun") ? 0f : 90));
            gb.GetComponent<Rigidbody>().isKinematic = false;
            gb.GetComponent<Rigidbody>().AddForce(transform.forward * 75f, ForceMode.Impulse);
            Destroy(gb, 3f);
            transform.parent.gameObject.SetActive(false);
        }
    }
    void Shoot()
    {
        WeaponRecoilScript.Fire();
        StartCoroutine(CrossHair());
        _readyToShot = false;
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        if (Physics.Raycast(ray, out hit, Range))
        {
            targetPoint = hit.point;
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            Bomb bombScript = hit.collider.GetComponent<Bomb>();
            if (damageable != null)
            {
                damageable.TakeDamage(Damage);
            }
            if (bombScript != null)
            {
                bombScript.Explode();
            }
            //if (hit.rigidbody != null)
            //{
            //    hit.rigidbody.AddForce(-hit.normal * impactForce);
            //}
        }
        else
        {
            targetPoint = ray.GetPoint(50f);
        }

        Vector3 dirWithOutSpread = targetPoint - AttachPoint.position;

        Vector3 dirWithSpread = dirWithOutSpread + new Vector3(x, y, 0);


        MuzzleFlash.Play();

        if (hit.collider != null)
        {
            HitPointFlash.transform.position = hit.point;
            HitPointFlash.transform.rotation = Quaternion.identity;
            HitPointFlash.Play();
        }

        if (allowInvoke)
        {
            Invoke(nameof(ResetShoot), TimeBetweenShooting);
            allowInvoke = false;
        }

        if (BulletsLeft > 0 && BulletsShot < BulletsPerTap)
        {
            Invoke(nameof(Shoot), TimeBetweenShots);
        }
        BulletsLeft--;
    }

    void ResetShoot()
    {
        _readyToShot = true;
        allowInvoke = true;
    }

    IEnumerator CrossHair()
    {
        CrossHairImage.rectTransform.localScale = new Vector3(3, 3, 3);
        yield return new WaitForSeconds(0.05f);
        CrossHairImage.rectTransform.localScale = new Vector3(2.382124f, 2.233241f, 2.382124f);
    }
}
