using UnityEngine;

public class WallDetector : MonoBehaviour, IWallDetector
{
    [Header("Detection")]
    public bool IsWallDetectedLeft;
    public bool IsWallDetectedRight;
    public RaycastHit LeftWallHit;
    public RaycastHit RightWallHit;
    [SerializeField] private LayerMask _whatIsWall;
    [SerializeField] private float _wallCheckDistance;

    [Header("References")]
    [SerializeField] private Transform _orientation;

    public bool IsWallDetected()
    {
        IsWallDetectedLeft = Physics.Raycast(transform.position, -_orientation.right, out LeftWallHit, _wallCheckDistance, _whatIsWall);
        IsWallDetectedRight = Physics.Raycast(transform.position, _orientation.right, out RightWallHit, _wallCheckDistance, _whatIsWall);

        if (IsWallDetectedLeft || IsWallDetectedRight)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}