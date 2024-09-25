using UnityEngine;
namespace Pikamoon.Controller
{
    public class ReferencesHolder : MonoBehaviour
    {
        public static ReferencesHolder Instance;

        public PlayerInput _playerInput;

        public PlayerController _playerController;

        public Transform _Camera;
        void Awake()
        {
            Instance = this;
        }
    }
}

