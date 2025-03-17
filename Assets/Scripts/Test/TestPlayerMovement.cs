using System;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    [SerializeField]
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 90f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;

    private Inventory inventory;
    private WeaponHandler weaponHandler;
    private StatsHandler stats;
    private EventHandler eventHandler;
    private EquipmentHandler equipmentHandler;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inventory = GetComponent<Inventory>();
        weaponHandler = GetComponent<WeaponHandler>();
        stats = GetComponent<StatsHandler>();
        eventHandler = GetComponent<EventHandler>();
        equipmentHandler = GetComponent<EquipmentHandler>();
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;

        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        DebugInputs();

    }

    private void DebugInputs()
    {
        UnequipItem();
        UseWeapon();
        ReloadWeapon();
        UseItem();
    }

    private void ReloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && equipmentHandler.currentWeapon != null)
        {
            weaponHandler.ReloadWeapon();
        }
    }

    private void UseWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && equipmentHandler.currentWeapon != null)
        {
            weaponHandler.Shoot();
        }
    }

    private void UnequipItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

        }
    }

    private void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            inventory.UseItem(ConsumableType.Healing);
        if (Input.GetKeyDown(KeyCode.X))
            inventory.UseItem(ConsumableType.Food);
        if (Input.GetKeyDown(KeyCode.C))
            inventory.UseItem(ConsumableType.Water);

    }

    
}
