using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public bool IsCameraInputEnabled = true;

    [Header("_orientation")]
    [SerializeField] private Transform _orientation;
    [SerializeField] private float _sensitivityX;
    [SerializeField] private float _sensitivityY;
    private float _xRotation;
    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (IsCameraInputEnabled)
        {
            // Get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * _sensitivityX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _sensitivityY;

            _yRotation += mouseX;
            _xRotation -= mouseY;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            // Rotate camera and orientation
            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
            _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
        }
    }
}