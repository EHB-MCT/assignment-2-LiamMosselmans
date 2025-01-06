using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public bool IsCameraInputEnabled = true;

    [Header("Camera")]
    [SerializeField] private Transform _orientation;
    [SerializeField] private float _sensitivityX = 400f;
    [SerializeField] private float _sensitivityY = 400f;

    private float _xRotation;
    private float _yRotation;

    private ICameraInputProvider _inputProvider;

    private void Awake()
    {
        _inputProvider = new UnityCameraInputProvider();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (IsCameraInputEnabled)
        {
            CalculateCameraRotation();
            RotateCamera();
        }
    }

    private void CalculateCameraRotation()
    {
        float mouseX = _inputProvider.GetMouseX() * Time.deltaTime * _sensitivityX;
        float mouseY = _inputProvider.GetMouseY() * Time.deltaTime * _sensitivityY;

        _yRotation += mouseX;
        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
    }

    private void RotateCamera()
    {
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}