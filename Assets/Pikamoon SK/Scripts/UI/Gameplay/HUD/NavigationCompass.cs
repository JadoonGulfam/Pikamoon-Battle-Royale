using UnityEngine;

namespace Pikamoon.UI
{

    public class NavigationCompass : MonoBehaviour
    {
        [Header("Player Transform")]
        public Transform Camera;
        [SerializeField] Transform NavigationCircle;

        void Update()
        {
            if (Camera == null) return;

            // Get the player's Y rotation
            float playerYaw = Camera.eulerAngles.y;

            // Rotate the compass in the opposite direction
            NavigationCircle.localRotation = Quaternion.Euler(0f, 0f, playerYaw);
        }
    }

}