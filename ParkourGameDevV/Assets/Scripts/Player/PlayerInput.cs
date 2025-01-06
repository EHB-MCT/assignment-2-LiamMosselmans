using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode DropDownKey = KeyCode.P;
    public KeyCode TabKey = KeyCode.Tab;
    public bool IsInputEnabled = true;

    public float GetHorizontalInput()
    {
        return IsInputEnabled ? Input.GetAxisRaw("Horizontal") : 0f;
    }

    public float GetVerticalInput()
    {
        return IsInputEnabled ? Input.GetAxisRaw("Vertical") : 0f;
    }

    public bool IsJumping()
    {
        return IsInputEnabled && Input.GetKeyDown(JumpKey);
    }
}