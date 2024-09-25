using System.Collections;
using UnityEngine;


namespace Pikamoon.Controller
{
    public enum WalkRunState
    {
        Walking,
        Running
    }

    public class PlayerInput : MonoBehaviour
    {
        public static PlayerInput Instance
        {
            get { return s_Instance; }
        }

        protected static PlayerInput s_Instance;

        [HideInInspector]
        public WalkRunState walkRunState = WalkRunState.Walking;
        public bool isSprinting;
        public bool isCrouching;
        public bool isSliding;
        [SerializeField] float horizontal;
        [SerializeField] float vertical;
        [SerializeField] Vector2 m_Camera;
        [SerializeField] bool jump;
        [SerializeField] float jumpVelocity;


        protected bool m_ExternalInputBlocked;
        [SerializeField] PlayerController m_Controller;
        public float Vertical
        {
            get
            {
                if (m_ExternalInputBlocked)
                    return 0;
                return vertical;
            }
        }
        public bool isMoving
        {
            get
            {
                return vertical!=0 || horizontal!=0;
            }
        }
        public float Horizontal
        {
            get
            {
                return horizontal;
            }
        }
        public Vector2 CameraInput
        {
            get
            {
                return m_Camera;
            }
        }
        public bool JumpInput
        {
            get { return jump; }
            set { jump = value; }
        }
        public float JumpVelocity
        {
            get { return jumpVelocity; }
            set { jumpVelocity = value; }
        }


        void Awake()
        {

            if (s_Instance == null)
                s_Instance = this;
            else if (s_Instance != this)
                throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_Instance.name + " and " + name + ".");
        }



        void Update()
        {
            vertical   = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");


            jump = Input.GetButton("Jump");

            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
                ToggleSprinting();
            }


            if (Input.GetKeyDown(KeyCode.LeftControl) && m_Controller.IsGrounded)
            {   
                if (isSliding)
                {
                    isSliding = false;
                    isSprinting = false;
                }
                else if(isSprinting)
                {
                    StartSliding();
                }
                else 
                {
                    ToggleCrouching();
                }
            }


            if (Input.GetKeyDown(KeyCode.Q))
            {
                ToggleWalkRunState();
            }


            if (vertical == 0 && horizontal == 0)
            {
                if(isSprinting)
                {
                    ToggleSprinting();
                }
            }

            //if (Input.GetButtonDown("Fire1"))
            //{
            //    if (m_AttackWaitCoroutine != null)
            //        StopCoroutine(m_AttackWaitCoroutine);

            //    m_AttackWaitCoroutine = StartCoroutine(AttackWait());
            //}
        }

        //IEnumerator AttackWait()
        //{
        //    attack = true;

        //    yield return m_AttackInputWait;

        //    attack = false;
        //}

        public void ToggleSprinting()
        {
            if(!isCrouching)
                if(!isSprinting)
                {
                    isSprinting = true;
                }
                else
                {
                    isSprinting = false;
                }
        }

        public void ToggleCrouching()
        {
            if (!isSprinting)
                if (!isCrouching)
                {
                    isCrouching = true;
                }
                else
                {
                    isCrouching = false;
                }
        }

        public void StartSliding()
        {
            isSliding = true;
        }

        public void ToggleWalkRunState()
        {
            walkRunState = walkRunState == WalkRunState.Walking ? WalkRunState.Running : WalkRunState.Walking;
        }

        public bool HaveControl()
        {
            return !m_ExternalInputBlocked;
        }

        public void ReleaseControl()
        {
            m_ExternalInputBlocked = true;
        }

        public void GainControl()
        {
            m_ExternalInputBlocked = false;
        }
    }
}