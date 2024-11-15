using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;

namespace Pikamoon.Controller
{
    public class Airbourne : MonoBehaviour
    {
        public PlayerData playerData;

        [SerializeField] float Gravity;

        [Header("Animation State")]
        [Space]
        [SerializeField] string _fallStateName = "Airbourne.Fall";
        int _fallStateHash;
        [SerializeField] string _jumpStateName = "Airbourne.Jump";
        int _jumpStateHash; 
        int _inAirAnimHash;


        //[Header("Gliding")]
        //[Space]
        //[SerializeField] Vector3 GlidingSpeed;

        [Header("Events")]
        [Space]
        public UnityEvent OnJumpStart;
        public UnityEvent OnLanded;

        PlayerController playerController;
        PlayerInput playerInput;
        bool isJumping;

        private void Start()
        {
            playerController = GetComponent<PlayerController>();
            playerInput = ReferencesHolder.Instance._playerInput;

            _fallStateHash = Animator.StringToHash(_fallStateName);
            _jumpStateHash = Animator.StringToHash(_jumpStateName);
            _inAirAnimHash = Animator.StringToHash("inAir");
        }


        private void Update()
        {
            HandleGravity();
        }

        void StartJumping()
        {
            playerInput.JumpVelocity = Mathf.Sqrt(playerData.JumpHeight * 2f * Gravity);
            playerInput.JumpInput = false;
            isJumping = true;

            OnJumpStart?.Invoke();
        }

        void Landed()
        {
            isJumping = false;
            playerController.Anim.SetBool(_inAirAnimHash, false);
            OnLanded?.Invoke();
        }

        void HandleGravity()
        {
            if (playerController.IsGrounded)
            {
                if (playerInput.JumpVelocity < 0)
                {
                    playerInput.JumpVelocity = -9.8f;
                }

                isJumping = false;

                if (playerInput.JumpInput && !playerController.InAir)
                {
                    StartJumping();
                }

                if (playerController.InAir)
                {
                    playerController.InAir = false;

                    Landed();
                }
            }
            else
            {
                if(!playerController.InAir)
                {
                    playerController.InAir = true;

                    playerController.Anim.SetBool(_inAirAnimHash, true);
                    if (isJumping)
                    {
                        playerController.SetAnimationState(_jumpStateHash,.2f);
                    }
                    else
                    {
                        playerController.SetAnimationState(_fallStateHash, .2f);
                    }
                }

                playerInput.JumpVelocity -= Gravity * Time.deltaTime;
            }
        }


    }
}