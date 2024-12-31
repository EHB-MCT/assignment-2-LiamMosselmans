using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public enum PlayerState
    {
        idle,
        walking,
        sprinting,
        jumping,
        wallrunning,
        air
    }

    private PlayerState _currentState;

    public PlayerState CurrentState
    {
        get => _currentState;
        set
        {
            if (_currentState != value)
            {
                _currentState = value;
                Debug.Log($"Player state changed to: {_currentState}");
            }
        }
    }

    void Start()
    {
        CurrentState = PlayerState.idle;
    }
}
