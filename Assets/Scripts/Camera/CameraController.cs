using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraController : MonoBehaviour
{
    [Header("Mouse Sensivity")]
    [SerializeField] float _sensX;
    [SerializeField] float _sensY;
    [SerializeField] float _multiplier;

    float _mouseX;
    float _mouseY;

    public float _rotationX;
    float _rotationY;

    [SerializeField] Transform _camera;
    [SerializeField] Transform _orientation;

    [Header("Effects")]
    [SerializeField] float _slowMotionScale = 0.2f;
    [SerializeField] PlayerController _playerController;


    bool _isSlow;
    bool _isUpdated;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        ControlCameraInput();

        _camera.transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
        _orientation.rotation = Quaternion.Euler(0f, _rotationY, 0f);

        if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Joystick1Button8))
        {
            DoFov(45f);
            _isSlow = true;
            StartCoroutine(SlowMotionSequence());
        }
        else if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Joystick1Button8))
        {
            DoFov(60f);
            _isSlow = false;
            StopCoroutine(SlowMotionSequence());
            Time.timeScale = 1f;
        }
    }
    void ControlCameraInput()
    {
        _mouseX = Input.GetAxisRaw("Mouse X" );
        _mouseY = Input.GetAxisRaw("Mouse Y");

        _rotationY += _mouseX * _sensX * _multiplier;
        _rotationX -= _mouseY * _sensY * _multiplier;

        if (!_isSlow)
        {
            if (_isUpdated)
            {
                _rotationX = Mathf.Lerp(_rotationX,0,15f * Time.deltaTime);
            }
            else
            {
                _rotationX = Mathf.Clamp(_rotationX, -90, 90);
            }
        }
        else if (_isSlow)
        {
            _rotationX = Mathf.Clamp(_rotationX, -180, 180);
        }

        if (_rotationX > 90 || _rotationX < -90)
        {
            _isUpdated = true;
        }
        else if (_rotationX < 10f)
        {
            _isUpdated = false;
        }

        if (_rotationX < -90)
        {
            transform.Rotate(-_mouseX, 0, 0);
        }
    }

    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
    public void DoTiltX(float xTilt)
    {
        transform.DOLocalRotate(new Vector3(xTilt, 0, 0), 0.25f);
    }

    public IEnumerator SlowMotionSequence()
    {
        Time.timeScale = _slowMotionScale;
        yield return null;
    }
}
