using System.Collections;
using UnityEngine;


namespace Pikamoon.Controller
{
    public delegate void OnBtnClicked();
    public delegate void OnBtnDown();
    public delegate void OnBtnUp();

    public class PlayerInput : MonoBehaviour
    {
        public static PlayerInput Instance
        {
            get { return s_Instance; }
        }

        protected static PlayerInput s_Instance;
        public OnBtnClicked onAttack1_Clicked;
        public OnBtnDown onAttack1_Down;
        public OnBtnUp onAttack1_Up;


        public OnBtnClicked onAttack2_Clicked; 
        public OnBtnDown onAttack2_Down;
        public OnBtnUp onAttack2_Up;

        public OnBtnDown onSprint_Down;
        public OnBtnUp onSprint_Up;

        public OnBtnDown onCrouch_Down;
        public OnBtnUp onCrouch_Up;

        public OnBtnDown onWalk_Down;
        public OnBtnUp   onWalk_Up;

        public OnBtnDown onPrimaryWeaponSelect_Down;
        public OnBtnDown onSecondaryWeaponSelect_Down;

        public OnBtnDown onJump_Down;
        
        [HideInInspector]
        //public bool isSprinting;
        //public bool isCrouching;
        //public bool isSliding;
        [SerializeField] float horizontal;
        [SerializeField] float vertical;

        [SerializeField] Vector2 m_Camera;
        [SerializeField] bool jump;
        [SerializeField] float jumpVelocity;
        [Space]
        [SerializeField] bool attack1;
        [SerializeField] bool attack2;

        [SerializeField] float m_AttackInputWait;
        protected bool m_ExternalInputBlocked;
        //[SerializeField] PlayerController m_Controller;


        Coroutine m_AttackWaitCoroutine;
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
                return vertical != 0 || horizontal != 0;
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
        public bool isAttacking
        {
            get { return attack1 || attack2; }
        }

        public bool isAttack_1
        {
            get { return attack1; }
            set { attack1 = value; }
        }
        public bool isAttack_2
        {
            get { return attack2; }
            set { attack2 = value; }
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

            if(Input.GetKeyDown(KeyCode.Space))
            {
                onJump_Down?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                onSprint_Down?.Invoke();
                //if (!isCrouching)
                //{
                //    ToggleSprinting();
                //}
            }

            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                onSprint_Up?.Invoke();
            }


            if (Input.GetKeyDown(KeyCode.LeftControl))// && m_Controller.IsGrounded)
            {
                Debug.Log("KSKSKSKSKSKSKSKSK");
                onCrouch_Down?.Invoke();
                //if (isSliding)
                //{
                //    isSliding = false;
                //    isSprinting = false;
                //    ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Default);
                //}
                //else if (isSprinting)
                //{
                //    StartSliding();
                //}
                //else
                //{
                //    ToggleCrouching();
                //}
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))// && m_Controller.IsGrounded)
            {
                onCrouch_Up?.Invoke();
               
            }


            if (Input.GetKeyDown(KeyCode.Q))
            {
                onWalk_Down?.Invoke();
            }
            
            if(Input.GetKeyUp(KeyCode.Q))
            {
                onWalk_Up?.Invoke();
            }



            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                onPrimaryWeaponSelect_Down?.Invoke();
            }


            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                onSecondaryWeaponSelect_Down?.Invoke();
            }


            //if (vertical == 0 && horizontal == 0)
            //{
            //    if(isSprinting)
            //    {
            //        ToggleSprinting();
            //    }
            //}

            if (Input.GetButtonDown("Fire1"))
            {
                //Debug.Log("LMB Down");
                onAttack1_Clicked?.Invoke();
                onAttack1_Down?.Invoke();
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                //Debug.Log("LMB Up");
                onAttack1_Up?.Invoke();
            }


            if (Input.GetButtonDown("Fire2"))
            {
                //Debug.Log("RMB Down");
                onAttack2_Clicked?.Invoke();
                onAttack2_Down?.Invoke();
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                //Debug.Log("RMB Up");
                onAttack2_Up?.Invoke();
            }
        }

        //IEnumerator Attack1Wait()
        //{
        //    attack1 = true;

        //    yield return new WaitForSeconds(m_AttackInputWait);

        //    attack1 = false;
        //}

        //IEnumerator Attack2Wait()
        //{
        //    attack1 = true;

        //    yield return new WaitForSeconds(m_AttackInputWait);

        //    attack1 = false;
        //}




        //public void ToggleSprinting()
        //{
        //    if(!isCrouching)
        //        if(!isSprinting && !m_Controller.IsInAttack)
        //        {
        //            isSprinting = true;
        //            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Sprint);
        //        }
        //        else
        //        {
        //            isSprinting = false;
        //            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Default);
        //        }
        //}

        //public void ToggleCrouching()
        //{
        //    if (!isSprinting)
        //        if (!isCrouching)
        //        {
        //            isCrouching = true;
        //            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Crouch);
        //        }
        //        else
        //        {
        //            isCrouching = false;
        //            ReferencesHolder.Instance._CameraController.ChangeCam(Cam.Default);
        //        }
        //}

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