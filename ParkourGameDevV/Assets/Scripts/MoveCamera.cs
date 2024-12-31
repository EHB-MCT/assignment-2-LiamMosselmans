using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("Camera Position")]
    [SerializeField] private Transform _cameraPosition;

    private void Update()
    {
        transform.position = _cameraPosition.position;
    }
}