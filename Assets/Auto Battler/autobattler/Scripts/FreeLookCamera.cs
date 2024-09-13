using UnityEngine;

public class FreeLookCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float baseMoveSpeed = 10f;          // Base speed for camera movement
    [SerializeField] private float baseZoomSpeed = 5f;           // Base speed for zooming in and out
    [SerializeField] private float accelerationFactor = 3f;      // Factor by which the speed increases over time
    [SerializeField] private float maxSpeedMultiplier = 4f;      // Max multiplier for speed
    [SerializeField] private float movementSmoothTime = 0.2f;    // Smoothing time for movement
    [SerializeField] private float zoomSmoothTime = 0.2f;        // Smoothing time for zooming
    [SerializeField] private float rotationSmoothTime = 0.2f;    // Smoothing time for rotation
    [SerializeField] private float panSpeed = 0.3f;              // Speed for panning the camera with middle mouse button
    [SerializeField] private float rotationSpeed = 100f;         // Speed of camera rotation

    [Header("Input Settings")]
    [SerializeField] private KeyCode forwardKey = KeyCode.W;
    [SerializeField] private KeyCode backwardKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode upKey = KeyCode.E;           // Up movement key
    [SerializeField] private KeyCode downKey = KeyCode.Q;         // Down movement key
    [SerializeField] private KeyCode shiftKey = KeyCode.LeftShift; // Key to increase speed

    private Vector3 currentVelocity = Vector3.zero;              // Velocity for movement smoothing
    private float currentMoveSpeed;                              // Current movement speed
    private float currentZoomSpeed;                              // Current zoom speed
    private Vector3 lastMousePosition;

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

        // Increase or reset the movement speed based on input
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
        if (Input.GetMouseButton(1)) // Right mouse button to rotate
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

            // Calculate target rotation based on mouse movement
            float yaw = mouseDelta.x * rotationSpeed * Time.deltaTime;
            float pitch = -mouseDelta.y * rotationSpeed * Time.deltaTime;

            // Apply rotation with smoothing
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(pitch, yaw, 0f));
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothTime);
        }

        lastMousePosition = Input.mousePosition;
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        bool isZooming = Mathf.Abs(scroll) > 0;

        // Gradually increase zoom speed when scrolling
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
