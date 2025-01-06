using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private SpeedBoostPowerUpFactory _speedBoostPowerUpFactory;
    [SerializeField] private Vector3[] _spawnLocations;

    void Start()
    {
        SpawnSpeedBoostPowerUps();
    }

    public void SpawnSpeedBoostPowerUps()
    {
        foreach (Vector3 location in _spawnLocations)
        {
            _speedBoostPowerUpFactory.CreatePowerUp(location);
        }
    }
}
