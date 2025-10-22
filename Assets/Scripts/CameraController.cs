using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _mouseSpeed = 100f;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private float _defaultFov;
    [SerializeField] private float _aimFov;

    private Camera _camera;
    private float rotX = 0f;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _camera.fieldOfView = _defaultFov;

    }

    void Update()
    {
        float mouseInputX = Input.GetAxis("Mouse X") * _mouseSpeed * Time.deltaTime;
        float mouseInputY = Input.GetAxis("Mouse Y") * _mouseSpeed * Time.deltaTime;

        rotX -= mouseInputY;
        rotX = Mathf.Clamp(rotX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);
        _playerBody.Rotate(Vector3.up * mouseInputX);
    }

    public void ManageAim(bool isAiming)
    {
        if (isAiming)
        {
            _camera.fieldOfView = _aimFov;

        } else 
        { 
            _camera.fieldOfView = _defaultFov;
        }
    }
}
