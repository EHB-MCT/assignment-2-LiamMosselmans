using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRun : MonoBehaviour
{
    // Event to notify when the pull-up action ends
    public event Action WallRunEnded;
    public float WallRunTimer;

    [Header("Wallrunning")]
    [SerializeField] private LayerMask _whatIsWall;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _wallRunForce;
    [SerializeField] private float _maxWallRunTime = 0.8f;

    [Space(10)]
    [Header("Detection")]
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private float _minJumpHeight;
    private RaycastHit _leftWallHit;
    private RaycastHit _rightWallHit;
    private bool _isWallLeft;
    private bool _isWallRight;

    [Space(10)]
    [Header("References")]
    [SerializeField] private Transform _orientation;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckForWall();
    }

    private void CheckForWall()
    {
        _isWallRight = Physics.Raycast(transform.position, _orientation.right, out _rightWallHit, _wallCheckDistance, _whatIsWall);
        _isWallLeft = Physics.Raycast(transform.position, -_orientation.right, out _leftWallHit, _wallCheckDistance, _whatIsWall);
    }

    private bool IsAboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, _minJumpHeight, _whatIsGround);
    }

    public bool UpdateWallRunningState(float verticalInput, bool isWallRunning)
    {
        // State 1 - Wallrunning
        if ((_isWallLeft || _isWallRight) && verticalInput > 0 && IsAboveGround())
        {
            if (!isWallRunning)
            {
                isWallRunning = true;
            }
        }
        // State 2 - Not wallrunning
        else
        {
            if (isWallRunning)
            {
                isWallRunning = false;
                StopWallRun();
            }
        }

        return isWallRunning;
    }

    public void PerformWallRunMovement(float horizontalInput)
    {
        WallRunTimer += Time.deltaTime;

        if (WallRunTimer > _maxWallRunTime)
        {
            _rb.useGravity = true;
        }

        Vector3 wallNormal = _isWallRight ? _rightWallHit.normal : _leftWallHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((_orientation.forward - wallForward).magnitude > (_orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        // Forward force
        _rb.AddForce(wallForward * _wallRunForce, ForceMode.Force);

        // Push player to wall
        if (!(_isWallLeft && horizontalInput > 0) && !(_isWallRight && horizontalInput < 0))
        {
            _rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }
    }

    public void StopWallRun()
    {
        WallRunEnded?.Invoke();
    }
}