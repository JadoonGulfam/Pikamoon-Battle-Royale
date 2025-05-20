using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;

    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float sprintSpeed = 6f;
    public float rotationSpeed = 720f; // Degrees per second

    // Parameters for animation transitions
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsSprinting = Animator.StringToHash("isSprinting");
    private static readonly int IsAttacking1 = Animator.StringToHash("isAttacking1");
    private static readonly int IsAttacking2 = Animator.StringToHash("isAttacking2");
    private static readonly int IsHit = Animator.StringToHash("isHit");
    private static readonly int IsDead = Animator.StringToHash("isDead");
    private static readonly int IsRecovering = Animator.StringToHash("isRecovering");

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        // Check if CharacterController is attached
        if (characterController == null)
        {
            Debug.LogError("CharacterController component is missing from this game object.");
        }
    }

    void Update()
    {
        if (characterController != null)
        {
            HandleMovement();
            HandleAttacks();
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        bool isWalking = direction.magnitude > 0;
        bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
        bool isSprinting = isWalking && Input.GetKey(KeyCode.LeftControl);

        animator.SetBool(IsWalking, isWalking);
        animator.SetBool(IsRunning, isRunning);
        animator.SetBool(IsSprinting, isSprinting);

        float speed = walkSpeed;
        if (isSprinting)
        {
            speed = sprintSpeed;
        }
        else if (isRunning)
        {
            speed = runSpeed;
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationSpeed);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            characterController.Move(move * speed * Time.deltaTime);
        }
    }

    void HandleAttacks()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button for Attack 1
        {
            animator.SetTrigger(IsAttacking1);
        }

        if (Input.GetMouseButtonDown(1)) // Right mouse button for Attack 2
        {
            animator.SetTrigger(IsAttacking2);
        }
    }

    // You can add more methods for handling hit and die/recover transitions
    public void GetHit()
    {
        animator.SetTrigger(IsHit);
    }

    public void Die()
    {
        animator.SetTrigger(IsDead);
    }

    public void Recover()
    {
        animator.SetTrigger(IsRecovering);
    }
}
