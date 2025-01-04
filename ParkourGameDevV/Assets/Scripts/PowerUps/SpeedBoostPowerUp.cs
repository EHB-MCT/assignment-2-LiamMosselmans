using UnityEngine;

public class SpeedBoostPowerUp : BasePowerUp
{
    [SerializeField] private float _speedMultiplier = 2f;
    private PlayerStateMachine _playerStateMachine;

    public override void Awake()
    {
        base.Awake();
        PowerUpName = "SpeedBoost";
    }

    public override void EnablePowerUpEffect()
    {
        _playerStateMachine = FindObjectOfType<PlayerStateMachine>();

        if (_playerStateMachine != null)
        {
            // Apply the speed boost effect (multiply speed)
            _playerStateMachine.SpeedMultiplier *= _speedMultiplier;
            Debug.Log($"{PowerUpName} effect enabled");
        }
        else
        {
            Debug.LogError("PlayerStateMachine not found!");
        }
    }
    public override void DisablePowerUpEffect()
    {
        if (_playerStateMachine != null)
        {
            // Reset the player's speed to normal (or the base speed)
            _playerStateMachine.SpeedMultiplier /= _speedMultiplier;
            Debug.Log($"{PowerUpName} effect disabled");
        }
    }
}