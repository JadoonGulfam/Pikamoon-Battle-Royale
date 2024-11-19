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
    public class Locomotion : MonoBehaviour
    {
        #region Public Fields
        [Header("References")]
        [Space]
        [SerializeField] PlayerInput input;
        [SerializeField] Transform _camera;

        [Header("Animation")]
        [Space]
        [SerializeField] string _walkRunAnimName;
        int _walkRunAnimHash;
        int _speedAnimHash;
        [SerializeField] float AnimationChangeDampening;

        [Header("Movement")]
        [Space]
        public float Acceleration;

        [Header("Rotation")]
        [Space]
        public float turnSmoothTime;


        #endregion

        #region Private Fields

        PlayerController playerController;
        Sliding sliding;

        float moveToSpeed;
        float animSpeed;
        float moveToBlendValue;

        #endregion


        private void Start()
        {
            input = ReferencesHolder.Instance._playerInput;
            _camera = ReferencesHolder.Instance._CameraController.camera.transform;
            playerController = this.GetComponent<PlayerController>();

            moveToSpeed = 0;
            playerController.Speed = 0;
            animSpeed = 0;
            moveToBlendValue = 0;


            _walkRunAnimHash = Animator.StringToHash(_walkRunAnimName);
            _speedAnimHash = Animator.StringToHash("Speed");
        }

        private void Update()
        {
            HandleSpeed();

            MovementAndRotationHandler();

            HandleAnimation();
        }

        void HandleSpeed()
        {
            if (input.isMoving)
            {
                if (input.isSprinting)
                {
                    moveToSpeed = playerController.Data.SprintSpeed;
                    moveToBlendValue = 2f;
                }
                else if (input.isCrouching)
                {
                    moveToSpeed = playerController.Data.WalkSpeed;
                    moveToBlendValue = 1;
                }
                else if(playerController.IsInAttack)
                {
                    moveToSpeed = playerController.Data.WalkSpeed;
                    moveToBlendValue = .2f;
                }
                else
                {
                    moveToSpeed = input.walkRunState == WalkRunState.Walking ? playerController.Data.WalkSpeed : playerController.Data.RunSpeed;
                    moveToBlendValue = input.walkRunState == WalkRunState.Walking ? 0.2f : 1f;
                }
            }
            else
            {
                moveToSpeed = 0;
                moveToBlendValue = 0;
            }

            if(input.isSliding)
            {
                //moveToBlendValue = sliding.
            }

            playerController.Speed = Mathf.Lerp(playerController.Speed, moveToSpeed, Time.deltaTime * Acceleration);

            animSpeed = Mathf.Lerp(animSpeed, moveToBlendValue, Time.deltaTime * AnimationChangeDampening);
        }

        void HandleAnimation()
        {
            playerController.Anim.SetFloat(_speedAnimHash, animSpeed);
            if(playerController.IsGrounded && !playerController.IsInAttack)
            {
                playerController.Anim.SetBool(_walkRunAnimHash, input.isMoving);
            }
        }

        void MovementAndRotationHandler()
        {
            if (input.isSliding || playerController.IsInAttack)
                return;


            playerController.Anim.SetFloat("YVal", 1);

            Vector3 direction = playerController.GetDirectionAccordingToCameraWhenMoving();

            playerController.RotatePlayerTowardDirection(direction, turnSmoothTime);

            // Always apply vertical velocity (for gravity or jumping)
            Vector3 finalMove = new Vector3(direction.x * playerController.Speed, input.JumpVelocity, direction.z * playerController.Speed);

            // Move the character based on calculated velocity and speed
            playerController.Move(finalMove);
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