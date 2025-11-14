using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    AnimatorManager animatorManager;
    
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;
    
    public float walkSpeed = 1.5f;
    public float runSpeed = 5;
    public float sprintSpeed = 10;
    public float crouchSpeed = 2;
    public float rotateSpeed = 15;
    public float jumpForce = 50;
    
    public bool isGrounded;
    public LayerMask groundMask;
    public LayerMask roofMask;
    public float groundCheckDistance = 0.3f;
    public bool isCrouching;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        animatorManager = GetComponent<AnimatorManager>();
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();
        HandleCrouch();
        CheckGroundStatus();
        
    }
    
    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.vertical;
        moveDirection += cameraObject.right * inputManager.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float currentSpeed = runSpeed;

        if (inputManager.sprintInput && inputManager.moveAmount > 0.5f)
        {
            currentSpeed = sprintSpeed;
        }
        else if (inputManager.moveAmount < 0.5f)
        {
            currentSpeed = walkSpeed;
        }

        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        
        
        Vector3 movemnetVel = moveDirection  * currentSpeed;
        playerRigidbody.linearVelocity = new Vector3(movemnetVel.x, playerRigidbody.linearVelocity.y, movemnetVel.z);
        
        animatorManager.UpdateAnimatorValues(inputManager.horizontal, inputManager.vertical);
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = cameraObject.forward * inputManager.vertical + cameraObject.right * inputManager.horizontal;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection =transform.forward;
        }
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        
        transform.rotation = newRotation;
    }

    private void HandleJump()
    {
        if (inputManager.jumpInput && isGrounded)
        {
            Debug.Log("Jump pressed!");
            animatorManager.SetJumping(); 
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            inputManager.jumpInput = false;
        }
    }
    
    private void HandleCrouch()
    {
        if (inputManager.crouchInput)
        {
            isCrouching = !isCrouching;
            animatorManager.SetCrouching(isCrouching);

            if (isCrouching)
            {
                transform.localScale = new Vector3(1, 0.8f, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1f, 1);
            }

            inputManager.crouchInput = false;
        }
    }

    private void CheckGroundStatus()
    {
        int combinedMask = groundMask | roofMask;
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance, combinedMask);
    }
}
