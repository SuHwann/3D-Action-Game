using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the character moves
    public float jumpForce = 10f; // Force applied to the character when they jump
    public Transform groundCheck; // Transform object used to check if the character is on the ground
    public float groundCheckRadius = 0.1f; // Radius of the sphere used to check if the character is on the ground
    public LayerMask whatIsGround; // Layer mask used to determine what objects count as ground
    private Animator animator; // Reference to the Animator component
    private CharacterController controller; // Reference to the CharacterController component
    private Vector3 movement; // Vector to store the character's movement
    private float gravity = -9.81f; // Gravity value to apply to the character

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the character
        controller = GetComponent<CharacterController>(); // Get the CharacterController component attached to the character
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the character is on the ground
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);

        // Get horizontal movement input (left/right arrow keys)
        float horizontalMovement = Input.GetAxis("Horizontal");

        // Get vertical movement input (up/down arrow keys)
        float verticalMovement = Input.GetAxis("Vertical");

        // Create a vector with the movement inputs (ignoring the y-axis)
        movement = new Vector3(horizontalMovement, 0f, verticalMovement);

        // Set the speed of the movement vector to the moveSpeed
        movement *= moveSpeed;

        // Rotate the character according to their movement direction
        if (movement.magnitude > 0f)
        {
            transform.rotation = Quaternion.LookRotation(movement);

            // Set the "isWalking" parameter in the Animator to true when the movement vector has a non-zero magnitude
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Set the "isWalking" parameter in the Animator to false when the movement vector has a zero magnitude
            animator.SetBool("isWalking", false);
        }

        // Jump action
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("jump"); // Set the "jump" trigger in the Animator to play the "Jump" animation
            movement.y = Mathf.Sqrt(jumpForce * -2f * gravity); // Calculate the upward velocity needed for the character to jump
        }

        // Set the "jump" parameter in the Animator to false when the character is grounded
        if (isGrounded)
        {
            animator.SetBool("jump", false);
        }

        // Apply gravity to the movement vector
        movement.y += gravity * Time.deltaTime;

        // Move the character with the CharacterController
        controller.Move(movement * Time.deltaTime);
    }
}
