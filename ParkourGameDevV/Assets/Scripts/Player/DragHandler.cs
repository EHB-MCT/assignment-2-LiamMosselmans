using UnityEngine;

public class DragHandler : MonoBehaviour
{
    private readonly Rigidbody _rigidbody;

    public DragHandler(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void ApplyDrag(bool isGrounded, float groundDrag)
    {
        _rigidbody.drag = isGrounded ? groundDrag : 0;
    }
}
