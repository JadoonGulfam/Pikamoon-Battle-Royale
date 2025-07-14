using UnityEngine;

public class CanvasFaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Cache the main camera reference once
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // Rotate the canvas to face the camera
        transform.forward = mainCamera.transform.forward;
    }
}
