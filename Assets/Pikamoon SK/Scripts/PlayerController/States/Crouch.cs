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

        public override void Initialize()
        {
            base.Initialize();

            isHurdleAboveWhileCrouch = false;

            playerInput.onSprint_Down +=  EnableFastSprinting;
            playerInput.onSprint_Up   += DisableFastSprinting;

            playerInput.onCrouch_Down += ToggleCrouch;
        }
        private void Update()
        {
            //if (_isCrouching)
            //    isHurdleAboveWhileCrouch = Physics.CheckBox(this.transform.position + crouchColliderOffset,
            //                                                new Vector3(.5f, 1, .5f), Quaternion.identity,
            //                                                Controller.groundLayer);

            if (Controller.CurrentPlayerState == StateType.Crouch)
            {
                if (!_isCrouching)
                {
                    _isCrouching = true;
                    OnStart();
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
                        OnEnd();                   
                    }
                }
            }
        }

        void ToggleFastSlowCrouch()
        {
            _isSlowCrouch = !_isSlowCrouch;
        }

        void ToggleCrouch()
        {
            if (_isCrouching)
            {
                Controller.ChangeState(StateType.Locomtion);
            }
        }

        void HandleSpeed()
        {
            if (playerInput.isMoving)
            {
                Controller.ChangeMovementSpeed(_isSlowCrouch ? Controller.PlayerData.CrouchWalkSpeed : Controller.PlayerData.CrouchRunSpeed);
                Controller.ChangeAnimationSpeed(_isSlowCrouch ? 1 : 2);
            }
            else
            {
                Controller.ChangeSpeed(0f, 0f);
            }
        }

        void HandleAnimation()
        {
            AC.PAnimator.SetFloat(AC.Parameters.Speed.Hash, Controller.AnimSpeed);
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
            _isSlowCrouch = false;

            ReferencesHolder.Instance._cameraController.ChangeCam(Cam.Default);

            AC.PAnimator.SetBool(AC.Parameters.isCrouch.Hash, false);

            Controller.SetCharacterControllerDefault();

            Controller.ChangeState(StateType.Locomtion);
        }

        public override void OnStart()
        {
            _isSlowCrouch = true;

            ReferencesHolder.Instance._cameraController.ChangeCam(Cam.Crouch);

            Controller.ChangeSpeed(Controller.PlayerData.CrouchWalkSpeed, 1f);

            AC.PAnimator.SetBool(AC.Parameters.isCrouch.Hash, true);

            Controller.SetCharacterController(height, radius, center);
        }

        public override void OnUpdate()
        {

        }

        void OnDestroy()
        {
            playerInput.onSprint_Down -=  EnableFastSprinting;
            playerInput.onSprint_Up   -= DisableFastSprinting;

            playerInput.onCrouch_Down -= ToggleCrouch;
        }
    }
}

