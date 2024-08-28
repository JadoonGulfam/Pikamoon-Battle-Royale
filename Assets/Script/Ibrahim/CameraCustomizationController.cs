using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCustomizationController : MonoBehaviour
{
    [System.Serializable]
    public class CameraPosition
    {
        public string customizationOption;  // e.g., "Face", "Body", etc.
        public Vector3 position;            // Desired camera position for this option
        public Vector3 rotation;            // Desired camera rotation for this option
    }

    public Camera mainCamera;                // Reference to the main camera
    public CameraPosition[] cameraPositions; // Array of camera positions for customization options
    public float transitionSpeed = 1.0f;     // Speed of the camera transition

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        // Set the initial camera position and rotation
        if (cameraPositions.Length > 0)
        {
            targetPosition = cameraPositions[0].position;
            targetRotation = Quaternion.Euler(cameraPositions[0].rotation);
        }
    }

    void Update()
    {
        // Smoothly move the camera towards the target position and rotation
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, transitionSpeed * Time.deltaTime);
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetRotation, transitionSpeed * Time.deltaTime);
    }

    public void ChangeCameraPosition(string customizationOption)
    {
        // Find the corresponding camera position for the customization option
        foreach (CameraPosition camPos in cameraPositions)
        {
            if (camPos.customizationOption == customizationOption)
            {
                targetPosition = camPos.position;
                targetRotation = Quaternion.Euler(camPos.rotation);
                break;
            }
        }
    }
}
