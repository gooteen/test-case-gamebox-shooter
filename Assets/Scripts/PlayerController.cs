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

    private CharacterController _controller;
    private bool _isGrounded;
    private bool _isSprinting;

    private float _currentHorizontalSpeed;
    private float _currentVerticalSpeed;

    void Start()
    {
        _isSprinting = false;
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

        ManageSprinting();

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

    private void ManageSprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _isSprinting = true;
        }
        else
        {
            _isSprinting = false;
        }

        if (_isGrounded)
        {
            if (_isSprinting)
            {
                _currentHorizontalSpeed = _playerConfig.sprintingSpeed;
            }
            else
            {
                _currentHorizontalSpeed = _playerConfig.walkingSpeed;
            }
        }
    }
}
