using System;
using System.Collections;
using UnityEngine;

public class PlayerLedgeGrab : MonoBehaviour
{
    // Event to notify when pull up action ends
    public event Action PullUpEnded;

    public bool IsHanging = false;
    public bool CanHang = true;

    [Header("Ledge Hanging")]
    public float HangCooldown;
    [SerializeField] private LayerMask _whatIsLedge;
    private Vector3 _pullUpStartPosition;
    private Vector3 _pullUpEndPosition;
    private float _pullUpTimer;
    private float _pullUpDuration = 0.8f; 
    private Vector3 _snappingPoint;
    private Vector3 _aboveLedge;

    [Header("Detection")]
    [SerializeField] private float _verticalRayDistance;
    [SerializeField] private float _verticalOffsetPositionX;
    [SerializeField] private float _verticalOffsetPositionY;
    [SerializeField] private float _verticalOffsetPositionZ;
    [SerializeField] private float _horizontalRayDistance;
    private RaycastHit _verticalLedgeHit;
    private Vector3 _verticalRayOffset;
    private Vector3 _verticalRayOrigin;
    private bool _ledgeTop;
    private RaycastHit _horizontalLedgeHit;
    private bool _ledgeFront;

    [Space(10)]
    [Header("References")]
    [SerializeField] private Transform _orientation;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(CanHang)
        {  
            CheckForLedge();
            UpdateHangingState();
        }
    }

    void ShootVerticalRay()
    {
        // Vertical raycast
        _verticalRayOffset = (_orientation.right * _verticalOffsetPositionX) + (_orientation.up * _verticalOffsetPositionY) + (_orientation.forward * _verticalOffsetPositionZ);
        _verticalRayOrigin = transform.position + _verticalRayOffset;
        _ledgeTop = Physics.Raycast(_verticalRayOrigin, Vector3.down, out _verticalLedgeHit, _verticalRayDistance, _whatIsLedge);
    }

    void ShootHorizontalRay()
    {
        // Horizontal raycast
        _ledgeFront = Physics.Raycast(transform.position, _orientation.forward, out _horizontalLedgeHit, _horizontalRayDistance, _whatIsLedge);
    }

    void CheckForLedge()
    {
        ShootVerticalRay();
        if(_ledgeTop)
        {
            ShootHorizontalRay();
        } 
    }

    public bool HangingStateValue()
    {
        return IsHanging;
    }

    public void UpdateHangingState()
    {
        if(!IsHanging)
        {
            if(_ledgeFront && _ledgeTop)
            {
            _snappingPoint = new Vector3(_horizontalLedgeHit.point.x, _verticalLedgeHit.point.y ,_horizontalLedgeHit.point.z);     
            SnapPlayerToLedge();    
            }
        }
    }

    void SnapPlayerToLedge()
    {
        // Step 1: Get the direction the player should face (opposite of the ledge normal)
        Vector3 facingDirection = -_horizontalLedgeHit.normal;

        // Step 2: Calculate the rotation based on the facing direction
        Quaternion targetRotation = Quaternion.LookRotation(facingDirection, Vector3.up);

        // Step 3: Smoothly rotate the player towards the target rotation (optional, for smoothness)
        transform.rotation = targetRotation;
        
        // Calculate initial snapping point based on raycast info
        Vector3 awayFromLedge = (transform.position - _snappingPoint).normalized;

        float backwardOffset = 0.7f; // Distance away from ledge
        float downwardOffset = 0.6f; // Amount to lower Y position

        // Move player to snapping point
        transform.position = _snappingPoint + new Vector3(awayFromLedge.x * backwardOffset, -downwardOffset, awayFromLedge.z * backwardOffset);
        _rb.velocity = new Vector3(0, 0, 0);

        CanHang = false;
        IsHanging = true;
    }

    public void ResetHang()
    {
        CanHang = true;
    }

    public void StartPlayerPullUp()
    {
        Debug.Log("Pull-up initiated.");
        float verticalOffset = 1.1f;
        float depthOffset = 1.2f;
        IsHanging = false;
        _pullUpStartPosition = transform.position;
        Vector3 pointAwayFromLedge = _horizontalLedgeHit.point + transform.forward * depthOffset;
        _aboveLedge = _verticalLedgeHit.point + _verticalLedgeHit.normal.normalized * verticalOffset;

        _pullUpEndPosition = new Vector3(pointAwayFromLedge.x, _aboveLedge.y, pointAwayFromLedge.z);

        // Start the pull-up coroutine
        StartCoroutine(PlayerPullUpAction());
    }

    IEnumerator PlayerPullUpAction()
    {
        _pullUpTimer = 0;
        while (_pullUpTimer < _pullUpDuration)
        {
            _pullUpTimer += Time.deltaTime / _pullUpDuration;
            transform.position = Vector3.Lerp(_pullUpStartPosition, _pullUpEndPosition, _pullUpTimer);

            // Wait for the next frame
            yield return null;
        }

        StopPullUp();
    }

    void StopPullUp()
    {
        PullUpEnded?.Invoke();
    }
}
