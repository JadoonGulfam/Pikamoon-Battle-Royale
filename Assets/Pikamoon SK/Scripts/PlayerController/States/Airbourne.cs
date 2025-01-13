using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;

namespace Pikamoon.Controller
{
    public class Airbourne : State
    {
        public PlayerData playerData;

        [SerializeField] float Gravity;

        [Header("Animation State")]
        [Space]
        [SerializeField] string _fallStateName = "Airbourne.Fall";
        int _fallStateHash;
        [SerializeField] string _jumpStateName = "Airbourne.Jump";
        int _jumpStateHash; 


        //[Header("Gliding")]
        //[Space]
        //[SerializeField] Vector3 GlidingSpeed;

        [Header("Events")]
        [Space]
        public UnityEvent OnJumpStart;
        public UnityEvent OnLanded;

        bool isJumping;

        private void Start()
        {
            base.Initialize();

            _fallStateHash = Animator.StringToHash(_fallStateName);
            _jumpStateHash = Animator.StringToHash(_jumpStateName);
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
            AC.PAnimator.SetBool(AC.Parameters.inAir.Hash, false);
            OnLanded?.Invoke();
        }

        void HandleGravity()
        {
            if(Controller.IgnoreGravity)
            {
                playerInput.JumpVelocity = 0;
                return;
            }


            if (Controller.IsGrounded)
            {
                if (playerInput.JumpVelocity < 0)
                {
                    playerInput.JumpVelocity = -22.8f;
                }

                isJumping = false;

                if (playerInput.JumpInput && !Controller.InAir)
                {
                    StartJumping();
                }

                if (Controller.InAir)
                {
                    Controller.InAir = false;

                    Landed();
                }
            }
            else 
            {
                if(!Controller.InAir)
                {
                    Controller.InAir = true;

                    AC.PAnimator.SetBool(AC.Parameters.inAir.Hash, true);
                    if (isJumping)
                    {
                        AC.SetAnimationState(_jumpStateHash,.2f);
                    }
                    else
                    {
                        AC.SetAnimationState(_fallStateHash, .2f);
                    }
                }

                playerInput.JumpVelocity -= Gravity * Time.deltaTime;
            }
        }

        public override void OnEnd()
        {
        }

        public override void OnStart()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}