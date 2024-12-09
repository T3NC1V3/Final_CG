using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 8f;           
    public float turbo = 15f;        
    public float accelerate = 12f;       
    public float decelerate = 15f;       // Driving mechanics

    public Transform cameraTransform;

    private Rigidbody body;
    private Vector3 moveInput;
    private float currentSpeed;
    private bool nitroActive;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        currentSpeed = speed;

        if(cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Fallback option
        }

    }

    private void Update()
    {
        // Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(moveX, 0f, moveZ).normalized;

        // Run Input
        nitroActive = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = nitroActive ? turbo : speed;

        // Update speed
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (moveInput.magnitude >= 0.1f)
        {
            // Get the forward direction of the camera
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            // Flatten the camera's forward and right directions on the XZ plane
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calculate the movement direction relative to the camera
            Vector3 desiredMoveDirection = (cameraForward * moveInput.z + cameraRight * moveInput.x).normalized;

            // Rotate the player to face the movement direction
            transform.rotation = Quaternion.LookRotation(desiredMoveDirection);

            // Apply the movement force via rigidbody
            Vector3 targetVelocity = desiredMoveDirection * currentSpeed;
            Vector3 velocity = body.velocity;
            Vector3 velocityChange = targetVelocity - new Vector3(velocity.x, 0, velocity.z);

            // Apply acceleration or deceleration
            if (velocityChange.magnitude > accelerate * Time.deltaTime)
            {
                velocityChange = velocityChange.normalized * accelerate * Time.deltaTime;
            }

            body.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        else
        {
            // Slow down when no input is given (deceleration)
            Vector3 horizontalVelocity = new Vector3(body.velocity.x, 0, body.velocity.z);
            Vector3 decelerationForce = -horizontalVelocity.normalized * decelerate * Time.deltaTime;

            if (horizontalVelocity.magnitude < decelerate * Time.deltaTime)
                body.velocity = new Vector3(0, body.velocity.y, 0);  // Stop completely
            else
                body.AddForce(decelerationForce, ForceMode.VelocityChange);
        }
    }
}
