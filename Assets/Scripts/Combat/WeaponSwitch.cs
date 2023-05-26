using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;
    public int previousSelectedWeapon;

    [Header("Animators")]
    [SerializeField] Animator WeaponSlotOne;
    [SerializeField] Animator WeaponSlotTwo;
    [SerializeField] Animator WeaponSlotThree;
    [SerializeField] Animator WeaponSlotFour;
    private void Start()
    {
        WeaponSwitching();
    }

    private void Update()
    {
        previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
            WeaponSlotOne.Play("WeaponSlot1Up");

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            WeaponSlotTwo.Play("WeaponSlot2Up");
            selectedWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            WeaponSlotThree.Play("WeaponSlot3Up");
            selectedWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            WeaponSlotFour.Play("WeaponSlot4Up");
            selectedWeapon = 3;
        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            WeaponSwitching();
        }
    }
    void WeaponSwitching()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
