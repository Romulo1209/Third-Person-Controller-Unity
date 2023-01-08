using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [Range(1, 12)] public float movementSpeed = 6f;
    [Range(1, 12)] public float sprintSpeed = 3f;
    [Range(1, 10)] public float velocityMultiplier = 3f;
    [Range(0, 50)] public float actualSpeed = 0f;
    [Range(1, 10)] public float jumpHeight = 3f;
    [Range(0.05f, 1)] float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Header("States")]
    [SerializeField] bool isGrounded;
    [SerializeField] bool onBackpack;

    [Header("Gravity")]
    public float gravityForce = -9.81f;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Vector3 velocity;

    [Header("Components")]

    [SerializeField] private PlayerInputController playerInput; 
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 0;
    }
    void Update()
    {
        if (playerInput.CanMove)
            Movement(playerInput.Movement);
        Gravity();
        Animations();
    }


    public void Movement(Vector2 dir)
    {
        Vector3 direction = new Vector3(dir.x, 0f, dir.y).normalized;
        float sprSpeed = playerInput.IsSprinting ? sprintSpeed : 0f;
        float maxVelocity = movementSpeed + sprSpeed;

        if (direction.magnitude >= 0.1f) {
            if (actualSpeed >= maxVelocity)
                actualSpeed -= Time.deltaTime * velocityMultiplier;
            else
                actualSpeed += Time.deltaTime * velocityMultiplier;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * actualSpeed * Time.deltaTime);
        }
        else {
            if (actualSpeed > 0)
                actualSpeed -= Time.deltaTime * 50;
            else
                actualSpeed = 0;
        }
    }
    public void Jump() {
        if (isGrounded) {
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityForce);
        }
    }

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravityForce * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Animations() {
        animator.SetBool("OnGround", isGrounded);
        animator.SetBool("Moving", playerInput.IsMoving);
        animator.SetFloat("Velocity", actualSpeed / ((movementSpeed + sprintSpeed) / 2));
    }
}