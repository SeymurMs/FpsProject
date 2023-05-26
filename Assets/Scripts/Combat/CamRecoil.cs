using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRecoil : MonoBehaviour
{
	[Header("Recoil Settings")]
	public float rotationSpeed = 6;
	public float returnSpeed = 25;
	[Space()]

	[Header("Hipfire:")]
	public Vector3 RecoilRotation = new Vector3(2f, 2f, 2f);
	[Space()]

	[Header("Aiming")]
	public Vector3 RecoilRotationAiming = new Vector3(0.5f, 0.5f, 1.5f);
	[Space()]

	private Vector3 currentRotation;
	private Vector3 Rot;

	WeaponController _myGun;



	private void Start()
	{
		_myGun = GetComponent<WeaponController>();
	}

	private void FixedUpdate()
	{
		currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
		Rot = Vector3.Slerp(Rot, currentRotation, rotationSpeed * Time.fixedDeltaTime);
		transform.localRotation = Quaternion.Euler(Rot);
	}

	public void Fire()
	{
		rotationSpeed = 6f;
		returnSpeed = 25f;
		RecoilRotation = new Vector3(15f, 15f, 2f);
		currentRotation += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
	}

	private void Update()
	{

		if (Input.GetButtonDown("Fire"))
		{
            Fire();
		}
	}
	public void FirePowerful()
	{
		rotationSpeed = 2f;
		returnSpeed = 7f;
		RecoilRotation = new Vector3(55f, 55f, 7f);
		currentRotation += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
	}
}
