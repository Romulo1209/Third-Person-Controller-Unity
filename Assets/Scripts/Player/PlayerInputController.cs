using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] Vector2 movement;

    [Header("States")]
    [SerializeField] bool canMove = true;
    [Space]
    [SerializeField] bool isMoving;
    [SerializeField] bool isJumping;
    [SerializeField] bool isSprinting;
    [SerializeField] bool isInteracting;
    [Space]
    [SerializeField] bool inBackpack;
    [SerializeField] bool inPause;

    [Space(15)]

    [SerializeField] private CinemachineFreeLook cameraBrain;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WindowController windowController;

    private PlayerInputActions playerInput;

    [HideInInspector] public UnityEvent InteractEvent;

    #region Getters

    public Vector2 Movement { get { return movement; } }
    public bool CanMove { get { return canMove; } }
    public bool IsMoving { get { return isMoving; } }
    public bool IsJumping { get { return isJumping; } }
    public bool IsSprinting { get { return isSprinting; } }
    public bool IsInteracting { get { return isInteracting; } }
    public bool InBackpack { get { return inBackpack; } }
    public bool InPause { get { return inPause; } }

    #endregion

    private void Awake() {
        Application.targetFrameRate = 0;

        CheckIfCanMove();

        playerInput = new PlayerInputActions();
        playerInput.Player.Enable();

        playerInput.Player.Jump.performed += JumpStart;
        playerInput.Player.Jump.canceled += JumpEnd;

        playerInput.Player.Movement.performed += MoveStart;
        playerInput.Player.Movement.canceled += MoveEnd;

        playerInput.Player.Sprint.performed += SprintStart;
        playerInput.Player.Sprint.canceled += SprintEnd;

        playerInput.Player.Interact.performed += InteractStart;
        playerInput.Player.Interact.canceled += InteractEnd;

        playerInput.Player.Backpack.performed += BackpackStart;

        playerInput.Player.Pause.performed += PauseStart;
    }

    #region Inputs Actions

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

    //Interact
    private void InteractStart(InputAction.CallbackContext context) {
        isInteracting = true;
        InteractEvent.Invoke();
    }
    private void InteractEnd(InputAction.CallbackContext context) {
        isInteracting = false;
    }

    //Backpack
    private void BackpackStart(InputAction.CallbackContext context) {
        SetBackpack();
    }

    //Pause
    private void PauseStart(InputAction.CallbackContext context) {
        SetPause();
    }

    #endregion

    void SetBackpack() {
        inBackpack = inBackpack == true ? false : true;
        canMove = CheckIfCanMove();
        if (!inBackpack) {
            windowController.OpenGameplay();
            return;
        }
        windowController.OpenBackpack();
    }
    void SetPause() {
        inPause = inPause == true ? false : true;
        canMove = CheckIfCanMove();
        if (!InPause) {
            windowController.OpenGameplay();
            return;
        } 
        windowController.OpenPause();
    }

    bool CheckIfCanMove() {
        if (inBackpack || inPause) {
            cameraBrain.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            return false;
        }
        cameraBrain.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        return true;
    }
}
