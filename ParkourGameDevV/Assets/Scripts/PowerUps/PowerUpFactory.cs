using UnityEngine;

public abstract class PowerUpFactory : MonoBehaviour
{
    public abstract IPowerUp CreatePowerUp(Vector3 position);
}
