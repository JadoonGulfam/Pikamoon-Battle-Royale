using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;

    [Header("Manual Rotation Adjust (Z locked to 0)")]
    public float rotationX = 0f; // pitch
    public float rotationY = 0f; // yaw (will be updated by mouse)

    [Header("Mouse Settings")]
    public float mouseSensitivity = 2f;

    void Update()
    {
        // Update Y rotation with mouse input (yaw)
        float mouseX = Input.GetAxis("Mouse X");
        rotationY += mouseX * mouseSensitivity;

        // Apply rotation with Z locked to 0
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);

        // WASD movement relative to camera rotation
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) move += transform.forward;
        if (Input.GetKey(KeyCode.S)) move -= transform.forward;
        if (Input.GetKey(KeyCode.A)) move -= transform.right;
        if (Input.GetKey(KeyCode.D)) move += transform.right;

        transform.position += move.normalized * moveSpeed * Time.deltaTime;
    }

    void OnValidate()
    {
        // Ensure rotation updates in Editor
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
