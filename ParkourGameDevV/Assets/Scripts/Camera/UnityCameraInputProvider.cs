using UnityEngine;

public class UnityCameraInputProvider : ICameraInputProvider
{
    public float GetMouseX()
    {
        return Input.GetAxisRaw("Mouse X");
    }

    public float GetMouseY()
    {
        return Input.GetAxisRaw("Mouse Y");
    }
}