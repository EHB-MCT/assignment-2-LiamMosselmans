using UnityEngine;

public class PlayerStateHolder : MonoBehaviour
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
            }
        }
    }

    void Start()
    {
        CurrentState = PlayerState.idle;
    }
}
