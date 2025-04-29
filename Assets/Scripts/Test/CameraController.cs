using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Camera cam;

    public float moveSpeed;
    public float rotationSpeed;

    private Vector2 moveInput;
    private Vector2 rotationInput;

    private Vector3 velocity;
    private float pitch;
    private float yaw;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MoveCamera();
        RotateCamera();
    }

    private void RotateCamera()
    {
        yaw += rotationInput.x * rotationSpeed;
        pitch -= rotationInput.y * rotationSpeed;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void MoveCamera()
    {
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnRotate(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }
}
