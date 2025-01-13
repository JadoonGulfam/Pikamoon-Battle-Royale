using UnityEngine;
 
namespace Pikamoon.Controller
{
    public class Crouch : State
    {
        [SerializeField] float height;
        [SerializeField] float radius;
        [SerializeField] Vector3 center;
        [Space]
        [SerializeField] Vector3 crouchColliderOffset;

        bool _isCrouching;
        bool _isSlowCrouch;
        bool isHurdleAboveWhileCrouch;

        private void Start()
        {
            base.Initialize();

            playerInput.onSprint_Down += EnableFastSprinting;
            playerInput.onSprint_Up += DisableFastSprinting;
        }
        private void Update()
        {
            if (_isCrouching)
                isHurdleAboveWhileCrouch = Physics.CheckBox(this.transform.position + crouchColliderOffset, new Vector3(.5f, 1, .5f), 
                    Quaternion.identity, Controller.groundLayer);

            if (playerInput.isCrouching)
            {
                if (!_isCrouching)
                {
                    _isCrouching = true;
                    StartCrouching();
                    isHurdleAboveWhileCrouch = false;
                }
                HandleSpeed();
                MovementAndRotationHandler();
                HandleAnimation();

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
            _isSlowCrouch = true;

            Controller.ChangeSpeed(Controller.PlayerData.CrouchWalkSpeed, .5f);

            AC.PAnimator.SetBool(AC.Parameters.isCrouch.Hash, true);

            Controller.SetCharacterController(height, radius, center);
        }


        public void EndCrouching()
        {
            AC.PAnimator.SetBool(AC.Parameters.isCrouch.Hash, false);

            Controller.SetCharacterControllerDefault();
        }

        void ToggleFastSlowCrouch()
        {
            _isSlowCrouch = !_isSlowCrouch;

            //
            //
            //Controller.ChangeSpeed(_isSlowCrouch ? Controller.PlayerData.CrouchWalkSpeed : Controller.PlayerData.CrouchRunSpeed, _isSlowCrouch ? 0.2f : 1f);
        }

        void HandleSpeed()
        {
            if (playerInput.isMoving)
            {

                //if (input.isSprinting)
                //{
                //    moveToSpeed = playerController.PlayerData.SprintSpeed;
                //    moveToBlendValue = 2f;
                //}
                //else if (input.isCrouching)
                //{
                //    moveToSpeed = playerController.PlayerData.WalkSpeed;
                //    moveToBlendValue = 1;
                //}
                //else if (playerController.IsInAttack)
                //{
                //    moveToSpeed = playerController.PlayerData.WalkSpeed;
                //    moveToBlendValue = .2f;
                //}
                //else
                //{

                    Controller.ChangeMovementSpeed(_isSlowCrouch ? Controller.PlayerData.CrouchWalkSpeed : Controller.PlayerData.CrouchRunSpeed);
                Controller.ChangeAnimationSpeed(_isSlowCrouch ? 0.5f : 1f);


                //}
            }
            else
            {
                Controller.ChangeSpeed(0f, 0f);
            }
        }

        void HandleAnimation()
        {
            AC.PAnimator.SetFloat(AC.Parameters.Speed.Hash, Controller.AnimSpeed);
            //if (Controller.IsGrounded && !Controller.IsInAttack)
            //{
            //    AC.PAnimator.SetBool(_walkRunAnimHash, playerInput.isMoving);
            //}
        }

        void MovementAndRotationHandler()
        {
            AC.PAnimator.SetFloat(AC.Parameters.YVal.Hash, 1);

            Vector3 direction = Controller.GetDirectionAccordingToCameraWhenMoving();

            Controller.RotatePlayerTowardDirection(direction, Controller.TurnSmoothTime);

            // Always apply vertical velocity (for gravity or jumping)
            Vector3 finalMove = new Vector3(direction.x * Controller.Speed, playerInput.JumpVelocity, direction.z * Controller.Speed);

            // Move the character based on calculated velocity and speed
            Controller.Move(finalMove);
        }

        void EnableFastSprinting()
        {
            _isSlowCrouch = false;
        }
        void DisableFastSprinting()
        {
            _isSlowCrouch = true;
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

        void OnDestroy()
        {
            playerInput.onSprint_Down -= ToggleFastSlowCrouch;
        }
    }
}

