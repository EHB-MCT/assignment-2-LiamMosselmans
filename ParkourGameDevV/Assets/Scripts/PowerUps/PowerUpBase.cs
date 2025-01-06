using System.Collections;
using UnityEngine;

public abstract class BasePowerUp : MonoBehaviour, IPowerUp
{
    [SerializeField] protected float _duration = 5f;

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
        EnablePowerUpEffect();

        _meshRenderer.enabled = false;
        _collider.enabled = false;

        yield return new WaitForSeconds(Duration);

        DisablePowerUpEffect();

        Destroy(gameObject);
    }

    public abstract void EnablePowerUpEffect();
    public abstract void DisablePowerUpEffect();
}