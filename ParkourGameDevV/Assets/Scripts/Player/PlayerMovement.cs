using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed;
    public float SprintSpeed;
    public float WalkSpeed;
    public float WallRunSpeed;
    public bool IsGrounded;
    [SerializeField] private float _groundDrag;
    [SerializeField] private float _airMultiplier = 0.4f;

    [Space(10)]
    [Header("Grounded Check")]
    [SerializeField] private float _playerHeight = 2f;
    [SerializeField] private LayerMask _whatIsGround;

    [Space(10)]
    [SerializeField] private Transform _orientation;

    private Rigidbody _rb;
    private GroundChecker _groundChecker;
    private DragHandler _dragHandler;

    private Vector3 _moveDirection;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        // Initialize services
        _groundChecker = new GroundChecker(transform, _playerHeight, _whatIsGround);
        _dragHandler = new DragHandler(_rb);
    }

    private void Update()
    {
        IsGrounded = _groundChecker.IsGrounded();
        _dragHandler.ApplyDrag(IsGrounded, _groundDrag);
        SpeedControl();
    }

    public void MovePlayer(float horizontalInput, float verticalInput)
    {
        _moveDirection = _orientation.forward * verticalInput + _orientation.right * horizontalInput;

        if (IsGrounded)
        {
            _rb.AddForce(_moveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);
        }
        else if (!IsGrounded)
        {
            _rb.AddForce(_moveDirection.normalized * MoveSpeed * 10f * _airMultiplier, ForceMode.Force);
        }
    }

    public void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        // Limit velocity if needed
        if (flatVelocity.magnitude > MoveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * MoveSpeed;
            _rb.velocity = new Vector3(limitedVelocity.x, _rb.velocity.y, limitedVelocity.z);
        }
    }
}