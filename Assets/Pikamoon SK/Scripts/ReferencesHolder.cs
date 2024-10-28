using UnityEngine;
namespace Pikamoon.Controller
{
    public class ReferencesHolder : MonoBehaviour
    {
        public static ReferencesHolder Instance;

        public PlayerInput _playerInput;

        public PlayerController _playerController;

        public CameraController _CameraController;
        void Awake()
        {
            Instance = this;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked ;

        }

        private void OnApplicationFocus(bool focus)
        {
            Cursor.visible = !focus;
            Cursor.lockState = focus ? CursorLockMode.Locked:CursorLockMode.None;
        }
    }


}

