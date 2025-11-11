using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Pikamoon.Controller
{
    public class Airbourne : State
    {
        public PlayerData playerData;

        [SerializeField] float Gravity = 9.81f;
        [SerializeField] float TransitionTime = 0.1f;
        [SerializeField] float CoyoteJumpTime = 0.2f;
        [SerializeField] float ApexHungTime = 0.2f;
        [SerializeField, Range(0.0f, 1f)] float ApexGravityMultiplier = 0.1f; // less gravity at jump apex

        [Header("Animation State")]
        [SerializeField] string _fallStateName = "Airbourne.Fall";
        [SerializeField] string _jumpStateName = "Airbourne.Jump";
        int _fallStateHash;
        int _jumpStateHash;

        [Header("Events")]
        public UnityEvent OnJumpStart;
        public UnityEvent OnLanded;

        bool isJumping;
        bool isInApexHang;
        bool isInCoyoteJump;
        bool hasApexed;

        Coroutine coyoteJumpRoutine;
        Coroutine apexHangRoutine;

        public PlayerSetupForMultiplayer MP_Setup;

        public override void Initialize()
        {
            base.Initialize();
            Setup();
        }

        public override void Initialize(Transform Root)
        {
            base.Initialize(Root);
            Setup();
        }

        void Setup()
        {
            isJumping = false;
            isInApexHang = false;
            isInCoyoteJump = false;
            hasApexed = false;

            _fallStateHash = Animator.StringToHash(_fallStateName);
            _jumpStateHash = Animator.StringToHash(_jumpStateName);

            playerInput.onJump_Down += StartJumping;
        }

        public override StateType GetStateType() => StateType.Air;

        private void Update()
        {
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;

            HandleGravity();
        }

        void StartJumping()
        {
            if (Controller.IsUIOpened || Controller.IsRootMotionEnabled || Controller.IsSwimming)
                return;

            // Allow jump if grounded OR in coyote window
            if (!Controller.IsGrounded && !isInCoyoteJump)
                return;

            // Calculate upward velocity
            playerInput.JumpVelocity = Mathf.Sqrt(playerData.JumpHeight * 2f * Gravity);
            isJumping = true;
            hasApexed = false;

            // Stop coyote timer if jumping
            if (coyoteJumpRoutine != null)
            {
                StopCoroutine(coyoteJumpRoutine);
                isInCoyoteJump = false;
                coyoteJumpRoutine = null;
            }

            AC.SetAnimationState(_jumpStateHash, TransitionTime);
            OnJumpStart?.Invoke();
        }

        void Landed()
        {
            if (!Controller.InAir) return;

            isJumping = false;
            isInApexHang = false;
            isInCoyoteJump = false;
            hasApexed = false;

            if (coyoteJumpRoutine != null)
            {
                StopCoroutine(coyoteJumpRoutine);
                coyoteJumpRoutine = null;
            }

            if (apexHangRoutine != null)
            {
                StopCoroutine(apexHangRoutine);
                apexHangRoutine = null;
            }

            AC.PAnimator.SetBool(AC.Parameters.inAir.Hash, false);
            Controller.InAir = false;
            OnLanded?.Invoke();
        }

        void HandleGravity()
        {
            if (Controller.IgnoreGravity)
            {
                playerInput.JumpVelocity = 0;
                return;
            }

            if (Controller.IsGrounded)
            {
                if (playerInput.JumpVelocity < 0)
                    playerInput.JumpVelocity = -10f;

                if (Controller.InAir)
                    Landed();
            }
            else
            {
                // Player just became airborne
                if (!Controller.InAir && !Controller.IsRootMotionEnabled)
                {
                    Controller.InAir = true;
                    AC.PAnimator.SetBool(AC.Parameters.inAir.Hash, true);

                    if (isJumping)
                    {
                        AC.SetAnimationState(_jumpStateHash, TransitionTime);
                    }
                    else
                    {
                        AC.SetAnimationState(_fallStateHash, TransitionTime);
                        // ✅ Only start coyote timer when *not jumping*
                        if (coyoteJumpRoutine != null)
                            StopCoroutine(coyoteJumpRoutine);
                        coyoteJumpRoutine = StartCoroutine(StartCoyoteJumping());
                    }
                }

                // Detect apex (velocity going from positive to negative)
                if (isJumping && !hasApexed && playerInput.JumpVelocity <= 0.2f)
                {



                    AC.SetAnimationState(_fallStateHash, TransitionTime);
                    hasApexed = true;
                    if (apexHangRoutine != null)
                        StopCoroutine(apexHangRoutine);
                    apexHangRoutine = StartCoroutine(StartApexHang());
                }

                // Apply gravity normally or reduced at apex
                float gravityToApply = isInApexHang ? Gravity * ApexGravityMultiplier : Gravity;
                playerInput.JumpVelocity -= gravityToApply * Time.deltaTime;
            }
        }

        IEnumerator StartCoyoteJumping()
        {
            isInCoyoteJump = true;
            yield return new WaitForSeconds(CoyoteJumpTime);
            isInCoyoteJump = false;
        }

        IEnumerator StartApexHang()
        {
            isInApexHang = true;
            yield return new WaitForSeconds(ApexHungTime);
            isInApexHang = false;
        }

        public override void OnStart() { }
        public override void OnEnd() { }
        public override void OnUpdate() { }

        private void OnDestroy()
        {
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;

            playerInput.onJump_Down -= StartJumping;
        }
    }
}
