using Pikamoon.Controller;
using UnityEngine;
namespace Pikamoon.Controller
{
    public class Sliding : MonoBehaviour
    {
        [SerializeField] PlayerController playerController;
        [SerializeField] PlayerInput PlayerInput;
        [SerializeField] float SlideSpeed;
        [SerializeField] float SpeedDeccelerator;
        [Space]
        [SerializeField] float colliderHeight;
        [SerializeField] float colliderRadius;
        [SerializeField] Vector3 colliderCenter;

        Vector3 groundNormal;
        bool wasSliding;
        Vector3 slideDirection;
        float speed;
        bool isHurdleAbove;
        private void Start()
        {
            wasSliding = false;

            //_playerController = GetComponent<PlayerController>();
            //PlayerInput = ReferencesHolder.Instance.GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (wasSliding)
                isHurdleAbove = Physics.CheckBox(this.transform.position + (Vector3.up*2), new Vector3(.5f, 1, .5f), Quaternion.identity, playerController.groundLayer);


            //Debug.Log("is Hurdle Above = "+ isHurdleAbove);

            if (PlayerInput.isSliding && !PlayerInput.JumpInput)
            {
                if (!wasSliding)
                {
                    wasSliding = true;
                    OnStateStart();
                }

                Vector3 direction = playerController.GetDirectionAccordingToCameraWhenMoving();
                playerController.RotatePlayerTowardDirection(direction, 8);


                groundNormal = GetGroundNormal();

                float angle = GetGroundNormalAngle(groundNormal);

                slideDirection = Vector3.ProjectOnPlane(transform.forward, groundNormal);

                //float AdditionalSpeedFromSlope = 0;
                if (!isHurdleAbove)
                    if (slideDirection.y > -0.3f)
                    {
                        speed -= SpeedDeccelerator * Time.deltaTime;
                    }
                    else
                    {
                        speed = SlideSpeed + (10 * slideDirection.y) * Time.deltaTime;
                    }

                playerController.Move(new Vector3(slideDirection.x * speed, PlayerInput.JumpVelocity, slideDirection.z * speed));

                if (playerController.Velocity.magnitude <= 5)
                {
                    PlayerInput.isSliding = false;
                }

            }
            else
            {
                OnStateEnd();
            }
        }

        void OnStateStart()
        {
            Debug.Log("State Start Method");
            playerController.Anim.SetBool("isSlide", true);
            speed = SlideSpeed;

            playerController.SetCharacterController(colliderHeight, colliderRadius, colliderCenter);
        }

        void OnStateEnd()
        {
            if (wasSliding)
            {
                PlayerInput.isSliding = false;
                wasSliding = false;
                playerController.Anim.SetBool("isSlide", false);
                playerController.SetCharacterControllerDefault();
                speed = 0; 
            }
        }

        RaycastHit hit;
        Vector3 GetGroundNormal()
        {
            Vector3 normal = Vector3.zero;
            if (Physics.Raycast(transform.position + (Vector3.up * .2f), Vector3.down, out hit, 1f, playerController.groundLayer))
            {
                normal = hit.normal;
            }
            return normal;
        }
        Vector3 GetGroundNormal(float rayLength)
        {
            Vector3 normal = Vector3.zero;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength, playerController.groundLayer))
            {
                normal = hit.normal;
            }
            return normal;
        }

        float GetGroundNormalAngle(Vector3 Normal)
        {
            float angle = 0;

            Vector3 worldUp = Vector3.up;

            // Calculate the angle between the ground normal and the world up vector
            angle = Vector3.Angle(Normal, worldUp);


            return angle;
        }

        void checkNormalAngleToGround()
        {

        }
    }
}