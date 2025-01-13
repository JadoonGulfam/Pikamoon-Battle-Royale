using UnityEngine;

namespace Pikamoon.Controller
{
    public class Sliding : State
    {
        [SerializeField] float SlideSpeed;
        [SerializeField] float SpeedDeccelerator;
        [Space]
        [SerializeField] float colliderHeight;
        [SerializeField] float colliderRadius;
        [Space]
        [SerializeField] Vector3 colliderCenter;

        Vector3 groundNormal;
        bool wasSliding;
        Vector3 slideDirection;
        float speed;
        bool isHurdleAbove;

        private void Start()
        {
            base.Initialize();
            wasSliding = false;
        }

        private void Update()
        {
            if (wasSliding)
                isHurdleAbove = Physics.CheckBox(this.transform.position + (Vector3.up*2), new Vector3(.5f, 1, .5f), Quaternion.identity, Controller.groundLayer);

            if (playerInput.isSliding && !playerInput.JumpInput)
            {
                if (!wasSliding)
                {
                    wasSliding = true;
                    OnStateStart();
                }

                Vector3 direction = Controller.GetDirectionAccordingToCameraWhenMoving();
                Controller.RotatePlayerTowardDirection(direction, 8);


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

                Controller.Move(new Vector3(slideDirection.x * speed, playerInput.JumpVelocity, slideDirection.z * speed));

                if (Controller.Velocity.magnitude <= 3)
                {
                    playerInput.isSliding = false;
                }

            }
            else
            {
                OnStateEnd();
            }
        }

        void OnStateStart()
        {
            AC.PAnimator.SetBool(AC.Parameters.isSlide.Hash, true);
            speed = SlideSpeed;

            Controller.SetCharacterController(colliderHeight, colliderRadius, colliderCenter);
        }

        void OnStateEnd()
        {
            if (wasSliding)
            {
                playerInput.isSliding = false;
                wasSliding = false;
                AC.PAnimator.SetBool(AC.Parameters.isSlide.Hash, false);
                Controller.SetCharacterControllerDefault();
                speed = 0; 
            }
        }

        RaycastHit hit;
        Vector3 GetGroundNormal()
        {
            Vector3 normal = Vector3.zero;
            if (Physics.Raycast(transform.position + (Vector3.up * .2f), Vector3.down, out hit, 1f, Controller.groundLayer))
            {
                normal = hit.normal;
            }
            return normal;
        }
        Vector3 GetGroundNormal(float rayLength)
        {
            Vector3 normal = Vector3.zero;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength, Controller.groundLayer))
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