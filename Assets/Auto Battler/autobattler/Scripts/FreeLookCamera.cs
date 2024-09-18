using UnityEngine;

public class FreeLookCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float baseMoveSpeed = 10f;
    [SerializeField] private float baseZoomSpeed = 5f;
    [SerializeField] private float accelerationFactor = 3f;
    [SerializeField] private float maxSpeedMultiplier = 4f;
    [SerializeField] private float movementSmoothTime = 0.2f;
    [SerializeField] private float zoomSmoothTime = 0.2f;
    [SerializeField] private float rotationSmoothTime = 0.2f;
    [SerializeField] private float rotationSpeed = 100f;

    [Header("Input Settings")]
    [SerializeField] private KeyCode forwardKey = KeyCode.W;
    [SerializeField] private KeyCode backwardKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode upKey = KeyCode.E;
    [SerializeField] private KeyCode downKey = KeyCode.Q;
    [SerializeField] private KeyCode shiftKey = KeyCode.LeftShift;

    private Vector3 currentVelocity = Vector3.zero;
    private float currentMoveSpeed;
    private float currentZoomSpeed;
    private Vector3 lastMousePosition;
    private bool isRotating;

    private void Start()
    {
        // Initialize speeds
        currentMoveSpeed = baseMoveSpeed;
        currentZoomSpeed = baseZoomSpeed;
    }

    private void FixedUpdate()
    {
        HandleRotation();
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 direction = Vector3.zero;
        bool isMoving = false;

        // Determine movement direction based on key input
        if (Input.GetKey(forwardKey)) { direction += transform.forward; isMoving = true; }
        if (Input.GetKey(backwardKey)) { direction -= transform.forward; isMoving = true; }
        if (Input.GetKey(leftKey)) { direction -= transform.right; isMoving = true; }
        if (Input.GetKey(rightKey)) { direction += transform.right; isMoving = true; }
        if (Input.GetKey(upKey)) { direction += transform.up; isMoving = true; }
        if (Input.GetKey(downKey)) { direction -= transform.up; isMoving = true; }

        // Normalize direction to prevent faster diagonal movement
        if (direction.magnitude > 1f) direction.Normalize();

        // Adjust movement speed
        if (Input.GetKey(shiftKey))
        {
            currentMoveSpeed = baseMoveSpeed * maxSpeedMultiplier;
        }
        else if (isMoving)
        {
            currentMoveSpeed += accelerationFactor * Time.deltaTime;
            currentMoveSpeed = Mathf.Min(currentMoveSpeed, baseMoveSpeed * maxSpeedMultiplier);
        }
        else
        {
            currentMoveSpeed = baseMoveSpeed;
        }

        // Smoothly move the camera to the target position
        Vector3 targetPosition = transform.position + direction * currentMoveSpeed * Time.deltaTime;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, movementSmoothTime);
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button pressed
        {
            isRotating = true;
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
            Cursor.visible = false; // Hide the cursor while rotating
        }

        if (Input.GetMouseButtonUp(1)) // Right mouse button released
        {
            isRotating = false;
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Show the cursor again
        }

        if (isRotating)
        {
            // Get mouse movement delta
            float yaw = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float pitch = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            // Apply rotation with smoothing
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(pitch, yaw, 0f));
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothTime);
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        bool isZooming = Mathf.Abs(scroll) > 0;

        if (isZooming)
        {
            currentZoomSpeed += accelerationFactor * Time.deltaTime;
            currentZoomSpeed = Mathf.Min(currentZoomSpeed, baseZoomSpeed * maxSpeedMultiplier);
        }
        else
        {
            currentZoomSpeed = baseZoomSpeed;
        }

        // Smoothly zoom the camera in/out
        Vector3 targetPosition = transform.position + transform.forward * scroll * currentZoomSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, zoomSmoothTime);
    }
}
