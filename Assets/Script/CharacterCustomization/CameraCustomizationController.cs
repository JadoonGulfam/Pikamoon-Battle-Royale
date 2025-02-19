using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
        public Transform player;                 // Reference to the player transform
        public float transitionSpeed = 1.0f;     // Speed of the camera transition
        public float rotateSpeed = 1.0f;         // Speed of player rotation on mouse drag
        public float rotationResetSpeed = 1.0f;  // Speed at which the player's rotation resets

        public RectTransform dragArea;           // The UI panel/area where dragging is allowed
       // public GameObject customizationPanel;    // The specific panel that controls when dragging is active

        private Vector3 targetPosition;
        private Quaternion targetRotation;
        private Quaternion originalPlayerRotation;  // Store the original rotation of the player

        // Variables for mouse dragging and rotation reset
        private Vector3 lastMousePosition;
        private bool isDragging;
        private bool resetRotation = false;        // Flag to track if we are resetting the player's rotation

        void Start()
        {
            // Set the initial camera position and rotation
            if (cameraPositions.Length > 0)
            {
                targetPosition = cameraPositions[0].position;
                targetRotation = Quaternion.Euler(cameraPositions[0].rotation);
            }

            // Store the player's original rotation
            // if(player != null)
            originalPlayerRotation = Quaternion.Euler(-2.62f,-2.22f,-0.8f);//player.rotation;
        }

        void Update()
        {
            // Smoothly move the camera towards the target position and rotation
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, transitionSpeed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetRotation, transitionSpeed * Time.deltaTime);

            // Check if the customization panel is open before handling drag
            // if (customizationPanel.activeSelf)
            // {
            if (CharacterHoverEffect.isSelected) // Proceed only if not already selected
            {
                HandleMouseDrag();
            }
           // }

            // Smoothly reset the player rotation if needed
            if (resetRotation)
            {
                SmoothResetPlayerRotation();
            }
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

                    // Begin resetting the player rotation smoothly when the camera position changes
                    resetRotation = true;

                    break;
                }
            }
        }

        void HandleMouseDrag()
        {
            if(player == null) 
            {
                if (GameManager.instance._player == null) return;
                player = GameManager.instance._player.transform;
            }
            // Check if the mouse is over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;  // Prevent dragging if the mouse is over UI
            }
            // Check if the mouse is within the drag area (panel)
            if (IsMouseWithinDragArea())
            {
                if (Input.GetMouseButtonDown(0))  // Left mouse button pressed
                {
                    lastMousePosition = Input.mousePosition;
                    isDragging = true;
                }
                else if (Input.GetMouseButtonUp(0))  // Left mouse button released
                {
                    isDragging = false;
                }

                if (isDragging)
                {
                    Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
                    float rotationX = deltaMousePosition.x * rotateSpeed;

                    // Rotate the player horizontally when dragging along the X-axis
                    player.Rotate(Vector3.up, rotationX * Time.deltaTime * rotateSpeed);

                    lastMousePosition = Input.mousePosition;  // Update the last mouse position

                    // Stop resetting rotation while manually rotating the player
                    resetRotation = false;
                }
            }
        }

        bool IsMouseWithinDragArea()
        {
            // Convert mouse position to the RectTransform's local space
            Vector2 localMousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(dragArea, Input.mousePosition, null, out localMousePosition);

            // Check if the local mouse position is within the bounds of the RectTransform
            return dragArea.rect.Contains(localMousePosition);
        }

        void SmoothResetPlayerRotation()
        {
            if (player == null)
            {
                if (GameManager.instance._player == null) return;
                player = GameManager.instance._player.transform;
            }
            // Smoothly reset the player's rotation using Lerp
            player.rotation = Quaternion.Lerp(player.rotation, originalPlayerRotation, rotationResetSpeed * Time.deltaTime);

            // Stop resetting if the player is close to the original rotation
            if (Quaternion.Angle(player.rotation, originalPlayerRotation) < 0.1f)
            {
                player.rotation = originalPlayerRotation;  // Ensure it's fully reset
                resetRotation = false;                    // Stop the reset
            }
        }
    }