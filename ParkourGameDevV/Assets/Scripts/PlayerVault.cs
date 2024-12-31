using System;
using UnityEngine;

public class PlayerVault : MonoBehaviour
{
    // Event to notify when vaulting ends
    public event Action VaultEnded;

    [Header("Vaulting")]
    [SerializeField] private LayerMask _obstacleLayerMask;
    private float _vaultTimer;
    private float _vaultDuration = 1f;
    private Vector3 _vaultStartPosition;
    private Vector3 _vaultEndPosition;
    private bool _isVaulting = false;
    private float _halfVaultDuration;

    [Space(10)]
    [Header("Detection")]
    [SerializeField] private float _obstacleCheckDistance;
    private RaycastHit _obstacleHit;
    private bool _isObstacleInFront;

    [Space(10)]
    [Header("References")]
    [SerializeField] private Transform _orientation;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Camera _camera;

    [Space(10)]
    [Header("Camera")]
    [SerializeField] private float _cameraTiltAmount = 10f;
    private Quaternion _initialCameraRotation;
    private Quaternion _targetTiltRotation;
    private float _cameraTiltTimer;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        CheckForObstacle();

        // Camera tilt while vaulting
        if (_isVaulting)
        {
            _vaultTimer += Time.deltaTime;

            // Manage camera tilt separately
            _cameraTiltTimer += Time.deltaTime;

            if (_vaultTimer <= _halfVaultDuration)
            {
                // First half of the vault: tilt to maximum rotation
                float t = _cameraTiltTimer / (_halfVaultDuration / 2); // Normalize for tilt
                _camera.transform.localRotation = Quaternion.Slerp(_initialCameraRotation, _targetTiltRotation, t);
            }
            else if (_vaultTimer <= _vaultDuration)
            {
                // Second half of the vault: tilt back to initial rotation
                float t = (_cameraTiltTimer - (_halfVaultDuration / 2)) / (_halfVaultDuration / 2); // Normalize for tilt
                _camera.transform.localRotation = Quaternion.Slerp(_targetTiltRotation, _initialCameraRotation, t);
            }
        }
    }

    private void CheckForObstacle()
    {
        _isObstacleInFront = Physics.Raycast(transform.position, _orientation.forward, out _obstacleHit, _obstacleCheckDistance, _obstacleLayerMask);
    }

    public bool UpdateVaultingState(float verticalInput, bool isJumping, bool isGrounded)
    {
        if (isJumping && _isObstacleInFront && verticalInput > 0 && isGrounded)
        {
            if (!_isVaulting)
            {
                StartVault();
            }
        }
        return _isVaulting;
    }

    public void StartVault()
    {
        _isVaulting = true;
        _vaultStartPosition = transform.position;
        Vector3 behindObstacle = _obstacleHit.point - _obstacleHit.normal * 3f;
        _vaultEndPosition = behindObstacle;
        _vaultTimer = 0;
        _cameraTiltTimer = 0;

        _initialCameraRotation = _camera.transform.localRotation;
        _targetTiltRotation = _initialCameraRotation * Quaternion.Euler(0, 0, _cameraTiltAmount);
        _halfVaultDuration = _vaultDuration / 2;

        if (_animator != null)
        {
            _animator.Play("PlayerVaultAnimation");
        }
    }

    public void PerformVaultAction()
    {
        _vaultTimer += Time.deltaTime / _vaultDuration;
        transform.position = Vector3.Lerp(_vaultStartPosition, _vaultEndPosition, _vaultTimer);

        if (_vaultTimer >= 1)
        {
            StopVault();
        }
    }

    public void StopVault()
    {
        _isVaulting = false;
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);

        // Reset camera rotation to ensure it's back to original rotation after vault
        _camera.transform.localRotation = _initialCameraRotation;

        // Trigger event when vaulting ends
        VaultEnded?.Invoke();
    }
}