using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStateManager _playerStateManager;
    private PlayerMovement _playerMovement;
    private PlayerJump _playerJump;
    private PlayerInput _playerInput;

    void Start()
    {
        _playerStateManager = GetComponent<PlayerStateManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerJump = GetComponent<PlayerJump>();
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {    
        StateSetter();
        StateHandler();
    }

    private void FixedUpdate()
    {
        _playerMovement.MovePlayer(_playerInput.GetHorizontalInput(), _playerInput.GetVerticalInput());
    }

    private void StateSetter()
    {
        if (_playerInput.IsJumping() && _playerMovement.IsGrounded)
        {
            _playerStateManager.CurrentState = PlayerStateManager.PlayerState.jumping;
        }
        else if (_playerMovement.IsGrounded && Input.GetKey(_playerInput.SprintKey))
        {
            _playerStateManager.CurrentState = PlayerStateManager.PlayerState.sprinting;
        }
        else if (_playerMovement.IsGrounded && ((_playerInput.GetHorizontalInput() > 0) || (_playerInput.GetVerticalInput() > 0)))
        {
            _playerStateManager.CurrentState = PlayerStateManager.PlayerState.walking;
        }
        else if (!_playerMovement.IsGrounded)
        {
            _playerStateManager.CurrentState = PlayerStateManager.PlayerState.air;
        }
        else 
        {
            _playerStateManager.CurrentState = PlayerStateManager.PlayerState.idle;
        }
    }

    private void StateHandler()
    {
        // Perform actions depending on the current state
        switch (_playerStateManager.CurrentState)
        {
            case PlayerStateManager.PlayerState.idle:
                break;

            case PlayerStateManager.PlayerState.walking:
                _playerMovement.MoveSpeed = _playerMovement.WalkSpeed;
                break;

            case PlayerStateManager.PlayerState.sprinting:
                _playerMovement.MoveSpeed = _playerMovement.SprintSpeed;
                break;

            case PlayerStateManager.PlayerState.jumping:
                _playerJump.Jump();
                break;

            case PlayerStateManager.PlayerState.air:
                break;

            // case PlayerStateManager.PlayerState.wallrunning:
            //     break;
        }
    }
}
