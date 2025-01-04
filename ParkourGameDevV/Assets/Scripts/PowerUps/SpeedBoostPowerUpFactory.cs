using UnityEngine;

public class SpeedBoostPowerUpFactory : PowerUpFactory
{
    public SpeedBoostPowerUp _speedBoostPowerUpPrefab;

    public override IPowerUp CreatePowerUp(Vector3 position)
    {
        SpeedBoostPowerUp newSpeedBoostPowerUp = Instantiate(_speedBoostPowerUpPrefab, position, Quaternion.identity);

        newSpeedBoostPowerUp.Initialize();

        return newSpeedBoostPowerUp;
    }
}