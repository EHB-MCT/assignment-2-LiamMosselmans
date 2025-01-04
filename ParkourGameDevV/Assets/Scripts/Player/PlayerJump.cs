using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jumping")]
    public bool CanJump = true;
    public float JumpForce = 4f;

    private float _jumpCooldown = 0.2f;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public void Jump()
    {
        if (!CanJump) return;

        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        _rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);

        CanJump = false;
        StartCoroutine(ResetJumpAfterCooldown());
    }

    private IEnumerator ResetJumpAfterCooldown()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        CanJump = true;
    }
}