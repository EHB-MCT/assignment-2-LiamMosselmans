using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        walking,
        sprinting,
        jumping,
        wallrunning,
        vaulting,
        hanging,
        air
    }
    
    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;
    private PlayerWallRun _playerWallRun;
    private PlayerVault _playerVault;
    private PlayerLedgeGrab _playerLedgeGrab;
    private PlayerCamera _playerCamera;
    private PlayerState _playerState;

    private bool _isPerformingAction = false;
    private bool _isVaulting;
    private bool _isWallrunning;
    private bool _isHanging;

    private Rigidbody _rb;

    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInput>();
        _playerWallRun = GetComponent<PlayerWallRun>();
        _playerVault = GetComponent<PlayerVault>();
        _playerLedgeGrab = GetComponent<PlayerLedgeGrab>();
        _playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCamera>();
        _rb = GetComponent<Rigidbody>();

        _playerVault.VaultEnded += StopPerformingAction;
        _playerLedgeGrab.PullUpEnded += StopPerformingAction;
        _playerWallRun.WallRunEnded += StopPerformingAction;
    }

    void Update()
    {
        _playerMovement.GroundedCheck();
        _playerMovement.SpeedControl();
        _playerMovement.AddDragForce();        
        _isWallrunning = _playerWallRun.UpdateWallRunningState(_playerInput.GetVerticalInput(), _isWallrunning);
        _isVaulting = _playerVault.UpdateVaultingState(_playerInput.GetVerticalInput(), _playerInput.IsJumping(), _playerMovement.IsGrounded);
        _isHanging = _playerLedgeGrab.HangingStateValue();

        if (!_isPerformingAction)
        {
            StateHandler();
        }
        else if (_playerState == PlayerState.vaulting)
        {
            _playerVault.PerformVaultAction();
        }
        else if (_playerState == PlayerState.hanging)
        {
            if(Input.GetKeyDown(_playerInput.DropDownKey))
            {
                PlayerDropDown();
            }
            if(Input.GetKeyDown(_playerInput.JumpKey))
            {
                _playerCamera.IsCameraInputEnabled = false;
                _playerLedgeGrab.StartPlayerPullUp();
                StartCoroutine(ResetHangAfterCooldown());
            }
        }
        else if (_playerState == PlayerState.wallrunning)
        {
            if(_playerInput.IsJumping() && _playerMovement.CanJump)
            {
                Debug.Log("Attempting wall jump.");
                PlayerJump();
            }
        }
    }

    private void FixedUpdate()
    {
        _playerMovement.MovePlayer(_playerInput.GetHorizontalInput(), _playerInput.GetVerticalInput());

        if (_isWallrunning)
        {
            _playerWallRun.PerformWallRunMovement(_playerInput.GetHorizontalInput());
        }
    }

    private void PlayerJump()
    {
        _playerMovement.CanJump = false;
        _playerMovement.Jump();
        StartCoroutine(ResetJumpAfterCooldown());
    }

    IEnumerator ResetJumpAfterCooldown()
    {
        yield return new WaitForSeconds(_playerMovement.JumpCooldown);
        _playerMovement.ResetJump();
    }

    private void PlayerDropDown()
    {
        _playerState = PlayerState.air;
        _isPerformingAction = false;
        _playerLedgeGrab.IsHanging = false;
        _playerLedgeGrab.CanHang = false;
        _rb.useGravity = true;
        _playerInput.IsInputEnabled = true;
        StartCoroutine(ResetHangAfterCooldown());
    }

    IEnumerator ResetHangAfterCooldown()
    {
        yield return new WaitForSeconds(_playerLedgeGrab.HangCooldown);
        _playerLedgeGrab.ResetHang();
    }

    private void StateHandler()
    {
        if (_isVaulting)
        {
            _playerState = PlayerState.vaulting;
            _isPerformingAction = true;
            _playerCamera.IsCameraInputEnabled = false;
        }
        else if(_isHanging)
        {
            _playerState = PlayerState.hanging;
            _isPerformingAction = true;
            _rb.useGravity = false;
            _playerInput.IsInputEnabled = false;
        }
        else if (_isWallrunning)
        {
            _rb.useGravity = false;
            _playerState = PlayerState.wallrunning;
            _playerMovement.MoveSpeed = _playerMovement.WallRunSpeed;
            _isPerformingAction = true;
            _playerWallRun.WallRunTimer = 0;
        }
        else if (_playerInput.IsJumping() && _playerMovement.CanJump && _playerMovement.IsGrounded)
        {
            _playerState = PlayerState.jumping;
            PlayerJump();
        }
        else if (_playerMovement.IsGrounded && Input.GetKey(_playerInput.SprintKey))
        {
            _playerState = PlayerState.sprinting;
            _playerMovement.MoveSpeed = _playerMovement.SprintSpeed;
        }
        else if (_playerMovement.IsGrounded) 
        {
            _playerState = PlayerState.walking;
            _playerMovement.MoveSpeed = _playerMovement.WalkSpeed;
        }
        else
        {
            _playerState = PlayerState.air;
        }
    }

    private void StopPerformingAction()
    {
        _isPerformingAction = false;
        _playerCamera.IsCameraInputEnabled = true;
        _rb.useGravity = true;
        _playerInput.IsInputEnabled = true;
    }
}
