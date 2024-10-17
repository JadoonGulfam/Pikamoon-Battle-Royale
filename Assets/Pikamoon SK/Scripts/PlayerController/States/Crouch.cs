
using UnityEngine;
namespace Pikamoon.Controller
{
    public class Crouch : MonoBehaviour
    {
        [SerializeField] float height;
        [SerializeField] float radius;
        [SerializeField] Vector3 center;
        [Space]
        [SerializeField] Vector3 crouchColliderOffset;
        [Header("Animation")]
        [Space]
        [SerializeField] string _crouchStateName = "isCrouch";
        int _crouchStateHash;

        bool _isCrouching;
        PlayerInput input;
        PlayerController playerController;

        bool isHurdleAboveWhileCrouch;

        private void Start()
        {
            playerController = ReferencesHolder.Instance._playerController;
            input = ReferencesHolder.Instance._playerInput;

            _crouchStateHash = Animator.StringToHash(_crouchStateName);
        }
        private void Update()
        {
            if (_isCrouching)
                isHurdleAboveWhileCrouch = Physics.CheckBox(this.transform.position + crouchColliderOffset, new Vector3(.5f, 1, .5f), Quaternion.identity, playerController.groundLayer);

            if (input.isCrouching)
            {
                if (!_isCrouching)
                {
                    _isCrouching = true;
                    StartCrouching();
                    isHurdleAboveWhileCrouch = false;
                }
            }
            else
            {
                if (!isHurdleAboveWhileCrouch)
                {
                    if (_isCrouching)
                    {
                        _isCrouching = false;
                        EndCrouching();
                    }
                }
            }
        }

        public void StartCrouching()
        {
            playerController.Anim.SetBool(_crouchStateHash, true);

            playerController.SetCharacterController(height, radius, center);
        }


        public void EndCrouching()
        {
            playerController.Anim.SetBool(_crouchStateHash, false);

            playerController.SetCharacterControllerDefault();
        }
    }
}

