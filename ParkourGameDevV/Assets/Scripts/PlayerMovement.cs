using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed;
    public float SprintSpeed;
    public float WalkSpeed;
    public float WallRunSpeed;
    [SerializeField] private float _groundDrag;

    [Space(10)]
    [Header("Grounded Check")]
    public bool IsGrounded;
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsLedge;

    [Space(10)]
    [SerializeField] private Transform _orientation;
    private Vector3 _moveDirection;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        GroundedCheck();
        AddDragForce();
        SpeedControl();
    }

    public void MovePlayer(float horizontalInput, float verticalInput)
    {
        _moveDirection = _orientation.forward * verticalInput + _orientation.right * horizontalInput;

        if (IsGrounded)
        {
            _rb.AddForce(_moveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);
        }
    }

    public void GroundedCheck()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _whatIsGround) || Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _whatIsLedge);
        Debug.DrawRay(transform.position, Vector3.down * (_playerHeight * 0.5f + 0.2f), Color.red);          
    }

    public void AddDragForce()
    {
        if (IsGrounded)
        {
            _rb.drag = _groundDrag;
        }
        else
        {
            _rb.drag = 0;
        }
    }

    public void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        // Limit velocity if needed
        if (flatVelocity.magnitude > MoveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * MoveSpeed;
            _rb.velocity = new Vector3(limitedVelocity.x, _rb.velocity.y, limitedVelocity.z);
        }
    }
}