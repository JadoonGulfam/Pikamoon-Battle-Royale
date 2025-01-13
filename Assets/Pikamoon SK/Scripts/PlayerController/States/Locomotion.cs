using UnityEngine;

public enum CharacterStates
{
    idle,
    Walk,
    Run,
    Sprint,
    Jump,
    Slide,
    Crouch,
    Attack,
}

namespace Pikamoon.Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class Locomotion : State
    {
        #region Public Fields

        [Header("References")]
        [Space]
        [SerializeField] Transform _camera;

        [Header("Animation")]
        [Space]
        [SerializeField] float AnimationChangeDampening;

        [Header("Movement")]
        [Space]
        public float Acceleration;

        [Header("Rotation")]
        [Space]
        public float turnSmoothTime;


        #endregion

        #region Private Fields

        Sliding sliding;

        bool isSprinting;
        bool isWalking;

        #endregion


        private void Start()
        {
            base.Initialize();
            _camera = ReferencesHolder.Instance._CameraController._camera.transform;


            playerInput.onSprint_Down +=  EnableSprinting;
            playerInput.onSprint_Up   += DisableSprinting;

            playerInput.onWalkToggle_Down +=  EnableWalk;
            playerInput.onWalkToggle_Up   += DisableWalk;


        }

        private void Update()
        {
            if (playerInput.isCrouching || Controller.IsInAttack || Controller.IsSwimming)
                return;

            HandleSpeed();

            MovementAndRotationHandler();

            HandleAnimation();
        }

        void HandleSpeed()
        {
            if (playerInput.isMoving)
            {
                if (isSprinting)
                {
                    ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Sprint);
                    Controller.ChangeSpeed(Controller.PlayerData.SprintSpeed, 2f);
                }
                else
                {
                    ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Default);
                    Controller.ChangeMovementSpeed  (isWalking ? Controller.PlayerData.WalkSpeed : Controller.PlayerData.RunSpeed);
                    Controller.ChangeAnimationSpeed (isWalking ? 0.2f : 1f);
                }

                //}
            }
            else
            {
                isSprinting = false;
                ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Default);
                Controller.ChangeSpeed(0f, 0f);
            }
        }

        void HandleAnimation()
        {
            AC.PAnimator.SetFloat(AC.Parameters.Speed.Hash, Controller.AnimSpeed);
            if(Controller.IsGrounded && !Controller.IsInAttack)
            {
                AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, playerInput.isMoving);
            }
        }

        void MovementAndRotationHandler()
        {
            if (playerInput.isSliding || Controller.IsInAttack)
                return;


            AC.PAnimator.SetFloat(AC.Parameters.YVal.Hash, 1);

            Vector3 direction = Controller.GetDirectionAccordingToCameraWhenMoving();

            Controller.RotatePlayerTowardDirection(direction, turnSmoothTime);

            // Always apply vertical velocity (for gravity or jumping)
            Vector3 finalMove = new Vector3(direction.x * Controller.Speed, playerInput.JumpVelocity, direction.z * Controller.Speed);

            // Move the character based on calculated velocity and speed
            Controller.Move(finalMove);
        }

        void EnableSprinting()
        {
            isSprinting = true;
        }
        void DisableSprinting()
        {
            isSprinting = false;
        }


        void EnableWalk()
        {
            isWalking = true;
        }
        void DisableWalk()
        {
            isWalking = false;
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

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawWireCube(transform.position, Vector3.one * .5f);
        //    //    Gizmos.DrawWireCube(transform.position + transform.up - transform.up * 0.7f, Vector3.one * .4f);
        //    //    Gizmos.DrawWireCube(transform.position + transform.up - transform.up * 1.4f, Vector3.one * .4f);
        //    //    Gizmos.DrawWireCube(transform.position + transform.up - transform.up * 2f, Vector3.one * .4f);
        //}
    }
}