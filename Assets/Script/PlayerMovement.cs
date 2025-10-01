using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move speeds")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float slowSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = walkSpeed;
        if (Keyboard.current.leftShiftKey.isPressed) currentSpeed = runSpeed;
        else if (Keyboard.current.leftCtrlKey.isPressed) currentSpeed = slowSpeed;

        // === 이동 입력 (WASD) ===
        Vector3 move = Vector3.zero;
        if (Keyboard.current.wKey.isPressed) move += transform.forward;
        if (Keyboard.current.sKey.isPressed) move -= transform.forward;
        if (Keyboard.current.aKey.isPressed) move -= transform.right;
        if (Keyboard.current.dKey.isPressed) move += transform.right;

        transform.position += move.normalized * currentSpeed * Time.deltaTime;
    }
}
