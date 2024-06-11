using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float distance = 5.0f; // Distance from the player
    public float height = 2.0f; // Height from the player
    public float rotationSpeed = 5.0f; // Speed of camera rotation

    private float currentX = 0.0f; // Current rotation on the X axis
    private float currentY = 0.0f; // Current rotation on the Y axis
    public float yMinLimit = -20f; // Minimum Y rotation limit
    public float yMaxLimit = 80f; // Maximum Y rotation limit

    void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
    }

    void Update()
    {
        // Get mouse input
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Clamp the Y rotation
        currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the new camera position
            Vector3 direction = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 position = player.position + rotation * direction + Vector3.up * height;

            // Set the camera position and rotation
            transform.position = position;
            transform.LookAt(player.position + Vector3.up * height);
        }
    }
}
