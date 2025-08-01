using Fusion;
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
    //[RequireComponent(typeof(CharacterController))]
    public class Locomotion : State 
    {
        #region Public Fields

        //[Header("References")]
        //[Space]

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

        bool _isLocomoting;
        #endregion


        public override void Initialize()
        {
            
                base.Initialize();

                playerInput.onSprint_Down += EnableSprinting;
                playerInput.onSprint_Up += DisableSprinting;

                playerInput.onWalk_Down += EnableWalk;
                playerInput.onWalk_Up += DisableWalk;

                playerInput.onCrouch_Down += ToggleCrouch;
                //playerInput.onCrouch_Up += DisableCrouch;
            


        }

        public override void Initialize(Transform Root)
        {

            base.Initialize(Root);

            playerInput.onSprint_Down += EnableSprinting;
            playerInput.onSprint_Up += DisableSprinting;

            playerInput.onWalk_Down += EnableWalk;
            playerInput.onWalk_Up += DisableWalk;

            playerInput.onCrouch_Down += ToggleCrouch;
            //playerInput.onCrouch_Up += DisableCrouch;



        }
        public override StateType GetStateType()
        {
            return StateType.Locomtion;
        }
        public PlayerSetupForMultiplayer MP_Setup;
        private void Update()
        {
            //if (Controller.MP_Setup != null && !Controller.MP_Setup.isMinePlayer)
            //    return;

            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;

            if (Controller.CurrentPlayerState == StateType.Crouch || Controller.IsInAttack)
                return;


            if (Controller.CurrentPlayerState == StateType.Locomtion)
            {
                if (!_isLocomoting)
                {
                    _isLocomoting = true;
                    OnStart();
                }

                HandleSpeed();

                MovementAndRotationHandler();

                HandleAnimation();
            }
            else
            {
                if (_isLocomoting)
                {
                    _isLocomoting = false;
                    OnEnd();
                }
            }
           
        }

        void HandleSpeed()
        {
            if (playerInput.isMoving)
            {
                if (isSprinting)
                {
                    ReferencesHolder.Instance._cameraController.ChangeCam(Cam.Sprint);
                    Controller.ChangeSpeed(Controller.PlayerData.SprintSpeed, 2f);
                }
                else
                {
                    ReferencesHolder.Instance._cameraController.ChangeCam(Cam.Default);
                    Controller.ChangeMovementSpeed  (isWalking ? Controller.PlayerData.WalkSpeed : Controller.PlayerData.RunSpeed);
                    Controller.ChangeAnimationSpeed (isWalking ? 0.2f : 1f);
                }
            }
            else
            {
                if(isSprinting)
                {

                }

                isSprinting = false;
                ReferencesHolder.Instance._cameraController.ChangeCam(Cam.Default);
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
            if (Controller.CurrentPlayerState == StateType.Slide || Controller.IsInAttack)
                return;

            AC.PAnimator.SetFloat(AC.Parameters.XVal.Hash, 0);
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
            if (Controller.CurrentPlayerState == StateType.Locomtion && playerInput.isMoving)
                isSprinting = true;
        }
        void DisableSprinting()
        {
            isSprinting = false;
        }


        void EnableCrouch()
        {
            if (Controller.CurrentPlayerState != StateType.Locomtion)
                return;

            if(isSprinting)
            {
                Controller.ChangeState(StateType.Slide);
            }
            else
            {
                Controller.ChangeState(StateType.Crouch);
            }
        }


        void ToggleCrouch()
        {
            if (Controller.CurrentPlayerState != StateType.Locomtion || !Controller.IsGrounded)
                return;

            if (isSprinting)
            {
                Debug.Log("Slide On");
                Controller.ChangeState(StateType.Slide);
            }
            else
            {
                Debug.Log("Crouch On");
                Controller.ChangeState(StateType.Crouch);
            }

        }

        void EnableWalk()
        {
            if (Controller.CurrentPlayerState == StateType.Locomtion)
                isWalking = true;
        }
        void DisableWalk()
        {
            isWalking = false;
        }

        public override void OnEnd()
        {
            AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, false);
        }

        public override void OnStart()
        {
            DisableSprinting();
            AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, true);
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