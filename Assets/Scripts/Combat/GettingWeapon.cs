using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingWeapon : MonoBehaviour
{
    public GameObject weaponSlot1;
    public GameObject weaponSlot2;
    public GameObject weaponSlot3;
    public GameObject weaponSlot4;
    private void Start()
    {
        weaponSlot1.SetActive(false);
        weaponSlot2.SetActive(false);
        weaponSlot3.SetActive(false);
        weaponSlot4.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag  == "Smg")
        {
            weaponSlot3.GetComponentInChildren<WeaponController>().BulletsLeft = weaponSlot3.GetComponentInChildren<WeaponController>().MagazineSize;
            weaponSlot1.SetActive(false);
            weaponSlot2.SetActive(false);
            weaponSlot3.SetActive(true);
            weaponSlot4.SetActive(false);
            Destroy(other.gameObject);
        }
        if (other.tag == "Pistol")
        {
            weaponSlot2.GetComponentInChildren<WeaponController>().BulletsLeft = weaponSlot2.GetComponentInChildren<WeaponController>().MagazineSize;
            weaponSlot1.SetActive(false);
            weaponSlot2.SetActive(true);
            weaponSlot3.SetActive(false);
            weaponSlot4.SetActive(false);
            Destroy(other.gameObject);
        }
        if (other.tag == "Uzi")
        {
            weaponSlot1.GetComponentInChildren<WeaponController>().BulletsLeft = weaponSlot1.GetComponentInChildren<WeaponController>().MagazineSize;
            weaponSlot1.SetActive(true);
            weaponSlot2.SetActive(false);
            weaponSlot3.SetActive(false);
            weaponSlot4.SetActive(false);
            Destroy(other.gameObject);
        }
        if (other.tag == "Shotgun")
        {
            weaponSlot4.GetComponentInChildren<WeaponController>().BulletsLeft = weaponSlot4.GetComponentInChildren<WeaponController>().MagazineSize;
            weaponSlot1.SetActive(false);
            weaponSlot2.SetActive(false);
            weaponSlot3.SetActive(false);
            weaponSlot4.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
