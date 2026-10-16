using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float InputAxisX;
    public float InputAxisY;
    public bool InputMouse_0;
    public bool InputMouseDown_0;

    void Update()
    {
        if (Time.timeScale == 0) return;

        InputAxisX = Input.GetAxisRaw("Horizontal");
        InputAxisY = Input.GetAxisRaw("Vertical");
        InputMouseDown_0 = Input.GetMouseButtonDown(0);
        InputMouse_0 = Input.GetMouseButton(0);
    }
}
