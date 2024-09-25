using UnityEngine;

namespace Pikamoon.Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public PlayerData Data;
        [SerializeField] Animator animator;
        [SerializeField] bool inAir;
        [SerializeField] bool canExitCrouch;
        [SerializeField] Vector3 crouchColliderOffset;

        [Header("Grounded Settings")]
        [Space]
        [SerializeField] bool isGrounded;
        [SerializeField] Vector3 groundCheckColliderScale;
        public LayerMask groundLayer;

        Transform _camera;
        PlayerInput input;

        CharacterController characterController;

        float defaultHeight;
        float defaultRadius;
        Vector3 defaultCenter;

        public bool InAir
        {
            get
            {
                return inAir;
            }
            set
            {
                inAir = value;
            }
        }
        public bool IsGrounded
        {
            get
            {
                //return Physics.CheckBox(this.transform.position, groundCheckColliderScale, Quaternion.identity, groundLayer);
                return characterController.isGrounded;
            }
        }
        public bool CanExitCrouch
        {
            get
            {
                if (input.isCrouching && Physics.CheckBox(this.transform.position + crouchColliderOffset, new Vector3(.5f, 1, .5f), Quaternion.identity, groundLayer))
                    return true;
                else
                    return false;
            }
        }
        public Animator Anim
        {
            get
            {
                return animator;
            }
            set
            {
                animator = value;
            }
        }

        public Vector3 Velocity
        {
            get 
            {
                return characterController.velocity;
            }

        }


        private void Awake()
        {
            ReferencesHolder.Instance._playerController = this;
            input = ReferencesHolder.Instance._playerInput;
            characterController = this.GetComponent<CharacterController>(); 
            _camera = ReferencesHolder.Instance._Camera;

            defaultHeight = characterController.height;
            defaultRadius = characterController.radius;
            defaultCenter = characterController.center;
        }


        public Vector3 GetDirectionAccordingToCamera()
        {
            Vector3 direction = transform.forward;

            if (input.isMoving)
            {
                Vector3 CamForward = _camera.forward.normalized;
                Vector3 CamRight = _camera.right.normalized;

                CamForward.y = 0;
                CamRight.y = 0;

                direction = (CamForward * input.Vertical + CamRight * input.Horizontal).normalized;
            }

            return direction;
        }

        public void RotatePlayerTowardDirection(Vector3 Direction, float damping)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Direction), Time.deltaTime * damping);
        }

        public void Move(Vector3 direction)
        {
            characterController.Move(direction * Time.deltaTime);
        }

        public void Move(float Speed)
        {

        }

        public void Move(Vector3 direction, float Speed)
        {
            characterController.Move(direction * Speed * Time.deltaTime);
        }

        public void MoveTowards(Transform Target, float Speed)
        {

        }

        public void SetCharacterController(float height, float radius, Vector3 center)
        {
            if (characterController)
            {
                Debug.Log("Came Here");
                characterController.height = height;
                characterController.radius = radius;
                characterController.center = center;
            }
        }
        public void SetCharacterControllerDefault()
        {
            if (characterController)
            {
                characterController.height = defaultHeight;
                characterController.radius = defaultRadius;
                characterController.center = defaultCenter;
            }
        }

        public void SetAnimationState(string stateName, float transitionDuration = 0.1f)
        {
            if (animator.HasState(0, Animator.StringToHash(stateName)))
                animator.CrossFadeInFixedTime(stateName, transitionDuration, 0);
        }
        public void SetAnimationState(int stateHash, float transitionDuration = 0.1f)
        {
            if (animator.HasState(0, stateHash))
                animator.CrossFadeInFixedTime(stateHash, transitionDuration, 0);
        }

    }
}