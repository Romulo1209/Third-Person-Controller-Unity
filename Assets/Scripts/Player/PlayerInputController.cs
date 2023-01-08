using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] Vector2 movement;

    [Header("States")]
    [SerializeField] bool canMove = true;
    [Space]
    [SerializeField] bool isMoving;
    [SerializeField] bool isJumping;
    [SerializeField] bool isSprinting;
    [SerializeField] bool isBackpack;

    private PlayerController playerController;
    private PlayerInputActions playerInput;

    //Getters
    #region Getters

    public Vector2 Movement { get { return movement; } }
    public bool CanMove { get { return canMove; } }
    public bool IsMoving { get { return isMoving; } }
    public bool IsJumping { get { return isJumping; } }
    public bool IsSprinting { get { return isSprinting; } }
    public bool IsBackpack { get { return isBackpack; } }

    #endregion

    private void Awake() {
        playerController = GetComponent<PlayerController>();

        playerInput = new PlayerInputActions();
        playerInput.Player.Enable();

        playerInput.Player.Jump.performed += JumpStart;
        playerInput.Player.Jump.canceled += JumpEnd;

        playerInput.Player.Movement.performed += MoveStart;
        playerInput.Player.Movement.canceled += MoveEnd;

        playerInput.Player.Sprint.performed += SprintStart;
        playerInput.Player.Sprint.canceled += SprintEnd;

        playerInput.Player.Backpack.performed += BackpackStart;
        playerInput.Player.Backpack.canceled += BackpackEnd;
    }

    //Movement
    private void MoveStart(InputAction.CallbackContext context) {
        movement = context.ReadValue<Vector2>();
        isMoving = true;
    }
    private void MoveEnd(InputAction.CallbackContext context) {
        movement = context.ReadValue<Vector2>();
        isMoving = false;
    }

    //Jump
    private void JumpStart(InputAction.CallbackContext context) { 
        playerController.Jump();
        isJumping = true;
    }
    private void JumpEnd(InputAction.CallbackContext context) {
        isJumping = false;
    }

    //Sprint
    private void SprintStart(InputAction.CallbackContext context) {
        isSprinting = true;
    }
    private void SprintEnd(InputAction.CallbackContext context) {
        isSprinting = false;
    }

    //Backpack
    private void BackpackStart(InputAction.CallbackContext context) {
        isBackpack = true;
    }
    private void BackpackEnd(InputAction.CallbackContext context) {
        isBackpack= false;
    }
}
