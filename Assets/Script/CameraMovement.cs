using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 400f;
    public Transform playerBody;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        Vector2 delta = Mouse.current.delta.ReadValue();
        float mouseX = delta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = delta.y * mouseSensitivity * Time.deltaTime;

        // ���� ȸ�� (ī�޶� pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // �� ���� ����
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // �¿� ȸ�� (�÷��̾� yaw)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
