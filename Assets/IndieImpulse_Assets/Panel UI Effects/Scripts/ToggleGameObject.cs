using UnityEngine;
using UnityEngine.UI;
namespace IndieImpulseAssets
{
    public class ToggleGameObject : MonoBehaviour
    {
        // Reference to the GameObject to toggle
        public GameObject targetGameObject;

        // Reference to the UI Button
        private Button toggleButton;

        void Start()
        {
            toggleButton = GetComponent<Button>();
            // Check if references are set
            if (targetGameObject == null || toggleButton == null)
            {
                Debug.LogError("Target GameObject or Button is not assigned.");
                return;
            }
         
            // Add listener to the button
            toggleButton.onClick.AddListener(ToggleObject);
        }

        void ToggleObject()
        {
            // Deactivate the GameObject
            targetGameObject.SetActive(false);

            // Reactivate the GameObject
            targetGameObject.SetActive(true);
        }
    }
}