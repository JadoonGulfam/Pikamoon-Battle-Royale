using UnityEngine;
using UnityEngine.EventSystems;

public class CursorLockController : MonoBehaviour
{
    private bool isCursorLocked;

    private void Start()
    {
        // Start with the cursor unlocked
        UnlockCursor();
    }

    private void Update()
    {
        // Lock the cursor when the player clicks and no UI is clicked
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            LockCursor();
        }

        // Unlock the cursor when the player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }
    }

    // Locks the cursor and makes it invisible
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;
    }

    // Unlocks the cursor and makes it visible
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
    }

    // Check if the mouse pointer is over a UI element
    private bool IsPointerOverUI()
    {
        // This will return true if the mouse is over any UI element
        return EventSystem.current.IsPointerOverGameObject();
    }
}
