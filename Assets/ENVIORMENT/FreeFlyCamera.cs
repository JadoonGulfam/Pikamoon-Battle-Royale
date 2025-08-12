using UnityEngine;

public class FreeFlyCamera : MonoBehaviour
{
    public float moveSpeed = 5f;       // Base movement speed
    public float fastMultiplier = 3f;  // Speed when holding Shift
    public float slowMultiplier = 0.25f; // Speed when holding Ctrl
    public float lookSensitivity = 2f; // Mouse look sensitivity
    public bool lockCursor = true;     // Lock mouse to screen center

    private float yaw;
    private float pitch;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    void HandleMovement()
    {
        float currentSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed *= fastMultiplier;
        else if (Input.GetKey(KeyCode.LeftControl))
            currentSpeed *= slowMultiplier;

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) direction += transform.forward;
        if (Input.GetKey(KeyCode.S)) direction -= transform.forward;
        if (Input.GetKey(KeyCode.A)) direction -= transform.right;
        if (Input.GetKey(KeyCode.D)) direction += transform.right;
        if (Input.GetKey(KeyCode.Q)) direction -= transform.up;
        if (Input.GetKey(KeyCode.E)) direction += transform.up;

        transform.position += direction.normalized * currentSpeed * Time.deltaTime;
    }
}
