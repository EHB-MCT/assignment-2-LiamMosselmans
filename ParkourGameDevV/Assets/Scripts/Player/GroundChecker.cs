using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private readonly Transform _playerTransform;
    private readonly float _playerHeight;
    private readonly LayerMask _groundLayer;

    public GroundChecker(Transform playerTransform, float playerHeight, LayerMask groundLayer)
    {
        _playerTransform = playerTransform;
        _playerHeight = playerHeight;
        _groundLayer = groundLayer;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(_playerTransform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }
}
