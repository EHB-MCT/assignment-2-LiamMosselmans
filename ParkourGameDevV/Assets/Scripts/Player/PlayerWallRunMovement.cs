using UnityEngine;

public class PlayerWallRunMovement : MonoBehaviour
{
    [SerializeField] private float _wallRunForce = 200f;
    [SerializeField] private float _maxWallRunTime = 0.8f;
    [SerializeField] private Transform _orientation;
    public float WallRunTimer = 0f;
    
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void PerformWallRunMovement(Vector3 wallNormal, bool isWallRight, float horizontalInput)
    {
        WallRunTimer += Time.deltaTime;

        if (WallRunTimer > _maxWallRunTime)
        {
            _rb.useGravity = true;
        }

        // Determine the wall's normal direction
        Vector3 wallNormalDirection = isWallRight ? wallNormal : -wallNormal;
        Vector3 wallForwardDirection = Vector3.Cross(wallNormalDirection, transform.up);

        // Determine the direction the player should move (towards or away from the wall)
        if ((_orientation.forward - wallForwardDirection).magnitude > (_orientation.forward - -wallForwardDirection).magnitude)
        {
            wallForwardDirection = -wallForwardDirection;
        }

        // Apply the forward movement force towards the wall
        _rb.AddForce(wallForwardDirection * _wallRunForce, ForceMode.Force);

        // Push the player towards the wall based on horizontal input
        if (!(isWallRight && horizontalInput < 0) && !(horizontalInput > 0))
        {
            _rb.AddForce(-wallNormal * 100f, ForceMode.Force);
        }
    }
}