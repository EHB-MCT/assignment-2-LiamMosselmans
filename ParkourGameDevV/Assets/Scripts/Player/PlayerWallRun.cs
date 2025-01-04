using System;
using UnityEngine;

public class PlayerWallRun : MonoBehaviour
{
    public event Action OnWallRunEnd;

    [Space(10)]
    [Header("Grounded Check")]
    [SerializeField] private float _playerHeight = 2f;
    [SerializeField] private LayerMask _whatIsGround;
    private bool _isGrounded;

    private WallDetector _wallDetector;
    private PlayerWallRunMovement _playerWallRunMovement;
    private GroundChecker _groundChecker;

    private void Start()
    {
        _wallDetector = GetComponent<WallDetector>();
        _playerWallRunMovement = GetComponent<PlayerWallRunMovement>();
        _groundChecker = new GroundChecker(transform, _playerHeight, _whatIsGround);
    }

    private void Update()
    {
        _isGrounded = _groundChecker.IsGrounded();
    }

    public bool IsWallRunning(float verticalInput)
    {
        if (_wallDetector.IsWallDetected() && verticalInput > 0 && !_isGrounded)
        {
            return true;
        }
        else
        {
            OnWallRunEnd?.Invoke();
            return false;
        }
    }

    public void PerformWallRun(float horizontalInput)
    {
        if (_wallDetector.IsWallDetectedLeft || _wallDetector.IsWallDetectedRight)
        {
            Vector3 wallNormal = _wallDetector.RightWallHit.normal;
            _playerWallRunMovement.PerformWallRunMovement(wallNormal, _wallDetector.IsWallDetectedRight, horizontalInput);
        }
    }
}