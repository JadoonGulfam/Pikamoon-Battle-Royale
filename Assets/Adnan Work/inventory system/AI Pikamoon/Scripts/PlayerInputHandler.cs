using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    // Method to handle player inputs
    void Update()
    {
        // Add input handling logic here, e.g., capturing Pikamoon, releasing, etc.
        if (Input.GetKeyDown(KeyCode.E))  // Example key to capture
        {
            // Call a method from PikamoonInventory to capture a Pikamoon
        }

        if (Input.GetKeyDown(KeyCode.Q))  // Example key to release
        {
            // Call a method from PikamoonInventory to release a Pikamoon
        }
    }
}
