using System.Collections;
using UnityEngine;

public abstract class BasePowerUp : MonoBehaviour, IPowerUp
{
    [SerializeField] protected float _duration = 5f;
    // [SerializeField] protected GameObject _pickupEffect;

    public string PowerUpName { get; set; }
    public float Duration { get => _duration; set => _duration = value; }

    private MeshRenderer _meshRenderer;
    private Collider _collider;

    public virtual void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    public void Initialize()
    {
        gameObject.name = PowerUpName;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerObject"))
        {
            ActivatePowerUp();
        }
    }

    public void ActivatePowerUp()
    {
        StartCoroutine(HandlePowerUp());
    }

    public IEnumerator HandlePowerUp()
    {
        // Enable powerup effect
        EnablePowerUpEffect();

        // Disable the power-up object
        _meshRenderer.enabled = false;
        _collider.enabled = false;

        // Wait for the duration
        yield return new WaitForSeconds(Duration);

        // Disable powerup effect
        DisablePowerUpEffect();

        // Destroy the power-up object
        Destroy(gameObject);
    }

    public abstract void EnablePowerUpEffect();
    public abstract void DisablePowerUpEffect();
}