using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public float SpeedMultiplier = 1f;

    private PlayerInput _playerInput;
    private PlayerStateHolder _playerStateHolder;
    private PlayerMovement _playerMovement;
    private PlayerJump _playerJump;
    private PlayerWallRun _playerWallRun;
    private PlayerWallRunMovement _playerWallRunMovement;
    private Rigidbody _rb;
    private bool _isWallRunning;
    private bool _isPerformingAction = false;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerStateHolder = GetComponent<PlayerStateHolder>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerJump = GetComponent<PlayerJump>();
        _playerWallRun = GetComponent<PlayerWallRun>();
        _playerWallRunMovement = GetComponent<PlayerWallRunMovement>();
        _rb = GetComponent<Rigidbody>();

        _playerWallRun.OnWallRunEnd += StopPerformingAction;
    }

    void Update()
    {   
        if(!_isPerformingAction)
        {
            StateSetter();
            StateHandler();
        }

        _isWallRunning = _playerWallRun.IsWallRunning(_playerInput.GetVerticalInput());
    }

    private void FixedUpdate()
    {
        _playerMovement.MovePlayer(_playerInput.GetHorizontalInput(), _playerInput.GetVerticalInput());

        if (_isWallRunning)
        {
            _playerWallRun.PerformWallRun(_playerInput.GetHorizontalInput());
        }

    }
    private void StateSetter()
    {
        if (_playerInput.IsJumping() && _playerMovement.IsGrounded)
        {
            _playerStateHolder.CurrentState = PlayerStateHolder.PlayerState.jumping;
        }
        else if (_isWallRunning)
        {
            _playerStateHolder.CurrentState = PlayerStateHolder.PlayerState.wallrunning;
        }
        else if (_playerMovement.IsGrounded && Input.GetKey(_playerInput.SprintKey))
        {
            _playerStateHolder.CurrentState = PlayerStateHolder.PlayerState.sprinting;
        }
        else if (_playerMovement.IsGrounded && ((_playerInput.GetHorizontalInput() > 0) || (_playerInput.GetVerticalInput() > 0)))
        {
            _playerStateHolder.CurrentState = PlayerStateHolder.PlayerState.walking;
        }
        else if (!_playerMovement.IsGrounded)
        {
            _playerStateHolder.CurrentState = PlayerStateHolder.PlayerState.air;
        }
        else 
        {
            _playerStateHolder.CurrentState = PlayerStateHolder.PlayerState.idle;
        }
    }

    private void StateHandler()
    {
        // Perform actions depending on the current state
        switch (_playerStateHolder.CurrentState)
        {
            case PlayerStateHolder.PlayerState.jumping:
                _playerJump.Jump();
                break;

            case PlayerStateHolder.PlayerState.wallrunning:
                _isPerformingAction = true;
                Vector3 velocity = _rb.velocity;
                velocity.y = 0;
                _rb.velocity = velocity;
                _rb.useGravity = false;
                _playerWallRunMovement.WallRunTimer = 0f;
                break;

            case PlayerStateHolder.PlayerState.sprinting:
                _playerMovement.MoveSpeed = _playerMovement.SprintSpeed * SpeedMultiplier;
                break;

            case PlayerStateHolder.PlayerState.walking:
                _playerMovement.MoveSpeed = _playerMovement.WalkSpeed * SpeedMultiplier;
                break;

            case PlayerStateHolder.PlayerState.air:
                break;

            case PlayerStateHolder.PlayerState.idle:
                break;
        }
    }

    private void StopPerformingAction()
    {
        _isPerformingAction = false;
        _rb.useGravity = true;
    }
}
