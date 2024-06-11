using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleCharacterController : NetworkBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
   // public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Camera playerCamera;

    private CharacterController controller;
    private Vector3 velocity;
   // public bool isGrounded;
    private Animator animator;

    void Start()
    {

        if (HasStateAuthority == false)
        return;

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerCamera = Camera.main;
        playerCamera.GetComponent<ThirdPersonCamera>().player=this.transform;
    }

    public override void FixedUpdateNetwork()
    {


        if (HasStateAuthority == false)
            return;
            // Ground check
         //   isGrounded = controller.isGrounded;// Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Get camera forward and right directions
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        // Keep the camera directions flat (ignore y component)
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Determine movement direction
        Vector3 move = (cameraForward * moveZ + cameraRight * moveX).normalized;

        // Calculate movement speed
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        // Move the player
        controller.Move(move * speed * Time.deltaTime);

        // Rotate the player to face the movement direction
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 720); // Adjust rotation speed as needed
        }

        // Set animation parameters
        animator.SetFloat("Speed", move.magnitude * speed);

        // Jumping
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Move the player based on gravity
        controller.Move(velocity * Time.deltaTime);
    }
}
