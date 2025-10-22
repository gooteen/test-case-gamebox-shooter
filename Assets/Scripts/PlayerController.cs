using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfiguration _playerConfig;

    // Ground contact registration settings 
    [SerializeField] private Transform _groundCheckSphereLocation;
    [SerializeField] private LayerMask _groundCheckSphereLayerMask;
    [SerializeField] private float _groundCheckSphereRadius = 0.4f;
    [SerializeField] private Weapon _equippedWeapon;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private GameObject _throwableObjectPrefab;

    private CharacterController _controller;
    private bool _isGrounded;

    private float _currentHorizontalSpeed;
    private float _currentVerticalSpeed;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _currentHorizontalSpeed = _playerConfig.walkingSpeed;
    }

    void Update()
    {
        // Checking if the player is grounded
        _isGrounded = Physics.CheckSphere(_groundCheckSphereLocation.position, _groundCheckSphereRadius, _groundCheckSphereLayerMask);

        Move();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(1))
        {
            _cameraController.ManageAim(true);
            _currentHorizontalSpeed = _isGrounded ? _currentHorizontalSpeed * _playerConfig.aimingDampMultiplier : _currentHorizontalSpeed;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _cameraController.ManageAim(false);
            _currentHorizontalSpeed = _isGrounded ? _playerConfig.walkingSpeed : _currentHorizontalSpeed;
        }

        if (_equippedWeapon != null)
            _equippedWeapon.Shoot();

        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowObject();
        }
    }

    private void Move()
    {
        // Simulating gravity
        _currentVerticalSpeed += _playerConfig.gravity * Time.deltaTime;

        float horizontalSpeed;
        horizontalSpeed = _isGrounded ? _currentHorizontalSpeed : _currentHorizontalSpeed * _playerConfig.airSpeedDampMultiplier;

        Debug.Log(horizontalSpeed);

        float movementX = Input.GetAxis("Horizontal");
        float movementZ = Input.GetAxis("Vertical");

        Vector3 direction = horizontalSpeed * (transform.forward * movementZ + transform.right * movementX) + transform.up * _currentVerticalSpeed;

        _controller.Move(direction * Time.deltaTime);
    }

    private void Jump()
    {
        _currentVerticalSpeed = _isGrounded ? Mathf.Sqrt(_playerConfig.jumpHeight * -2f * _playerConfig.gravity) : _currentVerticalSpeed;
    }

    private void ThrowObject()
    {
        

        Vector3 cameraForward = _cameraController.transform.forward;
        Vector3 cameraRight = _cameraController.transform.right;

        Vector3 throwDir = Quaternion.AngleAxis(-_playerConfig.throwAngleDegrees, cameraRight) * cameraForward;

        // 2) спавн и добавление силы
        GameObject proj = Instantiate(_throwableObjectPrefab, _cameraController.transform.position, Quaternion.LookRotation(throwDir));
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // рекомендуемый режим: импульс (вес объекта учитывается)
            rb.AddForce(throwDir.normalized * 30f, ForceMode.Impulse);

            // альтернативно: задать скорость напрямую
            // rb.velocity = throwDir.normalized * throwForce;
        }
        else
        {
            Debug.LogWarning("ThrowObject: prefab has no Rigidbody.");
        }
    }
}
