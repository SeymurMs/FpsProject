using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickController : MonoBehaviour
{
    public Animator _kickAnim;
    public GameObject _mesh;
    [SerializeField] float _kickCd;
    private float _KickTimer;
    public CamRecoil Recoil;
    [SerializeField] CameraController _camController;
    [SerializeField] PlayerController _pmController;
    [SerializeField] SphereCollider _myCollider;

    // Start is called before the first frame update
    void Start()
    {
        _mesh.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MyInputs();
        if (_KickTimer > 0) _KickTimer -= Time.deltaTime;
    }

    void MyInputs()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button4) )
        {
            if (_KickTimer > 0) return;
            else _KickTimer = _kickCd;
            _mesh.SetActive(true);
            _kickAnim.SetBool("KickStart", true);
            Recoil.FirePowerful();
            StartCoroutine(KickEnding()) ;
        }
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button4)) && _camController._rotationX < 45)
        {
            _myCollider.enabled = true;
        }
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button4)) && _camController._rotationX > 20)
        {
            _myCollider.enabled = false;
        }
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button4)) && !_pmController._isGround)
        {
            _myCollider.enabled = true;
        }
    }

    IEnumerator KickEnding()
    {
        yield return new WaitForSeconds(0.2f);
        _kickAnim.SetBool("KickEnd", true);
        yield return new WaitForSeconds(0.4f);
        _mesh.SetActive(false);
    }
}
