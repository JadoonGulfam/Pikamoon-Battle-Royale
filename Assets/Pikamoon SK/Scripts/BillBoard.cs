using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera

    void LateUpdate()
    {
        // Get the direction to the camera (flattened on the Y-axis)
        Vector3 direction = cameraTransform.position - transform.position;

        // Ignore any vertical tilt by zeroing out the Y-axis component
        direction.y = 0;

        // Only rotate the health bar if the direction is valid (non-zero)
        if (direction.magnitude > 0)
        {
            // Make the health bar face the camera in the Y-axis only
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
